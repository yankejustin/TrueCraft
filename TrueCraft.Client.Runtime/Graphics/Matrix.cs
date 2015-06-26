using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix
        : IEquatable<Matrix>
    {
        /// <summary>
        /// 
        /// </summary>
        public const int Rows = 4;

        /// <summary>
        /// 
        /// </summary>
        public const int Columns = 4;

        /// <summary>
        /// 
        /// </summary>
        public static readonly Matrix Zero = new Matrix(
            0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Matrix Identity = new Matrix(
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row0"></param>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <param name="row3"></param>
        /// <returns></returns>
        public static Matrix FromRows(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            return new Matrix(
                row0.X, row0.Y, row0.Z, row0.W,
                row1.X, row1.Y, row1.Z, row1.W,
                row2.X, row2.Y, row2.Z, row2.W,
                row3.X, row3.Y, row3.Z, row3.W);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col0"></param>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="col3"></param>
        /// <returns></returns>
        public static Matrix FromColumns(Vector4 col0, Vector4 col1, Vector4 col2, Vector4 col3)
        {
            return new Matrix(
                col0.X, col1.X, col2.X, col3.X,
                col0.Y, col1.Y, col2.Y, col3.Y,
                col0.Z, col1.Z, col2.Z, col3.Z,
                col0.W, col1.W, col2.W, col3.W);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        public static Matrix CreateOrthographic(float width, float height, float near, float far)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            var left = -width / 2.0f;
            var right = width / 2.0f;
            var bottom = -height / 2.0f;
            var top = height / 2.0f;

            var invRL = 1.0f / (right - left);
            var invTB = 1.0f / (top - bottom);
            var invFN = 1.0f / (far - near);

            return new Matrix(
                            2.0f * invRL,                     0.0f,                   0.0f, 0.0f,
                                    0.0f,             2.0f * invTB,                   0.0f, 0.0f,
                                    0.0f,                     0.0f,          -2.0f * invFN, 0.0f,
                 -(right + left) * invRL,  -(top + bottom) * invTB,  -(far + near) * invFN, 1.0f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fov"></param>
        /// <param name="aspect"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        public static Matrix CreatePerspective(float fov, float aspect, float near, float far)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            var left = near * (float)Math.Tan(0.5f * fov);
            var right = -left;
            var bottom = right * aspect;
            var top = left * aspect;

            return new Matrix(
                 (2.0f * near) / (right - left),                            0.0f,                                0.0f,  0.0f,
                                           0.0f,  (2.0f * near) / (top - bottom),                                0.0f,  0.0f,
                (right + left) / (right - left), (top + bottom) / (top - bottom),        -(far + near) / (far - near), -1.0f,
                                           0.0f,                            0.0f, (-2.0f * far * near) / (far - near),  0.0f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public static Matrix CreateTranslation(Vector3 value)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            return new Matrix(
                    1.0f,     0.0f,     0.0f, 0.0f,
                    0.0f,     1.0f,     0.0f, 0.0f,
                    0.0f,     0.0f,     1.0f, 0.0f,
                value.X,   value.Y,  value.Z, 1.0f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix CreateRotationX(float value)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            float cos = (float)Math.Cos(value);
            float sin = (float)Math.Sin(value);

            return new Matrix(
                1.0f,  0.0f, 0.0f, 0.0f,
                0.0f,  cos, sin, 0.0f,
                0.0f, -sin, cos, 0.0f,
                0.0f,  0.0f, 0.0f, 1.0f);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix CreateRotationY(float value)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            float cos = (float)Math.Cos(value);
            float sin = (float)Math.Sin(value);

            return new Matrix(
                cos, 0.0f, -sin, 0.0f,
                0.0f, 1.0f,  0.0f, 0.0f,
                sin, 0.0f,  cos, 0.0f,
                0.0f, 0.0f,  0.0f, 1.0f);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix CreateRotationZ(float value)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            float cos = (float)Math.Cos(value);
            float sin = (float)Math.Sin(value);

            return new Matrix(
                 cos, sin, 0.0f, 0.0f,
                -sin, cos, 0.0f, 0.0f,
                 0.0f, 0.0f, 1.0f, 0.0f,
                 0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix CreateScale(Vector3 value)
        {
            // Taken from the OpenTK project.
            // See [https://github.com/mono/opentk/blob/master/Source/OpenTK/Math/Matrix4.cs]

            return new Matrix(
                value.X,     0.0f,   0.0f, 0.0f,
                   0.0f, value.Y,    0.0f, 0.0f,
                   0.0f,    0.0f, value.Z, 0.0f,
                   0.0f,    0.0f,    0.0f, 1.0f);
        }

        private float[,] _values;

        /// <summary>
        /// 
        /// </summary>
        public float Determinant
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public float this[int x, int y]
        {
            get
            {
                if ((x < 0) || (x >= Matrix.Rows) || (y < 0) || (y >= Matrix.Columns))
                    throw new IndexOutOfRangeException();

                return _values[x, y];
            }
            set
            {
                if ((x < 0) || (x >= Matrix.Rows) || (y < 0) || (y >= Matrix.Columns))
                    throw new IndexOutOfRangeException();

                _values[x, y] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public Matrix(params float[] values)
        {
            if (values.Length != (Matrix.Rows * Matrix.Columns))
                throw new ArgumentException();

            _values = new float[Matrix.Rows, Matrix.Columns];
            _values[0, 0] = values[0];  _values[1, 0] = values[1];  _values[2, 0] = values[2];  _values[3, 0] = values[3];
            _values[0, 1] = values[4];  _values[1, 1] = values[5];  _values[2, 1] = values[6];  _values[3, 1] = values[7];
            _values[0, 2] = values[8];  _values[1, 2] = values[9];  _values[2, 2] = values[10]; _values[3, 2] = values[11];
            _values[0, 3] = values[12]; _values[1, 3] = values[13]; _values[2, 3] = values[14]; _values[3, 3] = values[15];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix Inverse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            Matrix result = Matrix.Zero;
            for (int i = 0; i < Matrix.Rows; i++)
                for (int j = 0; j < Matrix.Columns; j++)
                    result._values[j, i] = _values[i, j];

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Matrix other)
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

            Matrix cast = (Matrix)obj;
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
                for (int i = 0; i < Matrix.Rows; i++)
                    for (int j = 0; j < Matrix.Columns; j++)
                        hash = (hash * 397) ^ _values[i, j].GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "[";
            for (int i = 0; i < Matrix.Rows; i++)
            {
                for (int j = 0; j < Matrix.Columns; j++)
                    result += _values[i, j].ToString();
                result += "\n ";
            }
            result = (result.Remove(result.Length - 2) + "]");
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float[] ToArray()
        {
            return new float[]
            {
                _values[0, 0], _values[1, 0], _values[2, 0], _values[3, 0],
                _values[0, 1], _values[1, 1], _values[2, 1], _values[3, 1],
                _values[0, 2], _values[1, 2], _values[2, 2], _values[3, 2],
                _values[0, 3], _values[1, 3], _values[2, 3], _values[3, 3]
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix left, Matrix right)
        {
            bool result = true;
            for (int i = 0; i < Matrix.Rows; i++)
                for (int j = 0; j < Matrix.Columns; j++)
                    result = result && (left._values[i, j] == right._values[i, j]);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix left, Matrix right)
        {
            bool result = false;
            for (int i = 0; i < Matrix.Rows; i++)
                for (int j = 0; j < Matrix.Columns; j++)
                    result = result || (left._values[i, j] != right._values[i, j]);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            Matrix result = Matrix.Zero;
            for (int i = 0; i < Matrix.Rows; i++)
            {
                for (int j = 0; j < Matrix.Columns; j++)
                {
                    result._values[i, j] =
                        left._values[i, 0] * right._values[0, j] +
                        left._values[i, 1] * right._values[1, j] +
                        left._values[i, 2] * right._values[2, j] +
                        left._values[i, 3] * right._values[3, j];
                }
            }

            return result;
        }
    }
}
