using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Graphics;

namespace pulse.Client.Scripting.Factories
{
    class BackgroundFactory : IFactory
    {
        public Background Create(string texture)
        {
            return new Background(texture);
        }
    }
}
