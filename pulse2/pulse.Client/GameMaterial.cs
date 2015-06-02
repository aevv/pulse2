using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using pulse.Client.Graphics.Engine;

namespace pulse.Client
{
    class GameMaterial : GameWindow
    {

        private readonly IRenderer _renderer;

        public GameMaterial()
            : base(1024, 768)
        {
            _renderer = new Renderer();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _renderer.Initialise();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _renderer.OnRenderFrame(e);

            SwapBuffers();
        }
    }
}
