using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Graphics.Interface;
using pulse.Client.Input.Events;

namespace pulse.Client.Graphics
{
    class FpsCounter : IRenderable, IUpdateable
    {
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public float Depth { get; set; }

        private RawText _fpsText;
        private int _frameCount;
        private double _totalTime;

        public FpsCounter()
        {
            _fpsText = new RawText("FPS: 0", new PointF(0, 0), true);
            _fpsText.Depth = 10f;
        }

        public void OnRenderFrame(FrameEventArgs args)
        {
            _frameCount++;
            _fpsText.OnRenderFrame(args);
        }

        public void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            _totalTime += args.Time;

            if (_totalTime > 0.25)
            {
                _totalTime = 0;
                _fpsText.Text = string.Format("FPS: {0}", _frameCount*4);
                _frameCount = 0;
            }
        }
    }
}
