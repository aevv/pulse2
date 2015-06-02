using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;

namespace pulse.Client.Graphics.Engine
{
    class Renderer : IRenderer
    {
        private double _time;
        private bool _initialised;

        private float[] vertices =
        {
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f,
            0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f,
        };

        private int vbo;
        private int vertexShader;
        private int fragmentShader;
        private int shaderProgram;
        private int vao;

        public void Initialise()
        {
            if (_initialised)
                return;

            Console.WriteLine(GL.GetInteger(GetPName.MaxVertexAttribs));

            GL.ClearColor(Color4.SlateBlue);
            GL.GenBuffers(1, out vbo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            unsafe
            {
                fixed (float* ptr = vertices)
                {
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), (IntPtr)ptr, BufferUsageHint.StaticDraw);
                }
            }

            vertexShader = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vertexShader, File.ReadAllText("Graphics\\Engine\\Shaders\\VertexShader.glsl"));
            GL.CompileShader(vertexShader);
            var log = GL.GetShaderInfoLog(vertexShader);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText("Graphics\\Engine\\Shaders\\FragmentShader.glsl"));
            GL.CompileShader(fragmentShader);
            log = GL.GetShaderInfoLog(fragmentShader);

            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);
            log = GL.GetProgramInfoLog(shaderProgram);
            GL.UseProgram(shaderProgram);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GenVertexArrays(1, out vao);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            unsafe
            {
                fixed (float* ptr = vertices)
                {
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), (IntPtr)ptr, BufferUsageHint.StaticDraw);
                }
            }
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.BindVertexArray(0);

            _initialised = true;
        }
        public void OnRenderFrame(FrameEventArgs args)
        {
            _time += args.Time;
            var green = (float)Math.Abs(Math.Sin(_time/2));

            int colorLoc = GL.GetUniformLocation(shaderProgram, "ourColor");

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(shaderProgram);
            GL.Uniform4(colorLoc, 0.0f, green, 0.0f, 1.0f);
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);
        }
    }
}
