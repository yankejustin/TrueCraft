using System;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public class ShaderEventArgs
        : EventArgs
    {
        private Shader _shader;

        /// <summary>
        /// 
        /// </summary>
        public Shader Shader
        {
            get { return _shader; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shader"></param>
        public ShaderEventArgs(Shader shader)
        {
            _shader = shader;
        }
    }
}
