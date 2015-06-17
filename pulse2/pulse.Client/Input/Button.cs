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

        public Button(Vector3 point, SizeF size, string text)
        {
            Origin = point;
            Size = size;
            Boundaries = new RectangleF(new PointF(Origin.X, Origin.Y), size);
            _text = new RawText(text, true);
            _text.Origin = new Vector3(Origin.X + (Size.Width / 4), Origin.Y + Size.Height / 8, Origin.Z + 0.1f);
            // TODO: Config this
            ApplyTexture("Assets\\button.png");
        }

        public Button(float x, float y, float z, float width, float height, string text) : this(new Vector3(x, y, z), new SizeF(width, height), text)
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

        public void OnUpdateFrame(UpdateFrameEventArgs args)
        {
        }
    }
}
