using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace TrueCraft.Client.Input
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyModifiers
        : IEquatable<KeyModifiers>
    {
        private bool _alt, _control, _shift;

        /// <summary>
        /// 
        /// </summary>
        public bool Alt
        {
            get { return _alt; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Control
        {
            get { return _control; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Shift
        {
            get { return _shift; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alt"></param>
        /// <param name="control"></param>
        /// <param name="shift"></param>
        public KeyModifiers(bool alt, bool control, bool shift)
        {
            _alt = alt;
            _control = control;
            _shift = shift;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(KeyModifiers other)
        {
            if ((object)other == null)
                return false;

            return this == other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            KeyModifiers cast = (KeyModifiers)obj;
            return this == cast;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 397;
                hash = (hash * 397) ^ _alt.GetHashCode();
                hash = (hash * 397) ^ _control.GetHashCode();
                hash = (hash * 397) ^ _shift.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(
                "[{0}, {1}, {2}]",
                _alt, _control, _shift);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(KeyModifiers left, KeyModifiers right)
        {
            return
                (left._alt == right._alt) &&
                (left._control == right._control) &&
                (left._shift == right._shift);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(KeyModifiers left, KeyModifiers right)
        {
            return
                (left._alt != right._alt) ||
                (left._control != right._control) ||
                (left._shift != right._shift);
        }
    }
}
