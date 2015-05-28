﻿using System;
using System.Drawing;
using OpenTK;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Songs;

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

            _btnPlay = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2, 200, 50, "Play");
            _btnPlay.ApplyTexture("Assets\\button.png");
            _btnPlay.ClickEvent = Play;
            Renderables.Add(_btnPlay);

            _btnOptions = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + screenSize.Height / 5, 200, 50,"Options");
            _btnOptions.ApplyTexture("Assets\\button.png");
            _btnOptions.ClickEvent = Options;
            Renderables.Add(_btnOptions);

            _btnQuit = new Button(screenSize.Width / 2 - 100, screenSize.Height / 2 + ((screenSize.Height / 5) * 2), 200, 50, "Quit");
            _btnQuit.ApplyTexture("Assets\\button.png");
            _btnQuit.ClickEvent = Quit;
            Renderables.Add(_btnQuit);

            Name = "Menu Screen";

            _screenManager = ScreenManager.Resolve();

            MediaPlayer.Instance.PlayRandom();
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
