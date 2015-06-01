using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics
{
    class Line : IRenderable
    {
        public PointF Vertex1 { get; set; }
        public PointF Vertex2 { get; set; }
        public bool Visible { get; set; }
        public float Depth { get; set; }
        public Color4 Colour { get; set; }
        public float Thickness { get; set; }

        public Line(PointF vertex1, PointF vertex2)
        {
            Visible = true;
            Vertex2 = vertex2;
            Vertex1 = vertex1;
        }
        public void OnRenderFrame(FrameEventArgs args)
        {
            GL.PushMatrix();

            GL.Disable(EnableCap.Texture2D);
            GL.LineWidth(Thickness);
            GL.Color4(Colour);

            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(Vertex1.X, Vertex1.Y);
            GL.Vertex2(Vertex2.X, Vertex2.Y);

            GL.End();

            GL.Enable(EnableCap.Texture2D);

            GL.PopMatrix();
        }
    }
}
