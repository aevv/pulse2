using System;
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
        private ScreenManager screenManager;

        public MenuScreen()
        {
            _bg = new Quad(0, 0, 1024, 768);
            _bg.Texture = TextureManager.LoadImage("Assets\\bg.jpg");

            _button = new Button(300, 300, 300, 100);
            _button.Texture = TextureManager.LoadImage("Assets\\bg.jpg");
            _button.ClickEvent = () => { Play(); };

            _renderables.Add(_bg);
            _renderables.Add(_button);

            _name = "Menu Screen";

            screenManager = ScreenManager.Resolve(null);
        }

        public void OnUpdateFrame(FrameEventArgs e, MouseDevice mouse)
        {
            base.OnUpdateFrame(e);

            if (_button.Boundaries.Contains(mouse.X, mouse.Y))
            {
                if (mouse[MouseButton.Left])
                {
                    Console.WriteLine("Click");
                    _button.Click();
                }
            }

        }

        private void Play()
        {
            screenManager.SetActive("Game Screen");
        }
    }
}
