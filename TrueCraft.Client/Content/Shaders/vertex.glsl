#version 330

layout(location = 0)in vec3 in_Position;
layout(location = 1)in vec3 in_Normal;
layout(location = 2)in vec4 in_Color;
layout(location = 3)in vec2 in_TexCoord0;

out vec3 out_Normal;
out vec4 out_Color;
out vec2 out_TexCoord0;

uniform mat4 un_Projection;
uniform mat4 un_View;
uniform mat4 un_Model;

void main()
{
	out_Normal = in_Normal;
	out_Color = in_Color;
	out_TexCoord0 = in_TexCoord0;

	gl_Position = un_Projection * un_View * un_Model * vec4(in_Position, 1.0);
}
