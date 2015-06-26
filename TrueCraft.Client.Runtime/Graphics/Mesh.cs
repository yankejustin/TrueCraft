using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using TrueCraft.Client;
using TrueCraft.Client.Graphics;
using TrueCraft.Client.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Mesh : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public const int SubmeshLimit = 16;

        private VertexBuffer _vertices;
        private IndexBuffer[] _indices;
        private bool _isDynamic, _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDynamic
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _isDisposed;
            }
            set
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                _isDynamic = value;
                var usage = (_isDynamic) ? BufferUsage.Dynamic : BufferUsage.Static;
                _vertices.Usage = usage;
                foreach (var indices in _indices)
                    indices.Usage = usage;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vertex[] Vertices
        {
            set
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (value == null)
                    throw new ArgumentException();

                _vertices.Bind();
                _vertices.SetData(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submeshes"></param>
        /// <param name="isDynamic"></param>
        public Mesh(int submeshes = 1, bool isDynamic = false)
        {
            if ((submeshes < 0) || (submeshes >= Mesh.SubmeshLimit))
                throw new ArgumentOutOfRangeException();

            _vertices = new VertexBuffer();
            _indices = new IndexBuffer[submeshes];
            for (int i = 0; i < this._indices.Length; i++)
                _indices[i] = new IndexBuffer();
            IsDynamic = isDynamic;
            _isDisposed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="indices"></param>
        /// <param name="isDynamic"></param>
        public Mesh(Vertex[] vertices, ushort[] indices, bool isDynamic = false)
            : this(1, isDynamic)
        {
            Vertices = vertices;
            SetSubmesh(0, indices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="indices"></param>
        public void SetSubmesh(int index, ushort[] indices)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if ((index < 0) || (index > _indices.Length) || (indices == null))
                throw new ArgumentException();

            _indices[index].Bind();
            _indices[index].SetData(indices);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            _vertices.Bind();
            for (int i = 0; i < this._indices.Length; i++)
                Draw(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void Draw(int index)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if ((index < 0) || (index > _indices.Length))
                throw new ArgumentException();

            _vertices.Bind();
            _indices[index].Bind();
            GL.DrawElements(
                PrimitiveType.Triangles,
                _indices[index].Length,
                DrawElementsType.UnsignedShort,
                (int)IntPtr.Zero);
            OpenGLException.CheckErrors();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
                return;

            foreach (var indice in _indices)
                indice.Dispose();
            _vertices.Dispose();
            _isDisposed = true;
        }
    }
}
