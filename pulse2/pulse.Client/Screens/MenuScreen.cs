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
        private ScreenManager _screenManager;

        public MenuScreen(ScreenManager manager, InputHandler inputHandler) : base(inputHandler)
        {
            _screenManager = manager;

            Name = "MenuScreen";
            Title = "pulse - Menu";

            _screenManager = manager;
        }
    }
}
