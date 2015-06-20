using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client
{
    static class Extensions
    {
        public static float GetAspectRatio(this Size size)
        {
            return (float) size.Width/(float) size.Height;
        }
    }
}
