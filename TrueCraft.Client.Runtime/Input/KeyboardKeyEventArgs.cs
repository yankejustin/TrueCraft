using System;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardKeyEventArgs
        : KeyboardEventArgs
    {
        private Key _key;
        private bool _isPressed;
        private KeyModifiers _keyModifiers;
        private bool _isRepeat;

        /// <summary>
        /// 
        /// </summary>
        public Key Key
        {
            get { return _key; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPressed
        {
            get { return _isPressed; }
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyModifiers KeyModifiers
        {
            get { return _keyModifiers; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRepeat
        {
            get { return _isRepeat; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isPressed"></param>
        /// <param name="keyModifiers"></param>
        /// <param name="isRepeat"></param>
        public KeyboardKeyEventArgs(Key key, bool isPressed, KeyModifiers keyModifiers, bool isRepeat)
            : base()
        {
            _key = key;
            _isPressed = isPressed;
            _keyModifiers = keyModifiers;
            _isRepeat = isRepeat;
        }
    }
}
