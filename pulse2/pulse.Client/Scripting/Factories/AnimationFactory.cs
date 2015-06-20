using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using pulse.Client.Graphics;

namespace pulse.Client.Scripting.Factories
{
    class AnimationFactory : IFactory
    {
        public Animation Create(float x, float y, float z, float width, float height, string textureNameFormat, int count)
        {
            var anim = new Animation(new Vector3(x, y, z), new SizeF(width, height));
            anim.ApplyTextures(Enumerable.Range(0, count).Select(i => string.Format(textureNameFormat, i)));
            return anim;
        }
    }
}
