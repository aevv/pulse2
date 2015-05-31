using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace pulse.Client.Graphics.Interface
{
    interface IRenderable
    {
        bool Visible { get; set; }
        float Depth { get; set; }
        Color4 Colour { get; set; }

        void OnRenderFrame(FrameEventArgs args);
    }
}
