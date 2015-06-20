using System;
using System.Drawing;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics;
using pulse.Client.Graphics.Engine;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.Logging;
using pulse.Client.Screens;

namespace pulse.Client
{
    class Game : GameWindow
    {
        private readonly PulseConfig _config;
        private readonly ScreenManager _screenManager;
        private readonly InputHandler _inputHandler;
        private readonly LogTracer _trace;
        private readonly IRenderer _renderer;

        public Game(PulseConfig config) : base(config.Width, config.Height, GraphicsMode.Default, "pulse",
            config.Fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default)
        {
            _trace = LogTracer.Instance;
            _trace.TraceInfo("pulse startup");
            _config = config;

            _screenManager = ScreenManager.Resolve();
            _screenManager.TitleSetter = title => Title = title;

            _inputHandler = new InputHandler(this);

            VSync = _config.Vsync ? VSyncMode.On : VSyncMode.Off;

            Icon = DefaultAssets.PulseIcon;

            _renderer = new Renderer(new Size(Width, Height));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _renderer.Initialise();
            
            LoadScreens();
        }

        protected override void OnResize(EventArgs e)
        {
            _config.Height = Height;
            _config.Width = Width;

            _renderer.Resize(Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var updateArgs = new UpdateFrameEventArgs(e, _inputHandler);
            
            _screenManager.Active.OnUpdateFrame(updateArgs);

            _inputHandler.SwapBuffers();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _renderer.OnRenderFrame(e, _screenManager.Active);

            SwapBuffers();
        }

        private void LoadScreens()
        {
            _screenManager.Add(new GameScreen(_inputHandler));
            _screenManager.Active = new MenuScreen(_inputHandler);
        }
    }
}
