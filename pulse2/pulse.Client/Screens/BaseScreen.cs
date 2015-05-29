using System.Collections.Generic;
using System.Drawing;
using OpenTK;
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
        private SizeF ScreenSize { get; set; }

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

        public BaseScreen(InputHandler inputHandler, SizeF screenSize)
        {
            _inputHandler = inputHandler;
            _renderables = new List<Renderable>();
            ScreenSize = screenSize;
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
