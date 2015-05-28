using System.Drawing;

namespace pulse.Client
{
    class PulseConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Vsync { get; set; }
        public bool Fullscreen { get; set; }
        public Font Font { get; set; }

        public PulseConfig()
        {
            //Default values.
            Width = 1024;
            Height = 768;
            Font = new Font("Roboto", 20);
        }
    }
}
