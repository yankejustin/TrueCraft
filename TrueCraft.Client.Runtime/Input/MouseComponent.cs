using System;

using OpenTK;
using OpenTK.Input;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MouseComponent
        : GameComponent
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> Move;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonDown;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonUp;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MouseScrollEventArgs> Scroll;

        private MouseState _state;

        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _state.X;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _state.Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Wheel
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return _state.WheelPrecise;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public MouseComponent(Game game)
            : base(game)
        {
            Game.Window.MouseMove += OnMouseMove;
            Game.Window.MouseDown += OnMouseDown;
            Game.Window.MouseUp += OnMouseUp;
            Game.Window.MouseWheel += OnMouseWheel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool IsButtonDown(MouseButton button)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            return _state.IsButtonDown((OpenTK.Input.MouseButton)button);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool IsButtonUp(MouseButton button)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            return _state.IsButtonUp((OpenTK.Input.MouseButton)button);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            _state = e.Mouse;
            if (IsEnabled)
            {
                var args = new MouseMoveEventArgs(e.X, e.Y, e.XDelta, e.YDelta);
                if (Move != null)
                    Move(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            _state = e.Mouse;
            if (IsEnabled)
            {
                var args = new MouseButtonEventArgs(e.X, e.Y, (MouseButton)e.Button, true);
                if (ButtonDown != null)
                    ButtonDown(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            _state = e.Mouse;
            if (IsEnabled)
            {
                var args = new MouseButtonEventArgs(e.X, e.Y, (MouseButton)e.Button, false);
                if (ButtonUp != null)
                    ButtonUp(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            _state = e.Mouse;
            if (IsEnabled)
            {
                var args = new MouseScrollEventArgs(e.X, e.Y, e.ValuePrecise, e.DeltaPrecise);
                if (Scroll != null)
                    Scroll(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Game.Window.MouseWheel -= OnMouseWheel;
                Game.Window.MouseUp -= OnMouseUp;
                Game.Window.MouseDown -= OnMouseDown;
                Game.Window.MouseMove -= OnMouseMove;
            }

            base.Dispose(disposing);
        }
    }
}
