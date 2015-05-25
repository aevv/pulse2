using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using pulse.Client.Graphics;
namespace pulse.Client.Screens
{
    class GameScreen : BaseScreen
    {
        private Quad _quad;
        private Quad _quad2;

        private double _timeTotal = 0;

        public GameScreen()
        {
            _quad = new Quad(0, 0, 1024, 768);
            _quad.Texture = TextureManager.LoadImage("Assets\\bg.jpg");
            _quad2 = new Quad(0, 0, 50, 50);
            _quad2.Texture = _quad.Texture;
            _renderables.Add(_quad);
            _renderables.Add(_quad2);

            _name = "Game Screen";
        }

        private double _velocity = 500;

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var x = _quad2.Location.X + (_velocity*e.Time);

            _quad2.Location = new PointF((float)x, _quad2.Location.Y);
        }
    }
}
