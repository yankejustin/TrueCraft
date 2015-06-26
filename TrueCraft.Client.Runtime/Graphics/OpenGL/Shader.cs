using System;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Shader
        : SafeHandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Shader FromFile(ShaderType type, string path)
        {
            var source = File.ReadAllText(path);
            return new Shader(type, source);
        }

        private ShaderType _type;
        private string _source;
        private bool _isCompiled;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> SourceChanged;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Compiled;

        /// <summary>
        /// 
        /// </summary>
        public ShaderType Type
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Source
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _source;
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException();

                _source = value;
                GL.ShaderSource((int)base._handle, _source);
                OpenGLException.CheckErrors();

                if (_isCompiled)
                    _isCompiled = false;

                var args = EventArgs.Empty;
                if (SourceChanged != null)
                    SourceChanged(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCompiled
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _isCompiled;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="source"></param>
        public Shader(ShaderType type, string source = null)
            : base()
        {
            base._handle = (IntPtr)GL.CreateShader(
                (OpenTK.Graphics.OpenGL.ShaderType)type);
            OpenGLException.CheckErrors();

            _type = type;
            _isCompiled = false;

            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Compile()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!TryCompile())
                throw new GraphicsException(GetInfoLog());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool TryCompile()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var result = 0;
            GL.CompileShader((int)base._handle);
            GL.GetShader((int)base._handle, ShaderParameter.CompileStatus, out result);
            OpenGLException.CheckErrors();

            _isCompiled = (result != 0);
            if (_isCompiled)
            {
                var args = EventArgs.Empty;
                if (Compiled != null)
                    Compiled(this, args);
            }

            return _isCompiled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetInfoLog()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var result = string.Empty;
            GL.GetShaderInfoLog((int)base._handle, out result);
            OpenGLException.CheckErrors();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteShader((int)base._handle);
                OpenGLException.CheckErrors();

                _type = default(ShaderType);
                _source = default(string);
                _isCompiled = default(bool);
            }

            base.Dispose(disposing);
        }
    }
}
