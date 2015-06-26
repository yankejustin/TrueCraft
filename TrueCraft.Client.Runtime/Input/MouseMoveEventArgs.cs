using System;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseMoveEventArgs
        : MouseEventArgs
    {
        private int _deltaX, _deltaY;

        /// <summary>
        /// 
        /// </summary>
        public int DeltaX
        {
            get { return _deltaX; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeltaY
        {
            get { return _deltaY; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public MouseMoveEventArgs(int x, int y, int deltaX, int deltaY)
            : base(x, y)
        {
            _deltaX = deltaX;
            _deltaY = deltaY;
        }
    }
}
