using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public MenuScreen(InputHandler inputHandler) : base(inputHandler)
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

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (_button.CanClick(InputChecker.Cursor) && InputChecker.LeftClick)
            {
                _button.Click();
            }

            if (Input.Input.KeyPress(keyboard.KeyDown))
        }

        private void Play()
        {
            _screenManager.SetActive("Game Screen");
        }
    }
}
