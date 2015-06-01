using pulse.Client.Audio;

namespace pulse.Client.Songs
{
    public class Song
    {
        public string FileName { get; set; }
        public string GroupName { get; set; }
        public string GroupCreator { get; set; }

        public Sound Sound { get; set; }
    }
}
