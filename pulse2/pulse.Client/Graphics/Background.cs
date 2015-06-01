using System.Drawing;

namespace pulse.Client.Graphics
{
    class Background : Renderable
    {
        public Background()
        {
            Location = new PointF(0, 0);
            Size = new SizeF(1024f, 768f);
            Depth = -7;
        }
        public Background(string texture) : this()
        {
            ApplyTexture(texture);
        }
    }
}
