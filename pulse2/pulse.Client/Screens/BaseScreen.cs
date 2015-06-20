using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private readonly ConcurrentBag<IUpdateable> _updateables;
        private readonly InputHandler _inputHandler;

        protected IInputChecker InputChecker { get { return _inputHandler; }}

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public string Title { get { return _title; } set { _title = value; } }

        public List<IRenderable> Renderables
        {
            get { return _renderables; }
        }

        public ConcurrentBag<IUpdateable> Updateables
        {
            get { return _updateables; }
        } 
        public BaseScreen(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _renderables = new List<IRenderable>();
            _updateables = new ConcurrentBag<IUpdateable>();
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

        public void RegisterControl(object control)
        {
            try
            {
                Renderables.Add((IRenderable)control);
                Updateables.Add((IUpdateable)control);
            }
            catch
            {
            }
            var ordered = Renderables.OrderBy(r => r.Origin.Z).ToList();
            Renderables.Clear();
            Renderables.AddRange(ordered);
        }

        public void UnregisterControl(object control)
        {
            
        }
    }
}
