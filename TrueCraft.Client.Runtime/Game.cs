using System;

using OpenTK;
using OpenTK.Graphics;

using TrueCraft;
using TrueCraft.Client;
using TrueCraft.Client.Input;
using TrueCraft.Client.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Game : IDisposable
    {
        public event EventHandler<DisposedEventArgs> Disposed;
        public event EventHandler<FrameEventArgs> Update;
        public event EventHandler<FrameEventArgs> Render;

        private bool _isRunning;
        private string[] _arguments;
        private GameWindow _window;
        private double _updateTime, _renderTime;
        private GraphicsComponent _graphicsComponent;
        private MouseComponent _mouseComponent;
        private KeyboardComponent _keyboardComponent;
        private bool _isDisposed;

        public bool IsRunning
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _isRunning;
            }
        }

        public string[] Arguments
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _arguments;
            }
        }

        public GameWindow Window
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _window;
            }
        }

        public GraphicsComponent GraphicsComponent
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _graphicsComponent;
            }
        }

        public MouseComponent MouseComponent
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _mouseComponent;
            }
        }

        public KeyboardComponent KeyboardComponent
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _keyboardComponent;
            }
        }

        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

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

        public void Run(params string[] args)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!_isRunning)
            {
                try
                {
                    Initialize();

                    _isRunning = true;
                    _arguments = args;

                    _window.Run(60.0); // I think this is updates per second?
                }
                finally
                {
                    _arguments = null;
                    _isRunning = false;
                }
            }
        }

        public void Quit()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (_isRunning)
                _window.Close();
        }

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

        protected virtual void OnLoad(object sender, EventArgs e)
        {
            _graphicsComponent = new GraphicsComponent(this);
            _graphicsComponent.IsEnabled = true;
            _mouseComponent = new MouseComponent(this);
            _mouseComponent.IsEnabled = true;
            _keyboardComponent = new KeyboardComponent(this);
            _keyboardComponent.IsEnabled = true;
        }

        protected virtual void OnUpdate(object sender, FrameEventArgs e) { }

        protected virtual void OnRender(object sender, FrameEventArgs e) { }

        protected virtual void OnUnload(object sender, EventArgs e)
        {
            _keyboardComponent.IsEnabled = false;
            _keyboardComponent.Dispose();
            _mouseComponent.IsEnabled = false;
            _mouseComponent.Dispose();
            _graphicsComponent.IsEnabled = false;
            _graphicsComponent.Dispose();
        }

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

        private void OnUpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {
            _updateTime += e.Time;

            var args = new FrameEventArgs(_updateTime, e.Time);
            OnUpdate(this, args);
            if (Update != null)
                Update(this, args);
        }

        private void OnRenderFrame(object sender, OpenTK.FrameEventArgs e)
        {
            _renderTime += e.Time;

            var args = new FrameEventArgs(_renderTime, e.Time);
            OnRender(this, args);
            if (Render != null)
                Render(this, args);

            _window.SwapBuffers();
        }

        protected virtual void Initialize()
        {
        }

        ~Game()
        {
            Dispose(true);
        }
    }
}
