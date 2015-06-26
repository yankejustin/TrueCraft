using System;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUniform
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        ShaderProgram Parent { get; }

        /// <summary>
        /// 
        /// </summary>
        void RefreshHandle();
    }
}
