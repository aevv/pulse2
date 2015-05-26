using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using pulse.Client.Graphics;

namespace pulse.Client.Screens
{
    class MenuScreen : BaseScreen
    {
        private Quad _bg;
        private Quad _button;

        private double _timeTotal = 0;

        public MenuScreen()
        {
            _bg = new Quad(0, 0, 1024, 768);
            _bg.ApplyTexture("Assets\\bg.jpg");
            _button = new Quad(300, 300, 300, 100);
            _button.ApplyTexture("Assets\\bg.jpg");
            Renderables.Add(_bg);

            Name = "Menu Screen";
        }

        private double _velocity = 500;

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var mouse = Mouse.GetState(0);

            if (mouse[MouseButton.Left])
            {
                
            }

        }
    }
}
