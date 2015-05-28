using System;
using System.Drawing;
using OpenTK;
using OpenTK.Platform.Windows;
using pulse.Client.Graphics;

namespace pulse.Client.Input
{
    class Button : Renderable, IClickable
    {
        private Action _clickEvent;
        private readonly RawText _text;
        public RectangleF Boundaries { get; set; }
        public Action ClickEvent { set { _clickEvent = value; } }

        public Button(float x, float y, float width, float height, string text)
        {
            Location = new PointF(x, y);
            Size = new SizeF(width, height);
            Boundaries = new RectangleF(x, y, width, height);
            _text = new RawText(text, new PointF(Location.X + (Size.Width/4), Location.Y + Size.Height/8));
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
            _clickEvent();
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _text.OnRenderFrame(e);
        }

    }
}
