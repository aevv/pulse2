using System.Drawing;

namespace pulse.Client.Input
{
    interface IClickable
    {
        RectangleF Boundaries { get; set; }

        bool IsMouseOver(float x, float y);

        void Click();
    }
}
