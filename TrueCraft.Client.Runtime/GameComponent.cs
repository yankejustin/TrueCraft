using System;
using System.Threading;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GameComponent
        : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DisposedEventArgs> Disposed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> IsEnabledChanged;

        private Game _game;
        private bool _isEnabled;
        private bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public Game Game
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _game;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _isEnabled;
            }
            set
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (_isEnabled)
                    {
                        _game.Update += OnUpdate;
                        _game.Render += OnRender;
                    }
                    else
                    {
                        _game.Render -= OnRender;
                        _game.Update -= OnUpdate;
                    }

                    var args = EventArgs.Empty;
                    if (IsEnabledChanged != null)
                        IsEnabledChanged(this, args);
                }
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
        /// <param name="game"></param>
        protected GameComponent(Game game)
        {
            if (game == null)
                throw new ArgumentException();

            _game = game;
            IsEnabled = true;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnUpdate(object sender, FrameEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnRender(object sender, FrameEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IsEnabled = false;
                _game = null;
                _isDisposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ~GameComponent()
        {
            Dispose(false);
        }
    }
}
