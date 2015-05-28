using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Songs.Mechanics
{
    public class Bookmark
    {
        public double Offset { get; set; }

        public Bookmark(double offset)
        {
            Offset = offset;
        }
    }
}
