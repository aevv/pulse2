using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics.Interface;
using pulse.Configuration;

namespace pulse.Client.Graphics
{
    class RawText : IRenderable
    {
        private string _text;
        private int _textureId;

        public bool Visible { get; set; }
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public Font Font { get; set; }
        public bool Shadow { get; set; }
        public float Depth { get; set; }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                SizeF textSize;
                _textureId = TextureManager.LoadRawTextImage(_text, Font, out textSize);
                Size = textSize;
            }
        }

        public RawText(string text, bool shadow = false) : this(text, new PointF(0, 0), shadow)
        {
            
        }

        public RawText(string text, PointF location, bool shadow = false)
        {
            Font = ConfigLoader<PulseConfig>.Instance.Font;
            Text = text;
            Location = location;
            Colour = Color4.White;
            Shadow = shadow;
        }


        public void OnRenderFrame(FrameEventArgs args)
        {
            GL.PushMatrix();

            GL.BindTexture(TextureTarget.Texture2D, _textureId);
            GL.LineWidth(2f);

            float scaledX = GraphicsUtil.ScaleX(Location.X);
            float scaledY = GraphicsUtil.ScaleY(Location.Y);
            float scaledWidth = GraphicsUtil.ScaleX(Size.Width);
            float scaledHeight = GraphicsUtil.ScaleY(Size.Height);

            if (Shadow)
            {
                // 1 Pixel down and right for shadow
                GL.Color4(Color.Black);
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 0);
                GL.Vertex2(scaledX + 1, scaledY + 1);
                GL.TexCoord2(1, 0);
                GL.Vertex2(scaledX + 1 + scaledWidth, scaledY + 1);
                GL.TexCoord2(1, 1);
                GL.Vertex2(scaledX + 1 + scaledWidth, scaledY + 1 + scaledHeight);
                GL.TexCoord2(0, 1);
                GL.Vertex2(scaledX + 1, scaledY + 1 + scaledHeight);
                GL.End();
            }

            GL.Color4(Colour);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(scaledX, scaledY);
            GL.TexCoord2(1, 0);
            GL.Vertex2(scaledX + scaledWidth, scaledY);
            GL.TexCoord2(1, 1);
            GL.Vertex2(scaledX + scaledWidth, scaledY + scaledHeight);
            GL.TexCoord2(0, 1);
            GL.Vertex2(scaledX, scaledY + scaledHeight);
            GL.End();

            GL.PopMatrix();
        }
    }
}
