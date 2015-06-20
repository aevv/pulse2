using System;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
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

        public MenuScreen(InputHandler inputHandler) : base(inputHandler)
        {
            _bg = new Background();
            Renderables.Add(_bg);

            _btnPlay = new Button(Window.Width / 2 - 100, Window.Height / 2 + 100, 0, 200, 50, "Play");
            _btnPlay.OnClick += Play;
            _btnPlay.Colour = Color4.DeepSkyBlue;
            Renderables.Add(_btnPlay);
            Renderables.Add(_btnPlay.RawText);
            Updateables.Add(_btnPlay);

            _btnOptions = new Button(Window.Width / 2 - 100, Window.Height / 2 + Window.Height / 10 + 100, 0, 200, 50, "Options");
            _btnOptions.OnClick += Options;
            Renderables.Add(_btnOptions);
            Renderables.Add(_btnOptions.RawText);
            Updateables.Add(_btnOptions);

            _btnQuit = new Button(Window.Width / 2 - 100, Window.Height / 2 + ((Window.Height / 10) * 2) + 100, 0, 200, 50, "Quit");
            _btnQuit.OnClick += Quit;
            Renderables.Add(_btnQuit);
            Renderables.Add(_btnQuit.RawText);
            Updateables.Add(_btnQuit);

            _animTest = new Animation(new Vector3(200, 200, 0), new SizeF(300, 300));
            _animTest.ApplyTextures(Enumerable.Range(0, 7).Select(i => string.Format("Assets/Burst/burst{0}.png", i)));
            _animTest.Loop = true;
            Renderables.Add(_animTest);
            Updateables.Add(_animTest);

            Name = "Menu Screen";
            Title = "pulse - Menu";

            _screenManager = ScreenManager.Resolve();

            //TODO: Refactor this hack
            var player = MediaPlayer.Instance;
            player.PlayRandom();
            var chart = SongLibrary.Instance.GetGroupChart(player.CurrentSong);
            var bgName = chart.Charts[0].BackgroundName;
            if (!string.IsNullOrEmpty(bgName))
                _bg.ApplyTexture("Assets/bg.jpg");
        }

        public override void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            base.OnUpdateFrame(args);
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
