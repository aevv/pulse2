using pulse.Client.Screens.Forms;
using pulse.Client.Songs;
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
            var loadSplash = new LoadingSplashForm(new PulseLoader());
            loadSplash.ShowDialog();

            //new Game(config).Run(120, 120);
            new GameMaterial().Run(120, 120);
        }
    }
}
