using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics
{
    class RawText : IRenderable
    {
        private bool _shadow;
        private string _text;

        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public Font Font { get; set; }

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
                TextureId = TextureManager.LoadRawTextImage(_text, Font, out textSize);
                Size = textSize;
            }
        }

        public RawText(PointF location, bool shadow = false)
        {
            Location = location;
        }


        public void OnRenderFrame(FrameEventArgs args)
        {
            throw new NotImplementedException();
        }

        public int TextureId { get; private set; }
    }
}
