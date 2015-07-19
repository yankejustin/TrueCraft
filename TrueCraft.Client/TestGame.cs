using System;
using TrueCraft.Client.Maths;
using TrueCraft.Client.Graphics;
using TrueCraft.Client.Graphics.OpenGL;
using OGL = OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client
{
    public sealed class TestGame
        : Game
    {
        private ShaderProgram _program;
        private Mesh _cube;
        private float _time;

        public TestGame()
            : base(800, 600, "Test", false) { }

        protected override void OnLoad(object sender, EventArgs e)
        {
            base.OnLoad(sender, e);

            OGL.GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            OGL.GL.Enable(OGL.EnableCap.DepthTest);

            var vao = OGL.GL.GenVertexArray();
            OGL.GL.BindVertexArray(vao);

            var vertexShader = new Shader(ShaderType.Vertex, @"
#version 330

uniform mat4 un_Projection;
uniform mat4 un_Model;

layout(location = 0)in vec3 in_Position;
layout(location = 1)in vec3 in_Normal;
layout(location = 2)in vec4 in_Color;
layout(location = 3)in vec2 in_TexCoord0;

out vec3 out_Normal;
out vec4 out_Color;
out vec2 out_TexCoord0;

void main()
{
    out_Normal = in_Normal;
    out_Color = in_Color;
    out_TexCoord0 = in_TexCoord0;

    gl_Position = un_Projection * un_Model * vec4(in_Position, 1.0);
}
");
            vertexShader.Compile();

            var fragmentShader = new Shader(ShaderType.Fragment, @"
#version 330

in vec3 out_Normal;
in vec4 out_Color;
in vec2 out_TexCoord0;

out vec4 gl_Color;

void main()
{
    gl_Color = vec4(out_Color.rgb, 1.0);
}
");
            fragmentShader.Compile();

            this._program = new ShaderProgram(vertexShader, fragmentShader);
            this._program.Link();
            this._program.MakeCurrent();
            this._program.GetUniform<Matrix>("un_Projection").SetValue(Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(70.0f), 800.0f / 600.0f, 0.01f, 1000.0f));

            this._cube = new Mesh();

            this._cube.Vertices = new Vertex[]
            {
                new Vertex(new Vector3(-0.8f, -0.8f, -0.8f), Vector3.Zero, new Color(255, 0, 0), Vector2.Zero),
                new Vertex(new Vector3(0.8f, -0.8f, -0.8f), Vector3.Zero, new Color(0, 255, 0), Vector2.Zero),
                new Vertex(new Vector3(0.8f, 0.8f, -0.8f), Vector3.Zero, new Color(0, 0, 255), Vector2.Zero),
                new Vertex(new Vector3(-0.8f, 0.8f, -0.8f), Vector3.Zero, new Color(255, 255, 0), Vector2.Zero),

                new Vertex(new Vector3(-0.8f, -0.8f, 0.8f), Vector3.Zero, new Color(255, 0, 255), Vector2.Zero),
                new Vertex(new Vector3(0.8f, -0.8f, 0.8f), Vector3.Zero, new Color(0, 255, 255), Vector2.Zero),
                new Vertex(new Vector3(0.8f, 0.8f, 0.8f), Vector3.Zero, new Color(255, 255, 255), Vector2.Zero),
                new Vertex(new Vector3(-0.8f, 0.8f, 0.8f), Vector3.Zero, new Color(0, 0, 0), Vector2.Zero)
            };

            this._cube.SetSubmesh(0, new uint[]
            {
                0, 7, 3,
                0, 4, 7,

                1, 2, 6,
                6, 5, 1,

                0, 2, 1,
                0, 3, 2,

                4, 5, 6,
                6, 7, 4,

                2, 3, 6,
                6, 3, 7,

                0, 1, 5,
                0, 5, 4
            });
        }

        protected override void OnRender(object sender, FrameEventArgs e)
        {
            this._time += (float)e.DeltaTime;

            OGL.GL.Clear(
                OGL.ClearBufferMask.ColorBufferBit |
                OGL.ClearBufferMask.DepthBufferBit);

            this._program.MakeCurrent();
            //this._program.GetUniform<Matrix>("un_Model").SetValue(Matrix.Identity);
            this._program.GetUniform<Matrix>("un_Model").SetValue(
                Matrix.CreateRotationX(_time / 2.0f) *
                Matrix.CreateRotationY(_time / 2.0f) *
                Matrix.CreateTranslation(0.0f, 0.0f, -3.0f));

            this._cube.Draw(0);

            base.OnRender(sender, e);
        }

        protected override void OnUnload(object sender, EventArgs e)
        {
            this._cube.Dispose();
            this._program.Dispose();

            base.OnUnload(sender, e);
        }
    }
}
