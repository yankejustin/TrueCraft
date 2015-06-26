using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace TrueCraft.Client
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
        : IEquatable<Vector3>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Zero = new Vector3(0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Right = new Vector3(1.0f, 0.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Left = new Vector3(-1.0f, 0.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Down = new Vector3(0.0f, -1.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Back = new Vector3(0.0f, 0.0f, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 Front = new Vector3(0.0f, 0.0f, -1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3 One = new Vector3(1.0f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 start, Vector3 end, float value)
        {
            return new Vector3(
                start._x + (end._x - start._x) * value,
                start._y + (end._y - start._y) * value,
                start._z + (end._z - start._z) * value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            return new Vector3(
                (left._y * right._z) - (left._z * right._y),
                (left._z * right._x) - (left._x * right._z),
                (left._x * right._y) - (left._y * right._x));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float Dot(Vector3 left, Vector3 right)
        {
            return
                (left._x * right._x) +
                (left._y * right._y) +
                (left._z * right._z);
        }

        private float _x, _y, _z;

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
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(
                    (_x * _x) +
                    (_y * _y) +
                    (_z * _z));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Vector3(float value)
        {
            _x = value;
            _y = value;
            _z = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector3 Negate()
        {
            return new Vector3(
                -_x,
                -_y,
                -_z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector3 Normal()
        {
            float l = Length;
            return new Vector3(
                _x / l,
                _y / l,
                _z / l);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector3 other)
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

            Vector3 cast = (Vector3)obj;
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
                _x, _y, _z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return
                (left._x == right._x) &&
                (left._y == right._y) &&
                (left._z == right._z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return
                (left._x != right._x) ||
                (left._y != right._y) ||
                (left._z != right._z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left._x + right._x,
                left._y + right._y,
                left._z + right._z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left._x - right._x,
                left._y - right._y,
                left._z - right._z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left._x * right._x,
                left._y * right._y,
                left._z * right._z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 left, float right)
        {
            return new Vector3(
                left._x * right,
                left._y * right,
                left._z * right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left._x / right._x,
                left._y / right._y,
                left._z / right._z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 left, float right)
        {
            return new Vector3(
                left._x / right,
                left._y / right,
                left._z / right);
        }
    }
}
