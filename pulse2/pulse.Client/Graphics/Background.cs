using System.Drawing;
using OpenTK;

namespace pulse.Client.Graphics
{
    class Background : Renderable
    {
        public Background()
        {
            Origin = new Vector3(0, 0, -9f);
            Size = new SizeF(1024f, 768f);
            Depth = -7;
        }
        public Background(string texture) : this()
        {
            ApplyTexture(texture);
        }
    }
}
