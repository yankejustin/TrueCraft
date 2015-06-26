using System;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseButtonEventArgs
        : MouseEventArgs
    {
        private MouseButton _button;
        private bool _isPressed;

        /// <summary>
        /// 
        /// </summary>
        public MouseButton Button
        {
            get { return _button; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPressed
        {
            get { return _isPressed; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="button"></param>
        /// <param name="isPressed"></param>
        public MouseButtonEventArgs(int x, int y, MouseButton button, bool isPressed)
            : base(x, y)
        {
            _button = button;
            _isPressed = isPressed;
        }
    }
}
