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
        private Mesh _mesh;
        private Func<Mesh> _meshFactory;

        /// <summary>
        /// 
        /// </summary>
        public T Item { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Mesh Result
        {
            get
            {
                if (this._mesh == null)
                    this._mesh = this._meshFactory.Invoke();

                return this._mesh;
            }
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
        public RendererEventArgs(T item, Func<Mesh> meshFactory, bool isPriority)
        {
            this._mesh = null;
            this._meshFactory = meshFactory;

            Item = item;
            IsPriority = isPriority;
        }
    }
}
