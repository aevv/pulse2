using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Audio;
using pulse.Client.Graphics;
using pulse.Client.Screens;

namespace pulse.Client
{
    class Game : GameWindow
    {
        private readonly PulseConfig _config;
        private readonly ScreenManager _screenManager;

        public Game(PulseConfig config) : base(config.Width, config.Height, GraphicsMode.Default, "pulse",
            config.Fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default)
        {
            _config = config;
            VSync = _config.Vsync ? VSyncMode.On : VSyncMode.Off;

            _screenManager = ScreenManager.Resolve();
            _screenManager.TitleSetter = title => Title = title;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color4.SlateGray);
            
            LoadScreens();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            _screenManager.Active.OnUpdateFrame(e, Mouse, Keyboard);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadMatrix(ref ortho);

            _screenManager.Active.OnRenderFrame(e);

            GL.PopMatrix();
            SwapBuffers();
        }

        private void LoadScreens()
        {
            _screenManager.Add(new GameScreen());
            _screenManager.Active = new MenuScreen();
        }
    }
}
