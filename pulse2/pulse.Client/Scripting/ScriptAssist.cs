using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Input;
using pulse.Client.Logging;
using pulse.Client.Screens;
using pulse.Client.Songs;

namespace pulse.Client.Scripting
{
    class ScriptAssist
    {
        private readonly LogTracer _log;
        private readonly InputHandler _input;
        private readonly ScreenManager _screenManager;
        private readonly PulseConfig _config;
        private readonly MediaPlayer _media;
        private readonly Game _game;

        public LogTracer Log
        {
            get { return _log; }
        }

        public InputHandler Input
        {
            get { return _input; }
        }

        public ScreenManager ScreenManager
        {
            get { return _screenManager; }
        }

        public PulseConfig Config
        {
            get { return _config; }
        }

        public MediaPlayer Media
        {
            get { return _media; }
        }

        public Game Game
        {
            get { return _game; }
        }

        public ScriptAssist(LogTracer log, InputHandler inputHandler, ScreenManager screenManager, PulseConfig config, MediaPlayer mediaPlayer, Game game)
        {
            _log = log;
            _input = inputHandler;
            _screenManager = screenManager;
            _config = config;
            _media = mediaPlayer;
            _game = game;
        }
    }
}
