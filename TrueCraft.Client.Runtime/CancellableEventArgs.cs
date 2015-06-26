using System;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class CancellableEventArgs
        : EventArgs, ICancellable
    {
        private bool _isCancelled;

        /// <summary>
        /// 
        /// </summary>
        public bool IsCancelled
        {
            get { return _isCancelled; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CancellableEventArgs()
        {
            _isCancelled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Cancel()
        {
            _isCancelled = true;
        }
    }
}
