using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace pulse.Client.Graphics.Engine
{
    interface IRenderer
    {
        void Initialise();
        void OnRenderFrame(FrameEventArgs args);
    }
}
