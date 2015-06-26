using System;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICancellable
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsCancelled { get; }

        /// <summary>
        /// 
        /// </summary>
        void Cancel();
    }
}
