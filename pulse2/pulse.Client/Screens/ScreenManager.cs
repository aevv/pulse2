using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Screens
{
    class ScreenManager
    {
        private static ScreenManager _instance;
        private readonly Action<string> _titleSetter;
        private readonly IList<BaseScreen> _screens;
        private BaseScreen _activeScreen;

        public static ScreenManager Resolve(Action<string> titleSetter)
        {
            return _instance ?? (_instance = new ScreenManager(titleSetter));
        }

        private ScreenManager(Action<string> titleSetter)
        {
            _titleSetter = titleSetter;
            _screens = new List<BaseScreen>();
        }

        public BaseScreen this[string name]
        {
            get { return _screens.First(s => s.Name == name); }
        }

        public void Add(BaseScreen screen)
        {
            if (_screens.Contains(screen))
                _screens.Remove(screen);
            
            _screens.Add(screen);

            if (_screens.Count == 1)
                _activeScreen = screen;
        }

        public void Remove(BaseScreen screen)
        {
            _screens.Remove(screen);
        }

        public BaseScreen Active
        {
            get
            {
                return _activeScreen;
            }
            set
            {
                if (!_screens.Contains(value))
                    _screens.Add(value);

                _activeScreen = value;
                _titleSetter(_activeScreen.Name);
            }
        }

        public void SetActive(string name)
        {
            var screen = _screens.FirstOrDefault(s => s.Name == name);

            if (screen == null)
                return;

            _activeScreen = screen;
            _titleSetter(_activeScreen.Name);
        }
    }
}
