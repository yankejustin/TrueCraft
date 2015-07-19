using System;
using System.Collections;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ShaderProgram
        : SafeHandle
    {
        private static ShaderProgram _current =
            null;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ShaderEventArgs> ShaderAttached;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ShaderEventArgs> ShaderDetached;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Linked;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> MadeCurrent;

        private IList<Shader> _shaders;
        private IDictionary<string, IUniform> _uniforms;
        private bool _isLinked;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<Shader> Shaders
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return (IReadOnlyList<Shader>)_shaders;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLinked
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _isLinked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCurrent
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return (_current == this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shaders"></param>
        public ShaderProgram(params Shader[] shaders)
            : base()
        {
            base._handle = (IntPtr)GL.CreateProgram();
            OpenGLException.CheckErrors();

            _shaders = new List<Shader>();
            _uniforms = new Dictionary<string, IUniform>();
            _isLinked = false;

            foreach (var shader in shaders)
                AttachShader(shader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shader"></param>
        public void AttachShader(Shader shader)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if ((shader == null) || shader.IsDisposed || !shader.IsCompiled || _shaders.Contains(shader))
                throw new ArgumentException();

            if (IsCurrent)
            {
                _current = null;
                GL.UseProgram((int)IntPtr.Zero);
            }

            GL.AttachShader(
                (int)base._handle,
                (int)shader.Handle);
            OpenGLException.CheckErrors();

            _shaders.Add(shader);
            foreach (var uniform in _uniforms)
                uniform.Value.RefreshHandle();
            _isLinked = false;

            shader.SourceChanged += OnShaderSourceChanged;
            shader.Disposed += OnShaderDisposed;

            var args = new ShaderEventArgs(shader);
            if (ShaderAttached != null)
                ShaderAttached(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shader"></param>
        public void DetachShader(Shader shader)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (shader == null || !_shaders.Contains(shader))
                throw new ArgumentException();

            if (IsCurrent)
            {
                _current = null;
                GL.UseProgram((int)IntPtr.Zero);
            }

            GL.DetachShader(
                (int)base._handle,
                (int)shader.Handle);
            OpenGLException.CheckErrors();

            _shaders.Remove(shader);
            foreach (var uniform in _uniforms)
                uniform.Value.RefreshHandle();
            _isLinked = false;

            shader.Disposed -= OnShaderDisposed;
            shader.SourceChanged -= OnShaderSourceChanged;

            var args = new ShaderEventArgs(shader);
            if (ShaderDetached != null)
                ShaderDetached(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Link()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!TryLink())
                throw new GraphicsException(GetInfoLog());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool TryLink()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var result = 0;
            GL.LinkProgram((int)base._handle);
            GL.GetProgram((int)base._handle, GetProgramParameterName.LinkStatus, out result);
            OpenGLException.CheckErrors();

            _isLinked = (result != 0);
            if (_isLinked)
            {
                var args = EventArgs.Empty;
                if (Linked != null)
                    Linked(this, args);
            }

            return _isLinked;
        }

        /// <summary>
        /// 
        /// </summary>
        public void MakeCurrent()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!_isLinked)
                throw new InvalidOperationException();

            if (!IsCurrent)
            {
                _current = this;
                GL.UseProgram((int)base._handle);
                OpenGLException.CheckErrors();

                var args = EventArgs.Empty;
                if (MadeCurrent != null)
                    MadeCurrent(this, args);
            }
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
            GL.GetProgramInfoLog((int)base._handle, out result);
            OpenGLException.CheckErrors();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public Uniform<T> GetUniform<T>(string name)
        {
            var result = default(IUniform);
            if (!_uniforms.TryGetValue(name, out result))
            {
                result = new Uniform<T>(this, name);
                _uniforms.Add(name, result);
            }

            return result as Uniform<T>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShaderDisposed(object sender, DisposedEventArgs e)
        {
            var shader = (Shader)sender;
            DetachShader(shader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShaderSourceChanged(object sender, EventArgs e)
        {
            var shader = (Shader)sender;
            shader.Compile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var shader in _shaders)
                    DetachShader(shader);

                GL.DeleteProgram((int)base._handle);
                OpenGLException.CheckErrors();

                _uniforms = default(IDictionary<string, IUniform>);
                _shaders = default(IList<Shader>);
                _isLinked = default(bool);
            }

            base.Dispose(disposing);
        }
    }
}
