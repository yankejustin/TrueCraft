using System;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class SafeHandle
        : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DisposedEventArgs> Disposed;

        protected IntPtr _handle;
        private bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public IntPtr Handle
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _handle;
            }
        }

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
        protected SafeHandle()
            : this(IntPtr.Zero)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public SafeHandle(IntPtr value)
        {
            _handle = value;
            _isDisposed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            var args = new DisposedEventArgs(_isDisposed);
            if (Disposed != null)
                Disposed(this, args);

            if (args.IsCancelled || args.IsDisposed)
                return;

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _handle = IntPtr.Zero;
                _isDisposed = true;
            }
            // TODO: Enforce this after initial port.
            // else
            //     throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        ~SafeHandle()
        {
            Dispose(false);
        }
    }
}
