using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
        : IEquatable<Vector2>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector2 Zero = new Vector2(0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector2 Right = new Vector2(1.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector2 Left = new Vector2(-1.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector2 Up = new Vector2(0.0f, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector2 Down = new Vector2(0.0f, -1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector2 One = new Vector2(1.0f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2 Lerp(Vector2 start, Vector2 end, float value)
        {
            return new Vector2(
                start._x + (end._x - start._x) * value,
                start._y + (end._y - start._y) * value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float Dot(Vector2 left, Vector2 right)
        {
            return
                (left._x * right._x) +
                (left._y * right._y);
        }

        private float _x, _y;

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
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(
                    (_x * _x) +
                    (_y * _y));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Vector2(float value)
        {
            _x = value;
            _y = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 Negate()
        {
            return new Vector2(
                -_x,
                -_y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 Normal()
        {
            float l = Length;
            return new Vector2(
                _x / l,
                _y / l);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector2 other)
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

            Vector2 cast = (Vector2)obj;
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
                "[{0}, {1}]",
                _x, _y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return
                (left._x == right._x) &&
                (left._y == right._y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return
                (left._x != right._x) ||
                (left._y != right._y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left._x + right._x,
                left._y + right._y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left._x - right._x,
                left._y - right._y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left._x * right._x,
                left._y * right._y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(
                left._x * right,
                left._y * right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left._x / right._x,
                left._y / right._y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(
                left._x / right,
                left._y / right);
        }
    }
}
