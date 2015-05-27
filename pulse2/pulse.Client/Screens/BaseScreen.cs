using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using pulse.Client.Graphics;
using pulse.Client.Input;
using pulse.Client.Input.Interface;

namespace pulse.Client.Screens
{
    abstract class BaseScreen
    {
        private string _name;
        private string _title;
        private readonly List<Renderable> _renderables;
        private KeyboardState current, previous;

        private readonly InputHandler _inputHandler;

        protected IInputChecker InputChecker { get { return _inputHandler; }}

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public string Title { get; set; }

        protected List<Renderable> Renderables
        {
            get { return _renderables; }
        } 

        public BaseScreen(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _renderables = new List<Renderable>();
        }

        public virtual void OnRenderFrame(FrameEventArgs e)
        {
            foreach (var renderable in _renderables)
            {
                renderable.OnRenderFrame(e);
            }
        }

        public virtual void OnUpdateFrame(FrameEventArgs e)
        {
        }
    }
}
