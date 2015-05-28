using System;
using System.Drawing;
using OpenTK;
using pulse.Client.Graphics;
using pulse.Client.Input;

namespace pulse.Client.Screens
{
    class MenuScreen : BaseScreen
    {
        private Background _bg;
        private Button _btnPlay;
        private Button _btnOptions;
        private Button _btnQuit;
        private ScreenManager _screenManager;
        private RawText _text;

        public MenuScreen(InputHandler inputHandler, SizeF screenSize) : base(inputHandler, screenSize)
        {
            _bg = new Background("Assets\\bg.jpg", screenSize);
            Renderables.Add(_bg);

            _btnPlay = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2, 200, 50);
            _btnPlay.ApplyTexture("Assets\\play.png");
            _btnPlay.ClickEvent = Play;
            Renderables.Add(_btnPlay);

            _btnOptions = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + screenSize.Height / 5, 200, 50);
            _btnOptions.ApplyTexture("Assets\\options.png");
            _btnOptions.ClickEvent = Options;
            Renderables.Add(_btnOptions);

            _btnQuit = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + ((screenSize.Height / 5) * 2), 200, 50);
            _btnQuit.ApplyTexture("Assets\\exit.png");
            _btnQuit.ClickEvent = Quit;
            Renderables.Add(_btnQuit);

            Name = "Menu Screen";

            _screenManager = ScreenManager.Resolve();

            _text = new RawText("Hello world!", new PointF(0, 0), true);
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (_btnPlay.IsMouseOver(InputChecker.Cursor) && InputChecker.LeftClick)
            {
                _btnPlay.Click();
            }
            if (_btnPlay.IsMouseOver(InputChecker.Cursor))
            {

            }

            if (_btnOptions.IsMouseOver(InputChecker.Cursor) && InputChecker.LeftClick)
            {
                _btnOptions.Click();
            }
            if (_btnOptions.IsMouseOver(InputChecker.Cursor))
            {

            }

            if (_btnQuit.IsMouseOver(InputChecker.Cursor) && InputChecker.LeftClick)
            {
                _btnQuit.Click();
            }
            if (_btnQuit.IsMouseOver(InputChecker.Cursor))
            {

            }
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _text.OnRenderFrame(e);
        }

        private void Play()
        {
            _screenManager.SetActive("Game Screen");
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
