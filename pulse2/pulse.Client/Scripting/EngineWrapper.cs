using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using pulse.Client.Graphics.Interface;
using pulse.Client.Screens;
using pulse.Client.Scripting.Factories;

namespace pulse.Client.Scripting
{
    class EngineWrapper
    {
        private readonly Engine _engine;
        private readonly ScriptAssist _pulse;

        public EngineWrapper(ScriptAssist pulse)
        {
            _engine = new Engine();
            _pulse = pulse;

            _engine.SetValue("_pulse", _pulse);
            _engine.SetValue("log", new Action<object>(_pulse.Log.TraceInfo));
            _engine.SetValue("factories", CreateFactories());
        }

        public void ExecuteScript(string script)
        {
            _engine.Execute(script);
        }

        public void SetScreen(BaseScreen screen)
        {
            _engine.SetValue("currentScreen", screen);
        }

        public void UnsetScreen()
        {
            _engine.SetValue("currentScreen", new object());
        }
        private Dictionary<string, IFactory> CreateFactories()
        {
            var dict = new Dictionary<string, IFactory>();
            dict.Add("button", new ButtonFactory());
            dict.Add("background", new BackgroundFactory());
            dict.Add("animation", new AnimationFactory());
            return dict;
        }
    }
}
