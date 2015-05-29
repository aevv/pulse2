using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace pulse.Client.Graphics.Interface
{
    interface IRenderable
    {
        PointF Location { get; set; }
        SizeF Size { get; set; }
        float Rotation { get; set; }
        Color4 Colour { get; set; }
        float Depth { get; set; }

        void OnRenderFrame(FrameEventArgs args);
    }
}
