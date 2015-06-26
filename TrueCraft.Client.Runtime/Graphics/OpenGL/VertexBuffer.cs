using System;
using System.Runtime;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class VertexBuffer
        : SafeHandle
    {
        private static VertexBuffer _bound =
            null;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Bound;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Unbound;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> DataChanged;

        private BufferUsage _usage;
        private int _length;

        /// <summary>
        /// 
        /// </summary>
        public BufferUsage Usage
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _usage;
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                _usage = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBound
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return (_bound == this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public VertexBuffer()
            : base()
        {
            base._handle = (IntPtr)GL.GenBuffer();
            OpenGLException.CheckErrors();

            _usage = BufferUsage.Static;
            _length = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Bind()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!IsBound)
            {
                _bound = this;
                GL.BindBuffer(
                    BufferTarget.ArrayBuffer,
                    (int)base._handle);
                OpenGLException.CheckErrors();

                var args = EventArgs.Empty;
                if (Bound != null)
                    Bound(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetData(Vertex[] data)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!IsBound)
                throw new InvalidOperationException();

            _length = data.Length;

            GL.BufferData<Vertex>(
                BufferTarget.ArrayBuffer,
                new IntPtr(data.Length * Marshal.SizeOf(default(Vertex))),
                data,
                (BufferUsageHint)_usage);
            OpenGLException.CheckErrors();

            var args = EventArgs.Empty;
            if (DataChanged != null)
                DataChanged(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Unbind()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (IsBound)
            {
                _bound = null;
                GL.BindBuffer(
                    BufferTarget.ArrayBuffer,
                    (int)IntPtr.Zero);
                OpenGLException.CheckErrors();

                var args = EventArgs.Empty;
                if (Unbound != null)
                    Unbound(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (IsBound)
                    Unbind();

                GL.DeleteBuffer((int)base._handle);
                OpenGLException.CheckErrors();

                _usage = default(BufferUsage);
                _length = default(int);
            }

            base.Dispose(disposing);
        }
    }
}
