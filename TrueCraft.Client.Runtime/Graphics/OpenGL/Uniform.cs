﻿using System;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using TrueCraft.Client.Maths;

using TrueCraft.Client;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Uniform<T>
        : IUniform
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static TOut TryCast<TOut>(T value)
        {
            if (value is TOut)
                return ((TOut)(object)value);
            else
                return default(TOut);
        }

        private string _name;
        private IntPtr _handle;
        private ShaderProgram _parent;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
        {
            get { return (_handle != new IntPtr(-1)); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ShaderProgram Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        public Uniform(ShaderProgram parent, string name)
        {
            if ((parent == null) || string.IsNullOrEmpty(name))
                throw new ArgumentException();

            _name = name;
            _handle = IntPtr.Zero;
            _parent = parent;

            RefreshHandle();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshHandle()
        {
            if (_parent.IsDisposed)
                throw new InvalidOperationException();

            var hdl = GL.GetUniformLocation(
                (int)_parent.Handle,
                _name);
            this._handle = new IntPtr(hdl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(T value)
        {
            if (!IsValid)
                return;

            if (value is float)
            {
                var cast = TryCast<float>(value);
                GL.Uniform1((int)_handle, cast);
            }
            else if (value is int)
            {
                var cast = TryCast<int>(value);
                GL.Uniform1((int)_handle, cast);
            }
            else if (value is Vector2)
            {
                var cast = TryCast<Vector2>(value);
                GL.Uniform2((int)_handle, cast.X, cast.Y);
            }
            else if (value is Vector3)
            {
                var cast = TryCast<Vector3>(value);
                GL.Uniform3((int)_handle, cast.X, cast.Y, cast.Z);
            }
            else if (value is Vector4)
            {
                var cast = TryCast<Vector4>(value);
                GL.Uniform4((int)_handle, cast.X, cast.Y, cast.Z, cast.W);
            }
            else if (value is Matrix)
            {
                var cast = TryCast<Matrix>(value);
                GL.UniformMatrix4((int)_handle, 1, false, cast.ToArray());
            }
            else if (value is Texture)
            {
                var cast = TryCast<Texture>(value);
                GL.Uniform1((int)_handle, (int)cast.Unit);
            }
            else
                throw new NotSupportedException();

            OpenGLException.CheckErrors();
        }
    }
}
