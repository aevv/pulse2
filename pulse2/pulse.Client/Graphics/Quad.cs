using System.Drawing;

namespace pulse.Client.Graphics
{
    class Quad : Renderable
    {
        public Quad(float x, float y, float width, float height) : this(new PointF(x, y), new SizeF(width, height))
        {
        }

        public Quad(PointF location, SizeF size)
        {
            Location = location;
            Size = size;
        }
    }
}
