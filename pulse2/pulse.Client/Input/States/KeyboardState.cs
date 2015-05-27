using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace pulse.Client.Input.States
{
    class KeyboardState
    {
        private readonly List<Key> _oldKeysDown;
        private readonly List<Key> _newKeysDown;

        public KeyboardState()
        {
            _oldKeysDown = new List<Key>();
            _newKeysDown = new List<Key>();
        }

        public void SwapBuffers()
        {
            _oldKeysDown.Clear();
            _oldKeysDown.AddRange(_newKeysDown);
        }

        public void AddKey(Key key)
        {
            if (_newKeysDown.Contains(key))
                return;

            _newKeysDown.Add(key);
        }

        public void RemoveKey(Key key)
        {
            if (!_newKeysDown.Contains(key))
                return;

            _newKeysDown.Remove(key);
        }

        public bool KeyPress(Key key)
        {
            return _newKeysDown.Contains(key) && !_oldKeysDown.Contains(key);
        }

        public bool KeyHeld(Key key)
        {
            return _newKeysDown.Contains(key) && _oldKeysDown.Contains(key);
        }
    }
}
