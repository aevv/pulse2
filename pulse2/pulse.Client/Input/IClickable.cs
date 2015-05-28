using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Input
{
    interface IClickable
    {
        RectangleF Boundaries { get; set; }

        bool IsMouseOver(float x, float y);

        void Click();
    }
}
