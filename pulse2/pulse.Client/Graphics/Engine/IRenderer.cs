using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using pulse.Client.Screens;

namespace pulse.Client.Graphics.Engine
{
    interface IRenderer
    {
        void Initialise();
        void Resize(int width, int height);
        void OnRenderFrame(FrameEventArgs args, BaseScreen screen);
    }
}
