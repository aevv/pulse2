    using System.Drawing;
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using pulse.Client.Graphics.Engine.Util;
    using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics
{
    abstract class Renderable : IRenderable
    {
        public SizeF Size { get; set; }
        public int TextureId { get; private set; }
        public string TexturePath { get; private set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public ShapeType Shape { get; set; }
        public Vector3 Origin { get; set; }
        public float Depth { get; set; }

        protected Renderable()
        {
            TextureId = -1;
            Shape = ShapeType.Cube;
        }

        public void ApplyTexture(string path)
        {
            var textureId = TextureManager.LoadImage(path);
            if (textureId == -1)
                return;
            
            TextureId = textureId;
            TexturePath = path;
        }

        public void ApplyTexture(int textureId)
        {
            if (textureId < 0)
                return;
            // This could go very badly.
            TextureId = textureId;
            TexturePath = string.Empty;
        }
    }
}
