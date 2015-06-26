using System;

using OpenTK;
using OpenTK.Graphics;

using TrueCraft;
using TrueCraft.Client;
using TrueCraft.Client.Input;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Game
        : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DisposedEventArgs> Disposed;

        /// <summary>
        /// 
        /// </summary>
        internal event EventHandler<FrameEventArgs> Update;

        /// <summary>
        /// 
        /// </summary>
        internal event EventHandler<FrameEventArgs> Render;

        private bool _isRunning;
        private string[] _arguments;
        private GameWindow _window;
        private double _updateTime, _renderTime;
        private MouseComponent _mouseComponent;
        private KeyboardComponent _keyboardComponent;
        private bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _isRunning;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Arguments
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _arguments;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal GameWindow Window
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _window;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MouseComponent MouseComponent
        {
            get
            {
                if (this._isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return this._mouseComponent;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyboardComponent KeyboardComponent
        {
            get
            {
                if (this._isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return this._keyboardComponent;
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
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        /// <param name="isFullscreen"></param>
        protected Game(int width, int height, string title, bool isFullscreen = false)
        {
            _isRunning = false;
            _arguments = null;
            _window = new GameWindow(
                width, height, GraphicsMode.Default, title,
                (isFullscreen) ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow,
                DisplayDevice.Default, 3, 2, GraphicsContextFlags.Default);
            _isDisposed = false;

            _window.Load += OnLoad;
            _window.UpdateFrame += OnUpdateFrame;
            _window.RenderFrame += OnRenderFrame;
            _window.Unload += OnUnload;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Run(params string[] args)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!_isRunning)
            {
                try
                {
                    _isRunning = true;
                    _arguments = args;

                    _window.Run(20.0);
                }
                catch { throw; }
                finally
                {
                    _arguments = null;
                    _isRunning = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Quit()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (_isRunning)
                _window.Close();
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
        protected virtual void OnLoad(object sender, EventArgs e)
        {
            _mouseComponent = new MouseComponent(this);
            _mouseComponent.IsEnabled = true;
            _keyboardComponent = new KeyboardComponent(this);
            _keyboardComponent.IsEnabled = true;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnUnload(object sender, EventArgs e)
        {
            _keyboardComponent.IsEnabled = false;
            _keyboardComponent.Dispose();
            _mouseComponent.IsEnabled = false;
            _mouseComponent.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_isRunning)
                    Quit();

                _window.Unload -= OnUnload;
                _window.RenderFrame -= OnRenderFrame;
                _window.UpdateFrame -= OnUpdateFrame;
                _window.Load -= OnLoad;

                _window.Dispose();
                _isDisposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {
            _updateTime += e.Time;

            var args = new FrameEventArgs(_updateTime, e.Time);
            OnUpdate(this, args);
            if (Update != null)
                Update(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRenderFrame(object sender, OpenTK.FrameEventArgs e)
        {
            _renderTime += e.Time;

            var args = new FrameEventArgs(_renderTime, e.Time);
            OnRender(this, args);
            if (Render != null)
                Render(this, args);

            _window.SwapBuffers();
        }

        /// <summary>
        /// 
        /// </summary>
        ~Game()
        {
            Dispose(true);
        }
    }
}
