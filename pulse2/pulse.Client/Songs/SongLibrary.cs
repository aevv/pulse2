using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Songs
{
    class SongLibrary
    {
        private static SongLibrary _instance;
        private static object _sync = new object();

        public static SongLibrary Instance
        {
            get
            {
                lock (_sync)
                    return _instance ?? (_instance = new SongLibrary());
            }
        }

        private SongLibrary()
        {
            
        }

        public void Load(string directory)
        {
            var dir = new DirectoryInfo(directory);

            if (!dir.Exists)
                dir.Create();

            ProcessSongDirectory(dir);
        }

        private void ProcessSongDirectory(DirectoryInfo dir)
        {
            foreach (var subDir in dir.GetDirectories())
            {
                ProcessSongDirectory(subDir);
            }

        }
    }
}
