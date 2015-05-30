using System.Drawing;
using OpenTK;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Events;

namespace pulse.Client.Screens
{
    class GameScreen : BaseScreen
    {
        private Quad _quad;
        private Quad _quad2;

        private double _timeTotal = 0;

        public GameScreen(InputHandler inputHandler) : base(inputHandler)
        {
            _quad = new Quad(0, 0, 1024, 768);
            _quad.ApplyTexture("Assets\\bg.jpg");
            _quad2 = new Quad(0, 0, 50, 50);
            _quad2.ApplyTexture(_quad.TextureId);
            Renderables.Add(_quad);
            Renderables.Add(_quad2);

            Name = "Game Screen";
        }

        private double _velocity = 500;

        public override void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var x = _quad2.Location.X + (_velocity*args.Time);

            _quad2.Location = new PointF((float)x, _quad2.Location.Y);
            if (x > ScreenSize.Width)
                x = 0;

        }
    }
}
