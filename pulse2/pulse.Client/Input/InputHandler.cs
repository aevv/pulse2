using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using pulse.Client.Input.Interface;
using KeyboardState = pulse.Client.Input.States.KeyboardState;
using MouseState = pulse.Client.Input.States.MouseState;

namespace pulse.Client.Input
{
    class InputHandler : IInputChecker
    {
        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        public InputHandler(GameWindow gameWindow)
        {
            _keyboardState = new KeyboardState();
            _mouseState = new MouseState();

            gameWindow.KeyDown += KeyDown;
            gameWindow.KeyUp += KeyUp;
            gameWindow.MouseDown += MouseDown;
            gameWindow.MouseUp += MouseUp;
            gameWindow.MouseMove += MouseMove;
        }

        public void SwapBuffers()
        {
            _keyboardState.SwapBuffers();
            _mouseState.SwapBuffers();
        }

        #region Input Events

        private void KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            _keyboardState.AddKey(e.Key);
        }

        private void KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            _keyboardState.RemoveKey(e.Key);
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseState.AddButton(e.Button);
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseState.RemoveButton(e.Button);
        }

        private void MouseMove(object sender, MouseMoveEventArgs e)
        {
            _mouseState.Position = new PointF(e.X, e.Y);
        }

        #endregion

        #region Input Utilities

        public PointF Cursor { get { return _mouseState.Position; } }

        public bool LeftClick { get { return _mouseState.Click(MouseButton.Left); } }

        public bool KeyPress(Key key)
        {
            return _keyboardState.KeyPress(key);
        }

        public bool KeyHeld(Key key)
        {
            return _keyboardState.KeyHeld(key);
        }

        public bool Click(MouseButton button)
        {
            return _mouseState.Click(button);
        }

        public bool ClickHeld(MouseButton button)
        {
            return _mouseState.ClickHeld(button);
        }

        #endregion
    }
}
