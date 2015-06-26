using System;

using OpenTK;
using OpenTK.Input;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class KeyboardComponent
        : GameComponent
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyDown;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyUp;

        private KeyboardState _state;

        /// <summary>
        /// 
        /// </summary>
        public KeyModifiers KeyModifiers
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(GetType().Name);

                return new KeyModifiers(
                    (_state[OpenTK.Input.Key.AltLeft] || _state[OpenTK.Input.Key.AltRight]),
                    (_state[OpenTK.Input.Key.ControlLeft] || _state[OpenTK.Input.Key.ControlRight]),
                    (_state[OpenTK.Input.Key.ShiftLeft] || _state[OpenTK.Input.Key.ShiftRight]));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public KeyboardComponent(Game game)
            : base(game)
        {
            Game.Window.KeyDown += OnKeyDown;
            Game.Window.KeyUp += OnKeyUp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyDown(Key key)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            return _state.IsKeyDown((OpenTK.Input.Key)key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyUp(Key key)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            return _state.IsKeyUp((OpenTK.Input.Key)key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            _state = e.Keyboard;
            if (IsEnabled)
            {
                var args = new KeyboardKeyEventArgs((Key)e.Key, true, KeyModifiers, e.IsRepeat);
                if (KeyDown != null)
                    KeyDown(this, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            _state = e.Keyboard;
            if (IsEnabled)
            {
                var args = new KeyboardKeyEventArgs((Key)e.Key, false, KeyModifiers, e.IsRepeat);
                if (KeyUp != null)
                    KeyUp(this, args);
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
                Game.Window.KeyUp -= OnKeyUp;
                Game.Window.KeyDown -= OnKeyDown;
            }

            base.Dispose(disposing);
        }
    }
}
