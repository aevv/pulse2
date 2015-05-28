﻿using pulse.Client.Songs;
using pulse.Configuration;

namespace pulse.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load ini config. Avoid using app.config for time being, keep simplicity.
            var config = ConfigLoader<PulseConfig>.Resolve("config.ini").Load();

            // Load song library prior to anything being shown.
            // TODO: Splash screen while loading occurs.
            var library = SongLibrary.Instance;
            library.Load(Constants.SongFolder);
            new Game(config).Run(120, 120);
        }
    }
}
