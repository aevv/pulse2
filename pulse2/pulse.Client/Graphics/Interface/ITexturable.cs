using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Graphics.Interface
{
    interface ITexturable
    {
        int TextureId{ get; }
        string TexturePath { get; }
        void ApplyTexture(string path);
        void ApplyTexture(int textureId);
    }
}
