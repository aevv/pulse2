using System.Drawing;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Input
{
    interface IClickable : IUpdateable
    {
        RectangleF Boundaries { get; set; }
        bool IsMouseOver(float x, float y);
        bool IsMouseOver(PointF point);
        void Click();
    }
}
