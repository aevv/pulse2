using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.ES10;

namespace pulse.Client.Graphics
{
    class Background : Renderable
    {
        public Background(string texture, SizeF screenSize)
        {
            Location = new PointF(0, 0);
            Size = new SizeF(screenSize);
            ApplyTexture(texture);
        }
    }
}
