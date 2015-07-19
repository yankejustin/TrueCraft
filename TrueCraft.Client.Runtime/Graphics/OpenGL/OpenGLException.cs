using System;
using System.Diagnostics;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class OpenGLException
        : GraphicsException
    {
        /// <summary>
        /// 
        /// </summary>
        [Conditional("DEBUG")]
        internal static void CheckErrors()
        {
            var error = (ErrorCode)GL.GetError();
            if (error != ErrorCode.NoError)
                throw new OpenGLException(error);
        }

        private ErrorCode _errorCode;

        /// <summary>
        /// 
        /// </summary>
        public ErrorCode ErrorCode
        {
            get { return _errorCode; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public OpenGLException(ErrorCode errorCode)
            : base()
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public OpenGLException(string message, ErrorCode errorCode)
            : base(message)
        {
            _errorCode = errorCode;
        }
    }
}
