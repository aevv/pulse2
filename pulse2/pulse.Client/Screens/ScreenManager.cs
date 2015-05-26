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
        private Action<string> _titleSetter;
        private readonly IList<BaseScreen> _screens;
        private BaseScreen _activeScreen;

        public Action<string> TitleSetter { set { _titleSetter = value; } }

        public static ScreenManager Resolve()
        {
            return _instance ?? (_instance = new ScreenManager());
        }

        private ScreenManager()
        {
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
                if (_titleSetter != null)
                    _titleSetter(_activeScreen.Title);
            }
        }

        public void SetActive(string name)
        {
            var screen = _screens.FirstOrDefault(s => s.Name == name);

            if (screen == null)
                return;

            _activeScreen = screen;
            if (_titleSetter != null)
                _titleSetter(_activeScreen.Title);
        }
    }
}
