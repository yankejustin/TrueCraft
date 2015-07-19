using System;
using TrueCraft.Client.Graphics;

namespace TrueCraft.Client.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RendererEventArgs<T> : EventArgs
    {
        private Lazy<Mesh> _meshFactory;

        /// <summary>
        /// 
        /// </summary>
        public T Item { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Lazy<Mesh> Result
        {
            get { return this._meshFactory; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPriority { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="meshFactory"></param>
        /// <param name="isPriority"></param>
        public RendererEventArgs(T item, Lazy<Mesh> meshFactory, bool isPriority)
        {
            this._meshFactory = meshFactory;

            Item = item;
            IsPriority = isPriority;
        }
    }
}
