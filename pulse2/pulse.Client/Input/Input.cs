using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace pulse.Client.Input
{
    class Input
    {
        private static List<Key> keysDown;
        private static List<Key> keysDownLast;
        private static List<MouseButton> buttonsDown;
        private static List<MouseButton> buttonsDownLast;

        public static void Initialise(GameWindow game)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            buttonsDown = new List<MouseButton>();
            buttonsDownLast = new List<MouseButton>();

            game.KeyDown += game_KeyDown;
            game.KeyUp += game_KeyUp;
            game.MouseDown += game_MouseDown;
            game.MouseUp += game_MouseUp;
        }

        private static void game_KeyUp(object sender, KeyboardKeyEventArgs e) {
            while (keysDown.Contains(e.Key))
                keysDown.Remove(e.Key);
        }

        private static void game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonsDown.Add(e.Button);
        }

        private static void game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            while (buttonsDown.Contains(e.Button))
                buttonsDown.Remove(e.Button);
        }

        static void game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
            Console.WriteLine(DateTime.Now.Ticks + e.Key.ToString());
        }

        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonsDownLast = new List<MouseButton>(buttonsDown);
        }

        public static bool KeyPress(Key k)
        {
            var current = keysDown.Contains(k);
            var previous = !keysDownLast.Contains(k);
            if (current)
            {
                if(keysDown == keysDownLast)
                    Console.WriteLine("SadFace"); 
            }
            return current && previous;
            //return (keysDown.Contains(k) && !keysDownLast.Contains(k));
        }
        public static bool KeyRelease(Key k)
        {
            return (!keysDown.Contains(k) && keysDownLast.Contains(k));
        }
        public static bool KeyDown(Key k)
        {
            return (keysDown.Contains(k));
        }

        public static bool MousePress(MouseButton b)
        {
            return (buttonsDown.Contains(b) && !buttonsDownLast.Contains(b));
        }
        public static bool MouseRelease(MouseButton b)
        {
            return (!buttonsDown.Contains(b) && buttonsDownLast.Contains(b));
        }
        public static bool MouseDown(MouseButton b)
        {
            return (buttonsDown.Contains(b));
        }
    }
}
