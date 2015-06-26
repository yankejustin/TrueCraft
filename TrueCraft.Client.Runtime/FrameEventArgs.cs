using System;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameEventArgs
        : EventArgs
    {
        private double _time, _deltaTime;

        /// <summary>
        /// 
        /// </summary>
        public double Time
        {
            get { return _time; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double DeltaTime
        {
            get { return _deltaTime; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="deltaTime"></param>
        public FrameEventArgs(double time, double deltaTime)
        {
            _time = time;
            _deltaTime = deltaTime;
        }
    }
}
