using System.Drawing;
using OpenTK;

namespace pulse.Client.Graphics
{
    class Quad : Renderable
    {
        public Quad(float x, float y, float z, float width, float height) : this(new Vector3(x, y, z), new SizeF(width, height))
        {
            
        }

        public Quad(Vector3 point, SizeF size)
        {
            Origin = point;
            Size = size;
        }
    }
}
