using System;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseEventArgs
        : EventArgs
    {
        private int _x, _y;

        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public MouseEventArgs(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
