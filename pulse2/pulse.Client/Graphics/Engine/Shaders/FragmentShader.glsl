#version 330 core

in vec3 vertexColour;
in vec2 TexCoord;

out vec4 colour;

uniform vec3 lightColour;

uniform sampler2D textureSample;

void main()
{

	colour = texture(textureSample, TexCoord) * vec4(lightColour*vertexColour, 1.0f);	
}