using System;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Enumeration values taken from the OpenTK project. [https://github.com/opentk/opentk/]</remarks>
    public enum ErrorCode
        : int
    {
        /// <summary>
        /// 
        /// </summary>
        NoError = 0,

        /// <summary>
        /// 
        /// </summary>
        InvalidEnum = 0x0500,

        /// <summary>
        /// 
        /// </summary>
        InvalidValue = 0x0501,

        /// <summary>
        /// 
        /// </summary>
        InvalidOperation = 0x0502,

        /// <summary>
        /// 
        /// </summary>
        StackOverflow = 0x0503,

        /// <summary>
        /// 
        /// </summary>
        StackUnderflow = 0x0504,

        /// <summary>
        /// 
        /// </summary>
        OutOfMemory = 0x0505,

        /// <summary>
        /// 
        /// </summary>
        InvalidFramebufferOperation = 0x0506,

        /// <summary>
        /// 
        /// </summary>
        ContextLost = 0x0507
    }
}
