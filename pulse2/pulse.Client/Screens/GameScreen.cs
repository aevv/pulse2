using System.Drawing;
using System.Runtime.CompilerServices;
using OpenTK;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Events;

namespace pulse.Client.Screens
{
    class GameScreen : BaseScreen
    {
        private readonly ScreenManager _manager;
        private Quad _quad;
        private Quad _quad2;

        private double _timeTotal = 0;

        public GameScreen(ScreenManager manager, InputHandler inputHandler) : base(inputHandler)
        {
            _manager = manager;

            _quad = new Quad(0, 0, 0, 1024, 768);
            _quad.ApplyTexture("Assets\\bg.jpg");
            _quad2 = new Quad(0, 0, 0, 50, 50);
            _quad2.ApplyTexture(_quad.TextureId);
            Renderables.Add(_quad);
            Renderables.Add(_quad2);

            Name = "GameScreen";
            Title = "pulse";
        }

        private double _velocity = 2000;

        public override void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var x = _quad2.Origin.X + (_velocity * args.Time);
            var y = _quad2.Origin.Y + (_velocity*args.Time);

            _quad2.Origin = new Vector3((float)x, (float)y, _quad2.Origin.Z);

            if (x > Window.Width)
                _quad2.Origin = new Vector3(0, _quad2.Origin.Y, _quad2.Origin.Z);
            if (y > Window.Height)
                _quad2.Origin = new Vector3(_quad2.Origin.X, 0, _quad2.Origin.Z);
                //_quad2.Location = new PointF(0, (Window.Height + _quad2.Location.Y + 50) % Window.Height);
        }
    }
}
