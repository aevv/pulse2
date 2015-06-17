using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics.Engine.Util;
using pulse.Client.Graphics.Interface;
using pulse.Configuration;

namespace pulse.Client.Graphics
{
    class RawText : IRenderable
    {
        private string _text;
        private int _textureId;

        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public ShapeType Shape { get; set; }
        public Vector3 Origin { get; set; }
        public int TextureId { get; private set; }
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

        public RawText(string text, bool shadow = false) : this(text, new Vector3(0, 0, 0), shadow)
        {
            
        }

        public RawText(string text, Vector3 origin, bool shadow = false)
        {
            Origin = origin;
            Font = ConfigLoader<PulseConfig>.Instance.Font;
            Text = text;
            Colour = Color4.White;
            Shadow = shadow;
            Shape = ShapeType.Cube;
        }
    }
}
