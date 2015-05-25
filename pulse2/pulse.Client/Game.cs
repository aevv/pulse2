﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Audio;
using pulse.Client.Graphics;
using pulse.Client.Screens;

namespace pulse.Client
{
    class Game : GameWindow
    {
        private GameScreen _gameScreen;
        public Game() : base(1024, 768, GraphicsMode.Default, "pulse", GameWindowFlags.Default)
        {
            this.VSync = VSyncMode.Off;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color4.SlateGray);

            _gameScreen = new GameScreen();

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            _gameScreen.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadMatrix(ref ortho);

            _gameScreen.OnRenderFrame(e);

            GL.PopMatrix();
            SwapBuffers();
        }
    }
}
