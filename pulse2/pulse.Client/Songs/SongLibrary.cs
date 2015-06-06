using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using pulse.Client.Audio;
using pulse.Client.IO;

namespace pulse.Client.Songs
{
    class SongLibrary
    {
        #region Singleton
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
        #endregion

        private SongLibrary()
        {
            _songs = new List<Song>();
        }

        private readonly List<Song> _songs;

        // Temporary, honest :)
        public Song GetRandomSong()
        {
            if (_songs.Count == 0)
                return null;

            var rand = new Random();
            var index = rand.Next(0, _songs.Count);
            var unpacker = new FilePacker();
            var matchingSong = _songs[index];

            var chartGroup = unpacker.DeserialisePackedFile<ChartGroup>(matchingSong.FileName);
            chartGroup.PgcName = matchingSong.FileName;

            index = rand.Next(0, chartGroup.Charts.Count);
            return new Song { Sound = AudioManager.LoadSound(chartGroup.Files.First(f => f.FileName == chartGroup.Charts[index].FileName).Data), FileName = matchingSong.FileName,
                GroupCreator = matchingSong.GroupCreator, GroupName = matchingSong.GroupName };
        }

        public ChartGroup GetGroupChart(Song song)
        {
            var unpacker = new FilePacker();

            return unpacker.DeserialisePackedFile<ChartGroup>(song.FileName);
        }

        public void Scan(string directory)
        {
            var dir = new DirectoryInfo(directory);

            if (!dir.Exists)
            {
                dir.Create();
                return;
            }

            // TODO: Removal of songs no longer present?
            // For scan at runtime, file system watcher etc.
            foreach (var file in dir.GetFiles("*.pcg"))
            {
                if (_songs.All(f => f.FileName != file.FullName))
                    _songs.Add(new Song { FileName = file.FullName });
            }
        }

        public void LoadDatabase(Action<double> progressCallback)
        {
            Scan(Constants.SongFolder);

            var filePacker = new FilePacker();
            var dbPath = string.Format("{0}\\{1}", Constants.SongFolder, Constants.SongDB);

            IEnumerable<Song> database = null;

            if (File.Exists(dbPath))
                database = filePacker.DeserialisePackedFile<IEnumerable<Song>>(dbPath);

            if (database == null)
                database = new List<Song>();

            var newSongs = _songs.Where(song => database.All(dbSong => dbSong.FileName != song.FileName)).ToList();

            for (int i = 0; i < newSongs.Count; i++)
            {
                progressCallback((double)i / (newSongs.Count + 1) * 100);

                if (!File.Exists(newSongs[i].FileName))
                    continue;

                var group = filePacker.DeserialisePackedFile<ChartGroup>(newSongs[i].FileName);

                newSongs[i].GroupCreator = group.GroupCreator;
                newSongs[i].GroupName = group.GroupName;
                _songs.Add(newSongs[i]);
            }

            SaveDatabase();
        }

        public void SaveDatabase()
        {
            new FilePacker().PackObjectToFile(_songs, string.Format("{0}\\{1}", Constants.SongFolder, Constants.SongDB));
        }
    }
}
