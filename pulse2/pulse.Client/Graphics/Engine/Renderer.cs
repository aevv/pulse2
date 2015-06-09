using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;

namespace pulse.Client.Graphics.Engine
{
    class Renderer : IRenderer
    {
        private Shader _shader;
        private double _time;
        private bool _initialised;
        private int _textureId = 0;
        private int _transformLoc;

        private float[] vertices =
        {
            // Front
            0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
            -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 0.0f,

            // Right side
            0.5f, 0.5f, 0.0f, 1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            0.5f, 0.5f, -0.65f, 0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
            0.5f, -0.5f, -0.65f, 0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
            0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 0.0f,   0.0f, 0.0f,

            // Left Side
            -0.5f, 0.5f, 0.0f, 1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            -0.5f, 0.5f, -0.65f, 0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
            -0.5f, -0.5f, -0.65f, 0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
            -0.5f, -0.5f, 0f, 1.0f, 1.0f, 0.0f,   0.0f, 0.0f,

            // Top
            0.5f, 0.5f, 0.0f, 1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            0.5f, 0.5f, -0.65f, 0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
            -0.5f, 0.5f, -0.65f, 0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
            -0.5f, 0.5f, 0.0f, 1.0f, 1.0f, 0.0f,   0.0f, 0.0f,

            // Bottom
            0.5f, -0.5f, 0.0f,1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            0.5f, -0.5f, -0.65f,0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
            -0.5f, -0.5f, -0.65f,0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
            -0.5f, -0.5f, 0.0f,1.0f, 1.0f, 0.0f,   0.0f, 0.0f,
            // Back
            0.5f,  0.5f, -0.65f,   1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            0.5f, -0.5f, -0.65f,   0.0f, 1.0f, 0.0f,   1.0f, 1.0f,
            -0.5f, -0.5f, -0.65f,   0.0f, 0.0f, 1.0f,   0.0f, 1.0f,
            -0.5f,  0.5f, -0.65f,   1.0f, 1.0f, 0.0f,   0.0f, 0.0f,
        };

        private int[] indices =
        {
            // Front
            0, 1, 3,
            1, 2, 3,
            // Right
            4, 5, 7,
            5, 6, 7,
            //Left
            8, 9, 11,
            9, 10, 11,
            //Top
            12, 13, 15,
            13, 14, 15,
            //Bottom
            16, 17, 19,
            17, 18, 19,
            //Back
            20, 21, 23,
            21, 22, 23
        };

        private int vbo;
        private int vao;
        private int ebo;

        public void Initialise()
        {
            if (_initialised)
                return;

            Console.WriteLine(GL.GetInteger(GetPName.MaxVertexAttribs));

            GL.ClearColor(Color4.SlateBlue);

            GL.Viewport(0, 0, 1024, 768);

            //GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);

            GL.GenVertexArrays(1, out vao);
            GL.GenBuffers(1, out vbo);
            GL.GenBuffers(1, out ebo);


            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            unsafe
            {
                fixed (float* ptr = vertices)
                {
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), (IntPtr)ptr, BufferUsageHint.StaticDraw);
                }

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);

                fixed (int* ptr = indices)
                {
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(int)), (IntPtr)ptr, BufferUsageHint.StaticDraw);
                }
            }
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            GL.BindVertexArray(0);

            _shader = new Shader("Graphics\\Engine\\Shaders\\VertexShader.glsl", "Graphics\\Engine\\Shaders\\FragmentShader.glsl");

            _textureId = TextureManager.LoadImage("Assets\\bg.jpg");

            _initialised = true;
        }

        private float x, y, z, rY, rX;
        public void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Matrix4 ortho = Matrix4.CreateOrthographicOffCenter(0, 1024, 768, 0, -10, 10);
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.PushMatrix();
            //GL.LoadMatrix(ref ortho);

            //_screenManager.Active.OnRenderFrame(e);

            //_fpsCounter.OnRenderFrame(e);

            //GL.PopMatrix();

            GL.UseProgram(_shader.ProgramId);


            var m4 = Matrix4.CreateTranslation(0, 0, 0)
                     *Matrix4.CreateRotationY(rY += 0.01f)
                     *Matrix4.CreateRotationX(rX += 0.01f);
            var view = Matrix4.CreateTranslation(0, 0, -3f);
            //var projection = Matrix4.CreateOrthographicOffCenter(0, 1024, 0, 768, 0.1f, 100f);
            var projection = Matrix4.CreatePerspectiveFieldOfView(1, 1024 / 768, 0.1f, 100f);


            _shader.ApplyMatrices(view, projection);
            _shader.ApplyModelMatrix(m4);

            GL.UniformMatrix4(_shader.TransformPointer, false, ref m4);

            //GL.BindTexture(TextureTarget.Texture2D, _textureId);
            GL.BindVertexArray(vao);
            unsafe
            {
                fixed (int* ptr = indices)
                {
                    GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, (IntPtr)ptr);
                }
            }
            GL.BindVertexArray(0);
        }
    }
}
