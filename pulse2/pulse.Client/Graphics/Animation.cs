using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using pulse.Client.Graphics.Engine.Util;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics
{
    class Animation : IRenderable
    {
        public bool Visible { get; set; }
        public Vector3 Origin { get { return _frameProxy.Origin; } set { _frameProxy.Origin = value; } }
        public int TextureId { get; private set; }
        public SizeF Size { get { return _frameProxy.Size; } set { _frameProxy.Size = value; } }
        public float Rotation { get { return _frameProxy.Rotation; } set { _frameProxy.Rotation = value; } }
        public Color4 Colour { get { return _frameProxy.Colour; } set { _frameProxy.Colour = value; }}
        public ShapeType Shape { get; set; }
        public float Depth { get { return _frameProxy.Depth; } set { _frameProxy.Depth = value; } }
        public bool Loop { get; set; }

        private readonly List<int> _textureIds;
        private double _frameTimer;
        private int _currentFrame;
        private Quad _frameProxy;

        public Animation(Vector3 point, SizeF size)
        {
            _textureIds = new List<int>();
            _frameProxy = new Quad(point, size);
            Visible = true;
            Shape = ShapeType.Cube;
        }

        public void ApplyTextures(IEnumerable<string> filePaths)
        {
            _textureIds.Clear();
            _textureIds.AddRange(filePaths.Select(TextureManager.LoadImage).ToList());
            _currentFrame = 0;
        }

        public void OnRenderFrame(FrameEventArgs args)
        {
            if (!Visible)
                return;

            _frameTimer += args.Time;

            if (_frameTimer >= 1/20d)
            {
                _frameTimer = 0;

                if (_currentFrame >= _textureIds.Count - 1)
                {
                    if (Loop)
                        _currentFrame = 0;
                    else
                        Visible = false;
                }
                else
                {
                    _currentFrame++;
                }
            }

            _frameProxy.ApplyTexture(_textureIds[_currentFrame]);

            //_frameProxy.OnRenderFrame(args);
        }
    }
}
