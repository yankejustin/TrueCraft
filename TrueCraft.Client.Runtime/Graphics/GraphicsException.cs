using System;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphicsException
        : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public GraphicsException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public GraphicsException(string message)
            : base(message)
        {
        }
    }
}
