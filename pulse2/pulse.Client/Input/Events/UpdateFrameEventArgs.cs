using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using pulse.Client.Input.Interface;

namespace pulse.Client.Input.Events
{
    class UpdateFrameEventArgs
    {
        public FrameEventArgs FrameEventArgs { get; private set; }
        public IInputChecker InputChecker { get; private set; }

        public double Time { get { return FrameEventArgs.Time; } }

        public UpdateFrameEventArgs(FrameEventArgs args, IInputChecker checker)
        {
            FrameEventArgs = args;
            InputChecker = checker;
        }
    }
}
