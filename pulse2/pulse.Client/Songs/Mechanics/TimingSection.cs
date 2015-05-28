using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Songs.Mechanics
{
    public class TimingSection
    {
        public double Offset { get; set; }

        public double Bpm
        {
            get { return 60/(SnapInterval/1000); }
        }

        public double SnapInterval { get; set; }
        public bool BpmChange { get; set; }
        public int NoteSpeed { get; set; }

        public TimingSection(double offset, double snapInterval, int noteSpeed, bool bpmChange = false)
        {
            Offset = offset;
            SnapInterval = snapInterval;
            NoteSpeed = noteSpeed;
            BpmChange = bpmChange;
        }
    }
}
