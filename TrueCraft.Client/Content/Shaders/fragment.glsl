#version 330

uniform sampler2D un_Diffuse;

in vec3 out_Normal;
in vec4 out_Color;
in vec2 out_TexCoord0;

out vec4 gl_Color;

void main()
{
	vec4 texColor = texture(un_Diffuse, out_TexCoord0);
	if (texColor.a <= 0.5)
		discard;
		
	gl_Color = vec4(texColor.xyz, 1.0);
}