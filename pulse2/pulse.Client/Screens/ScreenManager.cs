using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using pulse.Client.Scripting;

namespace pulse.Client.Screens
{
    class ScreenManager
    {
        private readonly IDictionary<string, BaseScreen> _screens;
        private EngineWrapper _engine;
        public EngineWrapper Engine { get { return _engine; } set { _engine = value; } }

        private Action<string> _titleSetter;
        private BaseScreen _activeScreen;

        public Action<string> TitleSetter { set { _titleSetter = value; } }

        public ScreenManager()
        {
            _screens = new Dictionary<string, BaseScreen>();
        }

        public BaseScreen this[string name]
        {
            get { return _screens[name]; }
        }

        public void Add(string name, BaseScreen screen)
        {
            if (_screens.ContainsKey(name))
                _screens.Remove(name);
            
            _screens.Add(name, screen);
            LoadScreenFromScript(name, screen);

            if (_screens.Count == 1)
                _activeScreen = screen;
        }

        public void Remove(string name)
        {
            _screens.Remove(name);
        }

        public BaseScreen Active
        {
            get
            {
                return _activeScreen;
            }
            set
            {
                if (_screens.All(screen => screen.Value != value))
                {
                    _screens.Add(value.Name, value);
                    LoadScreenFromScript(value.Name, value);
                }

                _activeScreen = value;
                if (_titleSetter != null)
                    _titleSetter(_activeScreen.Title);
            }
        }

        public void SetActive(string name)
        {
            if (!_screens.ContainsKey(name))
                return;

            _activeScreen = _screens[name];
            if (_titleSetter != null)
                _titleSetter(_activeScreen.Title);
        }

        private void LoadScreenFromScript(string name, BaseScreen screen)
        {
            var fileName = string.Format("Assets\\Scripts\\Screens\\{0}.js", name);
            if (!File.Exists(fileName))
                return;

            var script = File.ReadAllText(fileName);

            _engine.SetScreen(screen);
            _engine.ExecuteScript(script);
            _engine.UnsetScreen();
        }
    }
}
