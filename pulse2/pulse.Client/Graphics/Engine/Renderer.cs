using System;
using System.Collections.Generic;
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
        private int textureId = 0;

        private float[] vertices =
        {
            0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f,
            0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f,
            -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f,
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f
        };

        private int[] indices =
        {
            0, 1, 3,
            1, 2, 3
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

            GL.Enable(EnableCap.Texture2D);

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
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * sizeof(int)), (IntPtr) ptr, BufferUsageHint.StaticDraw);
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

            textureId = TextureManager.LoadImage("Assets\\bg.jpg");

            _initialised = true;
        }
        public void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(_shader.ProgramId);

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.BindVertexArray(vao);
            unsafe
            {
                fixed (int* ptr = indices)
                {
                    GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (IntPtr)ptr);
                }
            }
            GL.BindVertexArray(0);
        }
    }
}
