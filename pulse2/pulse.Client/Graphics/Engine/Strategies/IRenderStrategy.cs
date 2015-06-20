using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics.Engine.Strategies
{
    interface IRenderStrategy
    {
        void Render(Shader shader, IRenderable renderable);
    }
}
