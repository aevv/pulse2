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
            _songs = new List<Song>();
        }

        private List<Song> _songs; 
        private bool _initialLoad;

        public Sound GetRandomSong()
        {
            if (_songs.Count == 0)
                return null;

            var rand = new Random();
            var index = rand.Next(0, _songs.Count);
            var chartGroup = ProcessPcgFile(_songs[index].FileName);
            chartGroup.PgcName = _songs[index].FileName;
            _songs[index].GroupName = chartGroup.GroupName;
            _songs[index].GroupCreator = chartGroup.GroupCreator;
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
                if (_songs.All(f => f.FileName != file.FullName))
                    _songs.Add(new Song(){FileName = file.FullName});
            }
        }

        // TODO: Refactor these out to reusable classes.

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
