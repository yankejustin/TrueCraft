using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;

using TrueCraft.Client.Maths;
using TrueCraft.Client.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SpriteRenderer
    {
        public ShaderProgram Effect { get; set; }
        public bool IsRendering { get; private set; }

        private VertexBuffer _vertexBuffer;
        private List<Vertex> _vertices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effect"></param>
        public SpriteRenderer(ShaderProgram effect)
        {
            Effect = effect;

            _vertexBuffer = new VertexBuffer();
            _vertices = new List<Vertex>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Begin()
        {
            if (IsRendering)
                throw new InvalidOperationException();

            IsRendering = true;
            _vertices.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="srcRectangle"></param>
        /// <param name="dstRectangle"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        public void DrawTexture(Texture2D texture, Rectangle srcRectangle, Rectangle dstRectangle, Color color, float scale = 1.0f)
        {
            if (!IsRendering)
                throw new InvalidOperationException();

            var positions = new Vector3[]
            {
                new Vector3(dstRectangle.X, dstRectangle.Y, 0.0f),
                new Vector3(dstRectangle.X + dstRectangle.Width, dstRectangle.Y, 0.0f),
                new Vector3(dstRectangle.X + dstRectangle.Width, dstRectangle.Y + dstRectangle.Height, 0.0f),
                new Vector3(dstRectangle.X, dstRectangle.Y + dstRectangle.Height, 0.0f)
            };
            var normal = Vector3.Forward;
            var texCoords = new Vector2[]
            {
                new Vector2(srcRectangle.X, srcRectangle.Y),
                new Vector2(srcRectangle.X + srcRectangle.Width, srcRectangle.Y),
                new Vector2(srcRectangle.X + srcRectangle.Width, srcRectangle.Y + srcRectangle.Height),
                new Vector2(srcRectangle.X, srcRectangle.Y + srcRectangle.Height)
            };

            _vertices.AddRange(new []
            {
                new Vertex(positions[0], normal, color, texCoords[0]),
                new Vertex(positions[1], normal, color, texCoords[1]),
                new Vertex(positions[2], normal, color, texCoords[2]),
                new Vertex(positions[2], normal, color, texCoords[2]),
                new Vertex(positions[1], normal, color, texCoords[1]),
                new Vertex(positions[3], normal, color, texCoords[3])
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void End()
        {
            if (!IsRendering)
                throw new InvalidOperationException();

            Effect.MakeCurrent();
            _vertexBuffer.SetData(_vertices.ToArray());
            _vertexBuffer.Bind();

            GL.DrawArrays(
                PrimitiveType.Triangles,
                0,
                _vertexBuffer.Length);
            OpenGLException.CheckErrors();

            IsRendering = false;
        }
    }
}
