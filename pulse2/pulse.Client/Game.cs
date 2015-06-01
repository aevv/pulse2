﻿using System;
using System.Drawing;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics;
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

        private FpsCounter _fpsCounter;

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
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.ClearColor(Color4.SlateGray);
            
            LoadScreens();

            _fpsCounter = new FpsCounter();
        }

        protected override void OnResize(EventArgs e)
        {
            _config.Height = Height;
            _config.Width = Width;

            GL.Viewport(new Size(Width, Height));
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, 0, Height, -10, 10);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var updateArgs = new UpdateFrameEventArgs(e, _inputHandler);
            
            _screenManager.Active.OnUpdateFrame(updateArgs);

            _fpsCounter.OnUpdateFrame(updateArgs);

            _inputHandler.SwapBuffers();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, -10, 10);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadMatrix(ref ortho);

            _screenManager.Active.OnRenderFrame(e);

            _fpsCounter.OnRenderFrame(e);

            GL.PopMatrix();
            SwapBuffers();
        }

        private void LoadScreens()
        {
            _screenManager.Add(new GameScreen(_inputHandler));
            _screenManager.Active = new MenuScreen(_inputHandler);
        }
    }
}
