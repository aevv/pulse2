using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using pulse.Client.Audio;

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
            _charts = new List<ChartGroup>();
        }

        public Sound GetRandomSong()
        {
            if (_charts.Count == 0)
                return null;

            var rand = new Random();
            var index = rand.Next(0, _charts.Count);
            var group = _charts[index];
            index = rand.Next(0, group.Charts.Count);
            return AudioManager.LoadSound(group.Files.First(f => f.FileName == group.Charts[index].FileName).Data);
        }

        private List<ChartGroup> _charts;
        private bool _initialLoad;

        public void Load(string directory)
        {
            if (_initialLoad)
                return;

            var dir = new DirectoryInfo(directory);

            if (!dir.Exists)
                dir.Create();

            ProcessSongDirectory(dir);

            _initialLoad = true;
        }

        private void ProcessSongDirectory(DirectoryInfo dir)
        {
            foreach (var file in dir.GetFiles("*.pcg"))
            {
                var chartGroup = ProcessPcgFile(file);

                if (chartGroup != null)
                    _charts.Add(chartGroup);
            }
        }

        // TODO: Refactor these out to reusable classes.
        // TODO: Song library fast load from a flat database.
        // Current implementation reads entire files into memory. This scales horribly. Clever solution needs to look at file names,
        // read new files one at a time to not flood RAM and serialise meta data to some local database. Then use this database of
        // files to navigate, and load a file into memory once it is required, unloading once it's no longer used (or after some specified time).

        private ChartGroup ProcessPcgFile(FileInfo file)
        {
            Console.WriteLine("Loading PCG {0}", file.FullName);
            return JsonConvert.DeserializeObject<ChartGroup>(Encoding.UTF8.GetString(Decompress(File.ReadAllBytes(file.FullName))));
        }

        private byte[] Decompress(byte[] toDecompress)
        {
            using (var stream = new MemoryStream(toDecompress))
            {
                using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var result = new MemoryStream())
                    {
                        gzip.CopyTo(result);
                        return result.ToArray();
                    }
                }
            }
        }
    }
}
