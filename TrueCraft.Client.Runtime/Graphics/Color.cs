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
        public static readonly Color Black = new Color(0, 0, 0);

        public static readonly Color White = new Color(255, 255, 255);

        private byte _r, _g, _b, _a;

        public byte R
        {
            get { return _r; }
            set { _r = value; }
        }

        public byte G
        {
            get { return _g; }
            set { _g = value; }
        }

        public byte B
        {
            get { return _b; }
            set { _b = value; }
        }

        public byte A
        {
            get { return _a; }
            set { _a = value; }
        }

        public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = a;
        }

        public Color(uint color)
        {
            _r = (byte)((color & 0x000000FF) >> 0);
            _g = (byte)((color & 0x0000FF00) >> 8);
            _b = (byte)((color & 0x00FF0000) >> 16);
            _a = (byte)((color & 0xFF000000) >> 24);
        }

        public bool Equals(Color other)
        {
            if ((object)other == null)
                return false;

            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            Color cast = (Color)obj;
            return this == cast;
        }

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

        public override string ToString()
        {
            return String.Format(
                "[{0}, {1}, {2}, {3}]",
                _r, _g, _b, _a);
        }

        public static bool operator ==(Color left, Color right)
        {
            return
                (left._r == right._r) &&
                (left._g == right._g) &&
                (left._b == right._b) &&
                (left._a == right._a);
        }

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
