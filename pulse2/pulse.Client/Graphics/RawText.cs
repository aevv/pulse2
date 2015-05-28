﻿using System.Drawing;
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

        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public Font Font { get; set; }
        public bool Shadow { get; set; }

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

            float x = Location.X;
            float y = Location.Y;
            float w = Size.Width;
            float h = Size.Height;

            if (Shadow)
            {
                // 1 Pixel down and right for shadow
                GL.Color4(Color.Black);
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0, 0);
                GL.Vertex2(x + 1, y + 1);
                GL.TexCoord2(1, 0);
                GL.Vertex2(x + 1 + Size.Width, y + 1);
                GL.TexCoord2(1, 1);
                GL.Vertex2(x + 1 + Size.Width, y + 1 + Size.Height);
                GL.TexCoord2(0, 1);
                GL.Vertex2(x + 1, y + 1 + Size.Height);
                GL.End();
            }

            GL.Color4(Colour);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(x, y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(x + w, y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(x + w, y + h);
            GL.TexCoord2(0, 1);
            GL.Vertex2(x, y + h);
            GL.End();

            GL.PopMatrix();
        }
    }
}