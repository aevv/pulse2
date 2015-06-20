using System;
using System.Drawing;
using System.Reflection;
using Jint;
using Jint.Parser.Ast;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics;
using pulse.Client.Graphics.Engine;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.Logging;
using pulse.Client.Screens;
using pulse.Client.Scripting;
using pulse.Client.Songs;

namespace pulse.Client
{
    class Game : GameWindow
    {
        private readonly PulseConfig _config;
        private readonly ScreenManager _screenManager;
        private readonly InputHandler _inputHandler;
        private readonly LogTracer _trace;
        private readonly IRenderer _renderer;
        private readonly EngineWrapper _jsEngine;

        public Game(PulseConfig config) : base(config.Width, config.Height, GraphicsMode.Default, "pulse",
            config.Fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default)
        {
            _trace = LogTracer.Instance;
            _trace.TraceInfo("pulse startup");
            _config = config;

            // TODO: Deal with triangle ref crap
            _screenManager = new ScreenManager();
            var pulseNamespace = new ScriptAssist(_trace, _inputHandler, _screenManager, _config, MediaPlayer.Instance, this);
            _jsEngine = new EngineWrapper(pulseNamespace);
            _screenManager.Engine = _jsEngine;
            _screenManager.TitleSetter = title => Title = title;


            _inputHandler = new InputHandler(this);

            VSync = _config.Vsync ? VSyncMode.On : VSyncMode.Off;

            Icon = DefaultAssets.PulseIcon;

            _renderer = new Renderer(new Size(Width, Height));
        }

        public void Quit()
        {
            // TODO: save, prompt, all that
            Environment.Exit(0);
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
            _screenManager.Add("GameScreen", new GameScreen(_screenManager, _inputHandler));
            _screenManager.Add("MenuScreen", new MenuScreen(_screenManager, _inputHandler));
            _screenManager.SetActive("MenuScreen");
        }
    }
}
