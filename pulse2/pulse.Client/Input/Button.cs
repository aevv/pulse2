using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Graphics;
using pulse.Client.Graphics.Interface;
using pulse.Client.Input.Events;
using pulse.Client.Input.Interface;

namespace pulse.Client.Input
{
    class Button : Renderable, IClickable
    {
        private Action _clickEvent;
        private readonly RawText _text;
        public RectangleF Boundaries { get; set; }
        public event ClickEventHandler OnClick;
        public Color4 TextColour { get { return _text.Colour; } set { _text.Colour = value; } }

        public Button(PointF location, SizeF size, string text, float depth = 0.0f)
        {

            Location = location;
            Size = size;
            Boundaries = new RectangleF(location, size);
            _text = new RawText(text, true);
            _text.Location = new PointF(Location.X + (Size.Width/4), Location.Y + Size.Height/8);
            Depth = depth;
            _text.Depth = depth + 0.1f;
            // TODO: Config this
            ApplyTexture("Assets\\button.png");
        }

        public Button(float x, float y, float width, float height, string text, float depth = 0.0f) : this(new PointF(x, y), new SizeF(width, height), text, depth)
        {
        }

        public bool IsMouseOver(float x, float y)
        {
            return IsMouseOver(new PointF(x, y));
        }

        public bool IsMouseOver(PointF point)
        {
            return Boundaries.Contains(point);
        }

        public void Click()
        {
            if (OnClick != null)
                OnClick();
        }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            _text.OnRenderFrame(args);
        }

        public void OnUpdateFrame(UpdateFrameEventArgs args)
        {
        }
    }
}
