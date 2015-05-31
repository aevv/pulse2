using System.Drawing;
using OpenTK.Input;

namespace pulse.Client.Input.Interface
{
    interface IInputChecker
    {
        bool LeftClick { get; }
        PointF Cursor { get; }
        PointF UnscaledCursor { get; }
        bool Click(MouseButton button);
        bool ClickHeld(MouseButton button);
        bool KeyPress(Key key);
        bool KeyHeld(Key key);
    }
}
