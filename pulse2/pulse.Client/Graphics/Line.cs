using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics.Engine.Util;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics
{
    class Line : IRenderable
    {
        public Vector3 Vertex1 { get; set; }
        public Vector3 Vertex2 { get; set; }
        public bool Visible { get; set; }
        public float Depth { get; set; }
        public Color4 Colour { get; set; }
        public ShapeType Shape { get; set; }
        public Vector3 Origin { get; set; }
        public int TextureId { get; private set; }
        public float Thickness { get; set; }
        public SizeF Size { get; set; }

        public Line(Vector3 vertex1, Vector3 vertex2)
        {
            Visible = true;
            Vertex2 = vertex2;
            Vertex1 = vertex1;
            Shape = ShapeType.Cube;
        }
    }
}
