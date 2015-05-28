using System.Drawing;

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
