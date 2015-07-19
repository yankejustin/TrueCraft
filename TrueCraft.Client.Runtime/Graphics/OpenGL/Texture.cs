using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Texture
        : SafeHandle
    {
        /// <summary>
        /// 
        /// </summary>
        public const int Units = 4;

        private static int _currentUnit = -1;
        private static Texture[] _bound =
            new Texture[Texture.Units];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        protected static void SwitchUnit(int unit)
        {
            if (unit == -1)
                return;

            if ((unit < 0) || (unit >= Texture.Units))
                throw new ArgumentException();

            if (_currentUnit != unit)
            {
                _currentUnit = unit;

                GL.ActiveTexture(
                    (TextureUnit)TextureUnit.Texture0 + unit);
                OpenGLException.CheckErrors();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Bound;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Unbound;

        private int _unit;

        public bool IsBound
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (this._unit == -1)
                    return false;
                else
                    return (_bound[this._unit] == this);
            }
        }

        public int Unit
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return this._unit;
            }
        }

        protected abstract TextureTarget Target
        {
            get;
        }

        public Texture()
            : base()
        {
            base._handle = (IntPtr)GL.GenTexture();
            OpenGLException.CheckErrors();

            this._unit = -1;
        }

        public void Bind(int unit = 0)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if ((unit < 0) || (unit >= Texture.Units))
                throw new ArgumentException();

            if (!IsBound)
            {
                if (_bound[unit] != null)
                    _bound[unit].Unbind();

                this._unit = unit;
                _bound[this._unit] = this;

                SwitchUnit(this._unit);
                GL.BindTexture(
                    (OpenTK.Graphics.OpenGL.TextureTarget)Target,
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
        public void Unbind()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (IsBound)
            {
                SwitchUnit(this._unit);

                _bound[this._unit] = null;
                this._unit = -1;

                GL.BindTexture(
                    (OpenTK.Graphics.OpenGL.TextureTarget)Target,
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

                GL.DeleteTexture((int)base._handle);
                OpenGLException.CheckErrors();

                this._unit = default(int);
            }

            base.Dispose(disposing);
        }
    }
}
