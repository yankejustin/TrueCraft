using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Color
        : IEquatable<Color>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Color White = new Color(255, 255, 255);

        private byte _r, _g, _b, _a;

        /// <summary>
        /// 
        /// </summary>
        public byte R
        {
            get { return _r; }
            set { _r = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte G
        {
            get { return _g; }
            set { _g = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte B
        {
            get { return _b; }
            set { _b = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte A
        {
            get { return _a; }
            set { _a = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Color other)
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

            Color cast = (Color)obj;
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
                hash = (hash * 397) ^ _r.GetHashCode();
                hash = (hash * 397) ^ _g.GetHashCode();
                hash = (hash * 397) ^ _b.GetHashCode();
                hash = (hash * 397) ^ _a.GetHashCode();
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
                "[{0}, {1}, {2}, {3}]",
                _r, _g, _b, _a);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Color left, Color right)
        {
            return
                (left._r == right._r) &&
                (left._g == right._g) &&
                (left._b == right._b) &&
                (left._a == right._a);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Color left, Color right)
        {
            return
                (left._r != right._r) ||
                (left._g != right._g) ||
                (left._b != right._b) ||
                (left._a != right._a);
        }
    }
}
