using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Configuration;

namespace pulse.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigLoader<PulseConfig>("config.ini").Load();
            new Game(config).Run(120, 120);
        }
    }
}
