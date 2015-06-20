using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Graphics.Engine.Util;

namespace pulse.Client.Graphics.Interface
{
    interface IRenderable
    {
        ShapeType Shape { get; }
        Vector3 Origin { get; set; }
        SizeF Size { get; set; }
        int TextureId { get; }
    }
}
