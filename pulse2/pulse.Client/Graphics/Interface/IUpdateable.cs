using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace pulse.Client.Graphics.Interface
{
    interface IUpdateable
    {
        void OnUpdateFrame(FrameEventArgs args);
    }
}
