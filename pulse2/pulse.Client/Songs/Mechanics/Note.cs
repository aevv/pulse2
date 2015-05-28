using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace pulse.Client.Songs.Mechanics
{
    public class Note
    {
        public int Lane { get; set; }
        public double Offset { get; set; }
        public double HoldOffset { get; set; }

        public Note(double offset, double holdOffset, int lane)
        {
            Offset = offset;
            HoldOffset = holdOffset;
            Lane = lane;
        }
    }
}
