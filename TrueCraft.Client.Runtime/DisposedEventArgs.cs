using System;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class DisposedEventArgs
        : CancellableEventArgs
    {
        private bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDisposed"></param>
        public DisposedEventArgs(bool isDisposed)
        {
            _isDisposed = isDisposed;
        }
    }
}
