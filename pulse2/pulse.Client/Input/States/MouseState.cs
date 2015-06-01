using System.Collections.Generic;
using System.Drawing;
using OpenTK.Input;

namespace pulse.Client.Input.States
{
    class MouseState
    {
        private readonly List<MouseButton> _oldButtonsDown;
        private readonly List<MouseButton> _newButtonsDown;

        public PointF Position { get; set; }

        public MouseState()
        {
            _oldButtonsDown = new List<MouseButton>();
            _newButtonsDown = new List<MouseButton>();
        }

        public void SwapBuffers()
        {
            _oldButtonsDown.Clear();
            _oldButtonsDown.AddRange(_newButtonsDown);
        }

        public void AddButton(MouseButton button)
        {
            if (_newButtonsDown.Contains(button))
                return;

            _newButtonsDown.Add(button);
        }

        public void RemoveButton(MouseButton button)
        {
            if (!_newButtonsDown.Contains(button))
                return;

            _newButtonsDown.Remove(button);
        }

        public bool Click(MouseButton button)
        {
            return _newButtonsDown.Contains(button) && !_oldButtonsDown.Contains(button);
        }

        public bool ClickHeld(MouseButton button)
        {
            return _newButtonsDown.Contains(button) && _oldButtonsDown.Contains(button);
        }
    }
}
