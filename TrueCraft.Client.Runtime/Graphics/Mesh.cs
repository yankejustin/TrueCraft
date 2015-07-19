using System;
using System.Runtime;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using TrueCraft.Client;
using TrueCraft.Client.Graphics;
using TrueCraft.Client.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics
{
    public class Mesh : IDisposable
    {
        public const int SubmeshLimit = 16;

        private VertexBuffer _vertices;
        private IndexBuffer[] _indices;
        private bool _isDynamic, _isDisposed;

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

        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

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

        public Mesh(Vertex[] vertices, ushort[] indices, bool isDynamic = false)
            : this(1, isDynamic)
        {
            Vertices = vertices;
            SetSubmesh(0, indices);
        }

        public void SetSubmesh(int index, ushort[] indices)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if ((index < 0) || (index > _indices.Length) || (indices == null))
                throw new ArgumentException();

            _indices[index].Bind();
            _indices[index].SetData(indices);
        }

        public void Draw()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            _vertices.Bind();
            for (int i = 0; i < this._indices.Length; i++)
                Draw(i);
        }

        public void Draw(int index)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if ((index < 0) || (index > _indices.Length))
                throw new ArgumentException();

            //if (!_vertices.IsBound)
            //{
                _vertices.Bind();
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 36, 0);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, 36, 12);
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, 36, 24);
                GL.EnableVertexAttribArray(3);
                GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, 36, 28);
                OpenGLException.CheckErrors();
            //}

            _indices[index].Bind();
            GL.DrawElements(
                PrimitiveType.Triangles,
                _indices[index].Length,
                DrawElementsType.UnsignedShort,
                (int)IntPtr.Zero);
            OpenGLException.CheckErrors();
        }

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
