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
            
        }
    }
}
