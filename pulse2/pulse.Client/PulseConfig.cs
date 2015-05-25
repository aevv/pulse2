using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client
{
    class PulseConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Vsync { get; set; }
        public bool Fullscreen { get; set; }

        public PulseConfig()
        {
            //Default values.
            Width = 1024;
            Height = 768;
        }
    }
}
