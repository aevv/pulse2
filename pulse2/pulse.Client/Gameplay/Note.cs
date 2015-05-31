using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Graphics;

namespace pulse.Client.Gameplay
{
    class Note : Quad
    {
        public Note(float x, float y, float width, float height) : base(x, y, width, height)
        {

        }

        public Note(PointF location, SizeF size) : base(location, size)
        {

        }
    }
}
