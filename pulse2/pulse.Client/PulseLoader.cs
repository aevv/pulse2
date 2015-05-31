using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using pulse.Client.Songs;

namespace pulse.Client
{
    public class PulseLoader
    {
        public void LoadStaticContent()
        {
            var library = SongLibrary.Instance;
            library.Scan(Constants.SongFolder);
        }
    }
}
