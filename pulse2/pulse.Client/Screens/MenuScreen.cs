using System;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.Scripting;
using pulse.Client.Songs;

namespace pulse.Client.Screens
{
    class MenuScreen : BaseScreen
    {
        private Animation _animTest;
        private ScreenManager _screenManager;

        public MenuScreen(ScreenManager manager, InputHandler inputHandler) : base(inputHandler)
        {
            _screenManager = manager;

            _animTest = new Animation(new Vector3(200, 200, 10), new SizeF(300, 300));
            _animTest.ApplyTextures(Enumerable.Range(0, 7).Select(i => string.Format("Assets/Burst/burst{0}.png", i)));
            _animTest.Loop = true;
            Renderables.Add(_animTest);
            Updateables.Add(_animTest);

            Name = "MenuScreen";
            Title = "pulse - Menu";

            _screenManager = manager;

            //TODO: Refactor this hack
            var player = MediaPlayer.Instance;
            player.PlayRandom();
        }
    }
}
