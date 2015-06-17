using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using pulse.Configuration;

namespace pulse.Client.Graphics
{
    // TODO: IoC based?
    public static class GraphicsUtil
    {
        public static float ScaleX(float f)
        {
            return f * (ConfigLoader<PulseConfig>.Instance.Width / 1024f);
        }

        public static float ScaleY(float f)
        {
            return f * (ConfigLoader<PulseConfig>.Instance.Height / 768f);
        }

        public static float ScaleInputX(float f)
        {
            return f * (1024f / ConfigLoader<PulseConfig>.Instance.Width);
        }

        public static float ScaleInputY(float f)
        {
            return f * (768f / ConfigLoader<PulseConfig>.Instance.Height);
        }
    }
}
