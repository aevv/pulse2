#version 330 core

in vec3 vertexColor;
in vec2 TexCoord;

out vec4 colour;

uniform sampler2D textureSample;

void main()
{
    colour = texture(textureSample, TexCoord);
}