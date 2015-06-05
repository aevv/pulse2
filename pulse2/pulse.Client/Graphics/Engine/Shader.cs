﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.ES30;

namespace pulse.Client.Graphics.Engine
{
    class Shader
    {
        private int _vertexShaderId;
        private int _fragmentShaderId;
        private int _shaderProgramId;

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
        }
    }
}
