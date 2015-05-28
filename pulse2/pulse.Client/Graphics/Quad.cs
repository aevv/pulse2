using System.Drawing;

namespace pulse.Client.Graphics
{
    class Quad : Renderable
    {
        public Quad(float x, float y, float width, float height)
        {
            Location = new PointF(x, y);
            Size = new SizeF(width, height);
        }
    }
}
