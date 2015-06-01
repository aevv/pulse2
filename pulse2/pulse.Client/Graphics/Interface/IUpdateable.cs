using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using pulse.Client.Input.Events;

namespace pulse.Client.Graphics.Interface
{
    interface IUpdateable
    {
        void OnUpdateFrame(UpdateFrameEventArgs args);
    }
}
