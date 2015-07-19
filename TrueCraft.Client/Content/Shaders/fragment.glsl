#version 330

in vec3 out_Normal;
in vec4 out_Color;
in vec2 out_TexCoord0;

out vec4 gl_Color;

void main()
{
	gl_Color = vec4(out_Normal.r, out_Color.g, out_TexCoord0.y, 1.0);
}