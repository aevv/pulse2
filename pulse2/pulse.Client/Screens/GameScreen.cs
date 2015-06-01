using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using pulse.Client.Gameplay;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.IO;
using pulse.Client.Songs;
using pulse.Client.Songs.Mechanics;
using Note = pulse.Client.Gameplay.Note;

namespace pulse.Client.Screens
{
    class GameScreen : BaseScreen
    {   
        private Receptor _receptor1;
        private Receptor _receptor2;
        private Receptor _receptor3;
        private Receptor _receptor4;
        private Receptor _receptor5;
        private Receptor _receptor6;
        private Receptor _receptor7;
        private Background _bg;
        private ScreenManager _screenManager;
        private Button _btnBack;

        private List<Songs.Mechanics.Note> _notes;

        private double _timeTotal = 0;

        public GameScreen(InputHandler inputHandler) : base(inputHandler)
        {
            _bg = new Background();
            Renderables.Add(_bg);

            _btnBack = new Button(500, 400, 200, 50, "Back");
            _btnBack.OnClick += Back;
            _btnBack.Colour = Color4.DeepSkyBlue;
            Renderables.Add(_btnBack);
            Updateables.Add(_btnBack);

            _receptor1 = new Receptor(new PointF(100, 600), new SizeF(100, 25));
            _receptor1.ApplyTexture("Assets\\receptor.png");
            _receptor2 = new Receptor(new PointF(200, 600), new SizeF(50, 25));
            _receptor2.ApplyTexture("Assets\\receptor.png");
            _receptor3 = new Receptor(new PointF(250, 600), new SizeF(50, 25));
            _receptor3.ApplyTexture("Assets\\receptor.png");
            _receptor4 = new Receptor(new PointF(300, 600), new SizeF(50, 25));
            _receptor4.ApplyTexture("Assets\\receptor.png");
            _receptor5 = new Receptor(new PointF(350, 600), new SizeF(50, 25));
            _receptor5.ApplyTexture("Assets\\receptor.png");
            _receptor6 = new Receptor(new PointF(400, 600), new SizeF(50, 25));
            _receptor6.ApplyTexture("Assets\\receptor.png");
            _receptor7 = new Receptor(new PointF(450, 600), new SizeF(50, 25));
            _receptor7.ApplyTexture("Assets\\receptor.png");
            Renderables.Add(_receptor1);
            Renderables.Add(_receptor2);
            Renderables.Add(_receptor3);
            Renderables.Add(_receptor4);
            Renderables.Add(_receptor5);
            Renderables.Add(_receptor6);
            Renderables.Add(_receptor7);

            _notes = new FilePacker().DeserialisePackedFile<ChartGroup>("songs\\ryuhappy.pcg").Charts[0].Notes;

            for(int i = 0; i<_notes.Count; i++)
            {
                int x = 0;
                int w = 50;
                switch (_notes[i].Lane)
                {
                    case 1:
                        x = 100;
                        w = 100;
                        break;
                    case 2:
                        x = 200;
                        break;
                    case 3:
                        x = 250;
                        break;
                    case 4:
                        x = 300;
                        break;
                    case 5:
                        x = 350;
                        break;
                    case 6:
                        x = 400;
                        break;
                    case 7:
                        x = 450;
                        break;

                }
                Renderables.Add(new Note(new PointF(x, 0), new SizeF(w, 25)));
            }
            



            Name = "Game Screen";
            Title = "pulse";
            _screenManager = ScreenManager.Resolve();

        }

        private double _velocity = 100;

        public override void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            /*var y = _note1.Location.Y + (_velocity*args.Time);

            _note1.Location = new PointF((float)100, (float)y);

            if (_receptor1.Boundaries.Contains(100, _note1.Location.Y+12))
            {
                if (InputChecker.KeyPress(Key.D))
                {
                    Renderables.Remove(_note1);
                }
            }*/
        }

        public void Back()
        {
            _screenManager.SetActive("Menu Screen");
        }
    }
}
