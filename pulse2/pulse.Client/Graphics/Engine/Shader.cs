﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.ES30;

namespace pulse.Client.Graphics.Engine
{
    class Shader
    {
        private int _vertexShaderId;
        private int _fragmentShaderId;
        private int _shaderProgramId;
        private int _id;

        public int TransformPointer
        {
            get { return _id; }
        }

        public int ProgramId { get { return _shaderProgramId; } }

        public Shader(string vertexSourcePath, string fragmentSourcePath)
        {
            LoadFromFile(vertexSourcePath, fragmentSourcePath);
        }

        public void LoadFromFile(string vertexSourcePath, string fragmentSourcePath)
        {
            if (!File.Exists(vertexSourcePath))
                throw new ArgumentNullException(vertexSourcePath);
            if (!File.Exists(fragmentSourcePath))
                throw new ArgumentNullException(fragmentSourcePath);

            var vertexProgram = File.ReadAllText(vertexSourcePath);
            var fragmentProgram = File.ReadAllText(fragmentSourcePath);

            _vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShaderId, vertexProgram);
            GL.CompileShader(_vertexShaderId);

            _fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShaderId, fragmentProgram);
            GL.CompileShader(_fragmentShaderId);

            _shaderProgramId = GL.CreateProgram();
            GL.AttachShader(_shaderProgramId, _vertexShaderId);
            GL.AttachShader(_shaderProgramId, _fragmentShaderId);

            GL.LinkProgram(_shaderProgramId);

            GL.DeleteShader(_vertexShaderId);
            GL.DeleteShader(_fragmentShaderId);

            _id = GL.GetUniformLocation(ProgramId, "transform");
        }

        public void ApplyMatrices(Matrix4 view, Matrix4 projection)
        {
            var viewLoc = GL.GetUniformLocation(_shaderProgramId, "view");
            var projectionLoc = GL.GetUniformLocation(_shaderProgramId, "projection");

            GL.UniformMatrix4(viewLoc, false, ref view);
            GL.UniformMatrix4(projectionLoc, false, ref projection);
        }

        public void ApplyModelMatrix(Matrix4 model)
        {
            var modelLoc = GL.GetUniformLocation(_shaderProgramId, "model");

            GL.UniformMatrix4(modelLoc, false, ref model);
        }
    }
}
