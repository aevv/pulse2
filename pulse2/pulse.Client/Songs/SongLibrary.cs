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
            _chartsGroups = new List<ChartGroup>();
            _songMetas = new List<SongMeta>();
        }

        private List<ChartGroup> _chartsGroups;
        private List<SongMeta> _songMetas; 
        private bool _initialLoad;

        public Sound GetRandomSong()
        {
            if (_songMetas.Count == 0)
                return null;

            var rand = new Random();
            var index = rand.Next(0, _songMetas.Count);
            var chartGroup = ProcessPcgFile(_songMetas[index].FileName);
            index = rand.Next(0, chartGroup.Charts.Count);
            return AudioManager.LoadSound(chartGroup.Files.First(f => f.FileName == chartGroup.Charts[index].FileName).Data);
        }

        public void Scan(string directory)
        {
            var dir = new DirectoryInfo(directory);

            if (!dir.Exists)
            {
                dir.Create();
                return;
            }

            foreach (var file in dir.GetFiles("*.pcg"))
            {
                if (!_songMetas.Any(f => f.FileName == file.FullName))
                    _songMetas.Add(new SongMeta(){FileName = file.FullName});
            }
        }

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
                var chartGroup = ProcessPcgFile(file.FullName);

                if (chartGroup != null)
                    _chartsGroups.Add(chartGroup);
            }
        }

        // TODO: Refactor these out to reusable classes.
        // TODO: Song library fast load from a flat database.
        // Current implementation reads entire files into memory. This scales horribly. Clever solution needs to look at file names,
        // read new files one at a time to not flood RAM and serialise meta data to some local database. Then use this database of
        // files to navigate, and load a file into memory once it is required, unloading once it's no longer used (or after some specified time).

        private ChartGroup ProcessPcgFile(string fileName)
        {
            Console.WriteLine("Loading PCG {0}", fileName);
            return JsonConvert.DeserializeObject<ChartGroup>(Encoding.UTF8.GetString(Decompress(File.ReadAllBytes(fileName))));
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
