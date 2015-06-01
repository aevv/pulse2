    using System.Drawing;
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics
{
    abstract class Renderable : IRenderable, ITexturable
    {
        public bool Visible { get; set; }
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public int TextureId { get; private set; }
        public string TexturePath { get; private set; }
        public float Rotation { get; set; }
        public PointF DrawOffset { get; set; }
        public Color4 Colour { get; set; }
        public float Depth { get; set; }

        protected Renderable()
        {
            TextureId = -1;
            Colour = Color4.White;
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

        public virtual void OnRenderFrame(FrameEventArgs args)
        {
            GL.PushMatrix();

            GL.Color4(Colour);

            var scaledX = GraphicsUtil.ScaleX(Location.X);
            var scaledY = GraphicsUtil.ScaleY(Location.Y);

            var scaledWidth = GraphicsUtil.ScaleX(Size.Width);
            var scaledHeight = GraphicsUtil.ScaleY(Size.Height);

            if (TextureId == -1)
            {
                GL.Disable(EnableCap.Texture2D);

                GL.Begin(PrimitiveType.Quads);
                GL.Color4(Color4.White);
                GL.Vertex2(scaledX, scaledY);
                GL.Vertex2(scaledX + scaledWidth, scaledY);
                GL.Vertex2(scaledX + scaledWidth, scaledY + scaledHeight);
                GL.Vertex2(scaledX, scaledY + scaledHeight);
                GL.End();

                GL.Enable(EnableCap.Texture2D);
            }

            else
            {
                // TODO: Apply draw offset to draw parts of texture sheets
                // TODO: Optimise texture rebinding?
                GL.BindTexture(TextureTarget.Texture2D, TextureId);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 0);
                GL.Vertex3(scaledX, scaledY, Depth);
               
                GL.TexCoord2(1, 0);
                GL.Vertex3(scaledX + scaledWidth, scaledY, Depth);

                GL.TexCoord2(1, 1);
                GL.Vertex3(scaledX + scaledWidth, scaledY + scaledHeight, Depth);

                GL.TexCoord2(0, 1);
                GL.Vertex3(scaledX, scaledY + scaledHeight, Depth);


                GL.End();
            }
            GL.PopMatrix();
        }

    }
}
