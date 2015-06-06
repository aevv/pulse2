using System;
using System.Drawing;
using System.Drawing.Text;

namespace pulse.Client
{
    class PulseConfig
    {
        private readonly PrivateFontCollection _fontCollection;
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
            _fontCollection = new PrivateFontCollection();
            // TODO: Maybe use marshal
            unsafe
            {
                fixed (byte* pointer = DefaultAssets.Roboto_Regular)
                {
                    _fontCollection.AddMemoryFont((IntPtr)pointer, DefaultAssets.Roboto_Regular.Length);
                }
            }
            Font = new Font(_fontCollection.Families[0], 20);
        }
    }
}
