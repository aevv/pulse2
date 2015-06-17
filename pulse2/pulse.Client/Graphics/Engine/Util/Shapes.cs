using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pulse.Client.Graphics.Engine.Util
{
    enum ShapeType
    {
        Flat = 1,
        Cube = 2,
    }

    static class Shapes
    {
        public static readonly float[] CubeVertices =
        {
            // Front
            1f,  1f, 0f,   1f, 1f, 1f,   1f, 1f,
            1f, 0f, 0f,    1f, 1f, 1f,   1f, 0f,
            0f, 0f, 0f,    1f, 1f, 1f,   0f, 0f,
            0f,  1f, 0f,   1f, 1f, 1f,   0f, 1f,

            // Right side
            1f, 1f, 0f,    1f, 1f, 1f,   1f, 0f,
            1f, 1f, -0.1f, 1f, 1f, 1f,   1f, 1f,
            1f, 0f, -0.1f, 1f, 1f, 1f,   0f, 1f,
            1f, 0f, 0f,    1f, 1f, 1f,   0f, 0f,

            // Left Side
            0f, 1f, 0f,     1f, 1f, 1f,   1f, 0f,
            0f, 1f, -0.1f,  1f, 1f, 1f,   1f, 1f,
            0f, 0f, -0.1f,  1f, 1f, 1f,   0f, 1f,
            0f, 0f, 0f,     1f, 1f, 1f,   0f, 0f,

            // Top
            1f, 1f, 0f,     1f, 1f, 1f,   0f, 0f,
            1f, 1f, -0.1f,  1f, 1f, 1f,   0f, 0f,
            0f, 1f, -0.1f,  1f, 1f, 1f,   0f, 0f,
            0f, 1f, 0f,     1f, 1f, 1f,   0f, 0f,

            // Bottom
            1f, 0f, 0f,     1f, 1f, 1f,   1f, 0f,
            1f, 0f, -0.1f,  1f, 1f, 1f,   1f, 1f,
            0f, 0f, -0.1f,  1f, 1f, 1f,   0f, 1f,
            0f, 0f, 0f,     1f, 1f, 1f,   0f, 0f,
            // Back
            1f,  1f, -0.1f, 1f, 1f, 1f,   1f, 0f,
            1f, 0f, -0.1f,  1f, 1f, 1f,   1f, 1f,
            0f, 0f, -0.1f,  1f, 1f, 1f,   0f, 1f,
            0f,  1f, -0.1f, 1f, 1f, 1f,   0f, 0f,
        };

        public static readonly int[] CubeIndices = {
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
    }
}
