using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Input;

namespace pulse.Client.Scripting.Factories
{
    class ButtonFactory : IFactory
    {
        public Button Create(float x, float y, float z, int width, int height, string text)
        {
            return new Button(x, y, z, width, height, text);
        }
    }
}
