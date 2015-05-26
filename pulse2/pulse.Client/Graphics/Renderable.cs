    using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace pulse.Client.Graphics
{
    abstract class Renderable
    {
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public int Texture { get; set; }
        public float Rotation { get; set; }
        public PointF DrawOffset { get; set; }
        public Color4 Colour { get; set; }

        public Renderable()
        {
            Texture = -1;
            Colour = Color4.White;
        }

        public void OnRenderFrame(FrameEventArgs e)
        {
            GL.PushMatrix();

            GL.Color4(Colour);

            if (Texture == -1)
            {
                GL.Disable(EnableCap.Texture2D);

                GL.Begin(BeginMode.Quads);
                GL.Color4(Color4.White);
                GL.Vertex2(Location.X, Location.Y);
                GL.Vertex2(Location.X + Size.Width, Location.Y);
                GL.Vertex2(Location.X + Size.Width, Location.Y + Size.Height);
                GL.Vertex2(Location.X, Location.Y + Size.Height);
                GL.End();

                GL.Enable(EnableCap.Texture2D);
            }

            else
            {
                GL.BindTexture(TextureTarget.Texture2D, Texture);
                GL.Begin(BeginMode.Quads);

                GL.TexCoord2(0, 0);
                GL.Vertex2(Location.X, Location.Y);
               
                GL.TexCoord2(1, 0);
                GL.Vertex2(Location.X + Size.Width, Location.Y);

                GL.TexCoord2(1, 1);
                GL.Vertex2(Location.X + Size.Width, Location.Y + Size.Height);

                GL.TexCoord2(0, 1);
                GL.Vertex2(Location.X, Location.Y + Size.Height);


                GL.End();
            }
            GL.PopMatrix();
        }

    }
}
