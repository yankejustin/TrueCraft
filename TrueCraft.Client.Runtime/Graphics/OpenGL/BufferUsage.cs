using System;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Enumeration values taken from the OpenTK project. [https://github.com/opentk/opentk/]</remarks>
    public enum BufferUsage
        : int
    {
        /// <summary>
        /// 
        /// </summary>
        Static = ((int)0x88E4),

        /// <summary>
        /// 
        /// </summary>
        Dynamic = ((int)0x88E8),

        /// <summary>
        /// 
        /// </summary>
        Streaming = ((int)0x88E0)
    }
}
