using System;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GraphicsComponent
        : GameComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public GraphicsComponent(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
