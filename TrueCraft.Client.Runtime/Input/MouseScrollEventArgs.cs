using System;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseScrollEventArgs
        : MouseEventArgs
    {
        private double _wheel, _wheelDelta;

        /// <summary>
        /// 
        /// </summary>
        public double Wheel
        {
            get { return _wheel; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double WheelDelta
        {
            get { return _wheelDelta; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wheel"></param>
        /// <param name="wheelDelta"></param>
        public MouseScrollEventArgs(int x, int y, double wheel, double wheelDelta)
            : base(x, y)
        {
            _wheel = wheel;
            _wheelDelta = wheelDelta;
        }
    }
}
