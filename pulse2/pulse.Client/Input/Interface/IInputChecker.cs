using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace pulse.Client.Input.Interface
{
    interface IInputChecker
    {
        bool LeftClick { get; }
        PointF Cursor { get; }
        bool Click(MouseButton button);
        bool ClickHeld(MouseButton button);
        bool KeyPress(Key key);
        bool KeyHeld(Key key);
    }
}
