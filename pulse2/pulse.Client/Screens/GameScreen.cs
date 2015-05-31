using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenTK;
using OpenTK.Input;
using pulse.Client.Gameplay;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.Songs;

namespace pulse.Client.Screens
{
    class GameScreen : BaseScreen
    {   
        private Receptor _receptor1;
        private Note _note1;
        private Note _note2;
        private Background _bg;

        private double _timeTotal = 0;

        public GameScreen(InputHandler inputHandler) : base(inputHandler)
        {
            _bg = new Background();
            Renderables.Add(_bg);

            _receptor1 = new Receptor(new PointF(100, 600), new SizeF(100, 25) );
            _receptor1.ApplyTexture("Assets\\receptor.png");
            _note1 = new Note(100, 150, 100, 25);
            _note1.ApplyTexture("Assets\\note.png");
            _note2 = new Note(100, 100, 100, 25);
            _note2.ApplyTexture("Assets\\note.png");
            Renderables.Add(_receptor1);
            Renderables.Add(_note1); 
            Renderables.Add(_note2);


            Name = "Game Screen";
            Title = "pulse";

            var player = MediaPlayer.Instance;
            var chart = player.CurrentChart;
            var bgName = chart.Charts[0].BackgroundName;
            if (!string.IsNullOrEmpty(bgName))
                _bg.ApplyTexture(TextureManager.LoadImage(chart.Files.First(file => file.FileName == bgName).Data));

        }

        private double _velocity = 100;

        public override void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var y = _note1.Location.Y + (_velocity*args.Time);

            _note1.Location = new PointF((float)100, (float)y);

            if (y > Window.Height)
                _note1.Location = new PointF(_note1.Location.X, 0);
                //_quad2.Location = new PointF(0, (Window.Height + _quad2.Location.Y + 50) % Window.Height);

            if (_receptor1.Boundaries.Contains(100, _note1.Location.Y+12))
            {
                if (InputChecker.KeyPress(Key.D))
                {
                    Console.WriteLine("Hit");
                }
            }
        }
    }
}
