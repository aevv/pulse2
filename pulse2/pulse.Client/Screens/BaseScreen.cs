using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using pulse.Client.Graphics;

namespace pulse.Client.Screens
{
    abstract class BaseScreen
    {
        protected string _name;
        protected readonly List<Renderable> _renderables;

        public string Name { get { return _name; } }

        public BaseScreen()
        {
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
