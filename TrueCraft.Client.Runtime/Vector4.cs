using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4
        : IEquatable<Vector4>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector4 Zero = new Vector4(0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector4 One = new Vector4(1.0f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector4 Lerp(Vector4 start, Vector4 end, float value)
        {
            return new Vector4(
                start._x + (end._x - start._x) * value,
                start._y + (end._y - start._y) * value,
                start._z + (end._z - start._z) * value,
                start._w + (end._w - start._w) * value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float Dot(Vector4 left, Vector4 right)
        {
            return
                (left._x * right._x) +
                (left._y * right._y) +
                (left._z * right._z) +
                (left._w * right._w);
        }

        private float _x, _y, _z, _w;

        /// <summary>
        /// 
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Z
        {
            get { return _z; }
            set { _z = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float W
        {
            get { return _w; }
            set { _w = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(
                    (_x * _x) +
                    (_y * _y) +
                    (_z * _z) +
                    (_w * _w));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Vector4(float value)
        {
            _x = value;
            _y = value;
            _z = value;
            _w = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector4 Negate()
        {
            return new Vector4(
                -_x,
                -_y,
                -_z,
                -_w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector4 Normal()
        {
            float l = Length;
            return new Vector4(
                _x / l,
                _y / l,
                _z / l,
                _w / l);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector4 other)
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

            Vector4 cast = (Vector4)obj;
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
                hash = (hash * 397) ^ _x.GetHashCode();
                hash = (hash * 397) ^ _y.GetHashCode();
                hash = (hash * 397) ^ _z.GetHashCode();
                hash = (hash * 397) ^ _w.GetHashCode();
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
                _x, _y, _z, _w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return
                (left._x == right._x) &&
                (left._y == right._y) &&
                (left._z == right._z) &&
                (left._w == right._w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return
                (left._x != right._x) ||
                (left._y != right._y) ||
                (left._z != right._z) ||
                (left._w != right._w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left._x + right._x,
                left._y + right._y,
                left._z + right._z,
                left._w + right._w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left._x - right._x,
                left._y - right._y,
                left._z - right._z,
                left._w - right._w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left._x * right._x,
                left._y * right._y,
                left._z * right._z,
                left._w * right._w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector4 operator *(Vector4 left, float right)
        {
            return new Vector4(
                left._x * right,
                left._y * right,
                left._z * right,
                left._w * right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector4 operator /(Vector4 left, Vector4 right)
        {
            return new Vector4(
                left._x / right._x,
                left._y / right._y,
                left._z / right._z,
                left._w / right._w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector4 operator /(Vector4 left, float right)
        {
            return new Vector4(
                left._x / right,
                left._y / right,
                left._z / right,
                left._w / right);
        }
    }
}
