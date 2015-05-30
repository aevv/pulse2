using System;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.Songs;

namespace pulse.Client.Screens
{
    class MenuScreen : BaseScreen
    {
        private Background _bg;
        private Button _btnPlay;
        private Button _btnOptions;
        private Button _btnQuit;
        private Animation _animTest;
        private ScreenManager _screenManager;
        private RawText _text;

        public MenuScreen(InputHandler inputHandler, SizeF screenSize) : base(inputHandler, screenSize)
        {
            _bg = new Background("Assets\\bg.jpg", screenSize);
            Renderables.Add(_bg);

            _btnPlay = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + 100, 200, 50, "Play");
            _btnPlay.OnClick += Play;
            _btnPlay.Colour = Color4.DeepSkyBlue;
            Renderables.Add(_btnPlay);
            Updateables.Add(_btnPlay);

            _btnOptions = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + screenSize.Height / 10 + 100, 200, 50,"Options");
            _btnOptions.OnClick += Options;
            Renderables.Add(_btnOptions);
            Updateables.Add(_btnOptions);

            _btnQuit = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + ((screenSize.Height / 10) * 2) + 100, 200, 50, "Quit");
            _btnQuit.OnClick += Quit;
            Renderables.Add(_btnQuit);
            Updateables.Add(_btnQuit);

            _animTest = new Animation(new PointF(300, 300), new SizeF(100, 100));
            _animTest.ApplyTextures(Enumerable.Range(0, 7).Select(i => string.Format("Assets/Burst/burst{0}.png", i)));
            _animTest.Depth = 5;
            _animTest.Loop = true;
            

            Name = "Menu Screen";
            Title = "pulse - Menu";

            _screenManager = ScreenManager.Resolve();

            MediaPlayer.Instance.PlayRandom();
        }

        public override void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            _animTest.OnRenderFrame(args);
        }

        private void Play()
        {
            _screenManager.SetActive("Game Screen");
            MediaPlayer.Instance.Play();
        }

        private void Options()
        {
            _screenManager.SetActive("Game Screen");
        }

        private void Quit()
        {
            Environment.Exit(0);
        }
    }
}
