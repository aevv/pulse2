using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using pulse.Client.Graphics;
using pulse.Client.Graphics.Interface;
using pulse.Client.Input;
using pulse.Client.Input.Events;
using pulse.Client.Input.Interface;

namespace pulse.Client.Screens
{
    abstract class BaseScreen : IUpdateable
    {
        private string _name;
        private string _title;
        private readonly List<IRenderable> _renderables;
        private readonly List<IUpdateable> _updateables;
        private readonly InputHandler _inputHandler;

        protected SizeF ScreenSize { get; set; }

        protected IInputChecker InputChecker { get { return _inputHandler; }}

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public string Title { get { return _title; } set { _title = value; } }

        protected List<IRenderable> Renderables
        {
            get { return _renderables; }
        }

        protected List<IUpdateable> Updateables
        {
            get { return _updateables; }
        } 

        public BaseScreen(InputHandler inputHandler) : this(inputHandler, new SizeF(0, 0))
        {
        }

        public BaseScreen(InputHandler inputHandler, SizeF screenSize)
        {
            _inputHandler = inputHandler;
            _renderables = new List<IRenderable>();
            _updateables = new List<IUpdateable>();
            ScreenSize = screenSize;
        }

        public virtual void OnRenderFrame(FrameEventArgs args)
        {
            foreach (var renderable in _renderables)
            {
                renderable.OnRenderFrame(args);
            }
        }

        public virtual void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            foreach (var updateable in _updateables)
            {
                updateable.OnUpdateFrame(args);

                if (updateable is IClickable)
                {
                    // TODO: Make this depth based to avoid click throughs
                    var clickable = updateable as IClickable;
                    if (clickable.IsMouseOver(args.InputChecker.Cursor) && args.InputChecker.LeftClick)
                        clickable.Click();
                    
                }
            }
        }
    }
}
