﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using pulse.Client.Graphics;
using pulse.Client.Input;

namespace pulse.Client.Screens
{
    class MenuScreen : BaseScreen
    {
        private Quad _bg;
        private Button _button;
        private ScreenManager _screenManager;

        public MenuScreen()
        {
            _bg = new Quad(0, 0, 1024, 768);
            _bg.ApplyTexture("Assets\\bg.jpg");
            _button = new Button(300, 300, 300, 100);
            _button.ApplyTexture("Assets\\bg.jpg");
            Renderables.Add(_bg);

            _button = new Button(300, 300, 300, 100);
            _button.ApplyTexture("Assets\\bg.jpg");
            _button.ClickEvent = Play;
            Renderables.Add(_button);

            Name = "Menu Screen";

            _screenManager = ScreenManager.Resolve();
        }

        public override void OnUpdateFrame(FrameEventArgs e, MouseDevice mouse, KeyboardDevice keyboard)
        {
            base.OnUpdateFrame(e, mouse, keyboard);

            if (_button.Boundaries.Contains(mouse.X, mouse.Y))
            {
                if (Input.Input.MouseRelease(OpenTK.Input.MouseButton.Left))
                {
                    Console.WriteLine("Click");
                    _button.Click();
                }
            }

            if (Input.Input.KeyPress(Key.D))
            {
                Console.WriteLine("D");
            }

            if (Input.Input.KeyPress(keyboard.KeyDown))
        }

        private void Play()
        {
            _screenManager.SetActive("Game Screen");
        }
    }
}
