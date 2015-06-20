using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.ES30;
using pulse.Client.Graphics.Engine.Util;
using pulse.Client.Graphics.Interface;

namespace pulse.Client.Graphics.Engine.Strategies
{
    class CubeRenderStrategy : IRenderStrategy
    {
        private int _vaoId;
        private int _vboId;
        private int _eboId;

        public CubeRenderStrategy()
        {
            ConfigureVAO();
        }

        private void ConfigureVAO()
        {
            GL.GenVertexArrays(1, out _vaoId);
            GL.GenBuffers(1, out _vboId);
            GL.GenBuffers(1, out _eboId);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboId);
            unsafe
            {
                fixed (float* ptr = Shapes.CubeVertices)
                {
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Shapes.CubeVertices.Length * sizeof(float)), (IntPtr)ptr, BufferUsageHint.StaticDraw);
                }

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboId);

                fixed (int* ptr = Shapes.CubeIndices)
                {
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Shapes.CubeIndices.Length * sizeof(int)), (IntPtr)ptr, BufferUsageHint.StaticDraw);
                }
            }

            GL.BindVertexArray(_vaoId);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            GL.BindVertexArray(0);
        }

        public void Render(Shader shader, IRenderable renderable)
        {
            var m4 = Matrix4.CreateTranslation(renderable.Origin);
            shader.ApplyMatrix(m4, "model");

            m4 = Matrix4.CreateScale(renderable.Size.Width, renderable.Size.Height, 10);
            shader.ApplyMatrix(m4, "scale");

            GL.BindTexture(TextureTarget.Texture2D, renderable.TextureId);
            GL.BindVertexArray(_vaoId);
            unsafe
            {
                fixed (int* ptr = Shapes.CubeIndices)
                {
                    GL.DrawElements(PrimitiveType.Triangles, Shapes.CubeIndices.Length, DrawElementsType.UnsignedInt, (IntPtr)ptr);
                }
            }
            GL.BindVertexArray(0);
        }
    }
}
