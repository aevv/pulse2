using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using pulse.Client.Graphics.Engine.Strategies;
using pulse.Client.Graphics.Engine.Util;
using pulse.Client.Screens;

namespace pulse.Client.Graphics.Engine
{
    class Renderer : IRenderer
    {
        private IDictionary<ShapeType, IRenderStrategy> _strategies;
        private Shader _shader;
        private bool _initialised;

        private Size _screenSize;

        public Renderer(Size screenSize)
        {
            _strategies = new Dictionary<ShapeType, IRenderStrategy>();
            _screenSize = screenSize;
        }

        public void Initialise()
        {
            if (_initialised)
                return;

            SetupStrategies();

            GL.ClearColor(Color4.SlateBlue);

            GL.Viewport(0, 0, _screenSize.Width, _screenSize.Height);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            _shader = new Shader("Graphics\\Engine\\Shaders\\VertexShader.glsl", "Graphics\\Engine\\Shaders\\FragmentShader.glsl");

            _initialised = true;
        }

        public void Resize(int width, int height)
        {
            _screenSize = new Size(width, height);
            GL.Viewport(0, 0, _screenSize.Width, _screenSize.Height);
        }

        private void SetupStrategies()
        {
            _strategies.Clear();
            _strategies.Add(ShapeType.Cube, new CubeRenderStrategy());
        }

        public void OnRenderFrame(FrameEventArgs args, BaseScreen screen)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_shader.ProgramId);
            var view = Matrix4.CreateTranslation(0, 0, -3f);
            var projection = Matrix4.CreateOrthographicOffCenter(0, _screenSize.Width, _screenSize.Height, 0, -10f, 100f);

            _shader.ApplyMatrix(view, "view");
            _shader.ApplyMatrix(projection, "projection");
            _shader.ApplyLighting(Color.White);

            Render(args, screen);
        }

        private void Render(FrameEventArgs args, BaseScreen screen)
        {
            foreach (var renderable in screen.Renderables)
            {
                _strategies[renderable.Shape].Render(_shader, renderable);
            }
        }
    }
}
