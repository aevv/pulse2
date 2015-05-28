using pulse.Client.Songs.Mechanics;
using System.Collections.Generic;

namespace pulse.Client.Songs
{
    public class Chart
    {
        public string Creator { get; set; }
        public string Difficulty { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string FileName { get; set; }

        public double LeadInTime { get; set; }
        public string BackgroundName { get; set; }
        public double Preview { get; set; }

        public List<Note> Notes { get; private set; }
        public List<TimingSection> TimingSections { get; private set; }
        public List<Bookmark> Bookmarks { get; private set; } 

        public Chart()
    {
            Notes = new List<Note>();
            TimingSections = new List<TimingSection>();
            Bookmarks = new List<Bookmark>();
        }
    }
}
