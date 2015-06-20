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
using pulse.Client.Input.Events;

namespace pulse.Client.Graphics
{
    class Animation : IRenderable, IUpdateable
    {
        public bool Visible { get; set; }
        public Vector3 Origin { get; set; }
        public int TextureId { get; private set; }
        public SizeF Size { get; set; }
        public float Rotation { get; set; }
        public Color4 Colour { get; set; }
        public ShapeType Shape { get; set; }
        public bool Loop { get; set; }

        private readonly List<int> _textureIds;
        private double _frameTimer;
        private int _currentFrame;

        public Animation(Vector3 point, SizeF size)
        {
            Origin = point;
            Size = size;
            _textureIds = new List<int>();
            Visible = true;
            Shape = ShapeType.Cube;
        }

        public void ApplyTextures(IEnumerable<string> filePaths)
        {
            _textureIds.Clear();
            _textureIds.AddRange(filePaths.Select(TextureManager.LoadImage).ToList());
            _currentFrame = 0;
        }

        public void OnUpdateFrame(UpdateFrameEventArgs args)
        {
            if (!Visible)
                return;

            _frameTimer += args.Time;

            if (_frameTimer >= 1 / 20d)
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

            TextureId = _textureIds[_currentFrame];
        }
    }
}
