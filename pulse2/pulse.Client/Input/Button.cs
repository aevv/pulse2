using System;
using System.Drawing;
using pulse.Client.Graphics;

namespace pulse.Client.Input
{
    class Button : Renderable, IClickable
    {
        private Action _clickEvent;
        public RectangleF Boundaries { get; set; }
        public Action ClickEvent { set { _clickEvent = value; } }

        public Button(float x, float y, float width, float height)
        {
            Location = new PointF(x, y);
            Size = new SizeF(width, height);
            Boundaries = new RectangleF(x, y, width, height);
        }

        public bool CanClick(float x, float y)
        {
            return CanClick(new PointF(x, y));
        }

        public bool CanClick(PointF point)
        {
            return Boundaries.Contains(point);
        }

        public void Click()
        {
            _clickEvent();
        }
    }
}
