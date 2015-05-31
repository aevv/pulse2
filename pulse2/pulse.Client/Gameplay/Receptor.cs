using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Graphics;
using pulse.Client.Input.Events;

namespace pulse.Client.Gameplay
{
    class Receptor :  Quad
    {
        public RectangleF Boundaries { get; set; }

        public Receptor(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public Receptor(PointF location, SizeF size) : base(location, size)
        {
            Boundaries = new RectangleF(location, size);
        }
    }
}
