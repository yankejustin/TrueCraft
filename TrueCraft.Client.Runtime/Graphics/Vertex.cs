using System;
using System.Runtime;
using System.Runtime.InteropServices;
using TrueCraft.Client.Maths;

namespace TrueCraft.Client.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vertex Empty = new Vertex(
            Vector3.Zero, Vector3.Zero, Color.White, Vector2.Zero);

        private Vector3 _position, _normal;
        private Color _color;
        private Vector2 _texCoord;

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Normal
        {
            get { return _normal; }
            set { _normal = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 TexCoord
        {
            get { return _texCoord; }
            set { _texCoord = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="normal"></param>
        /// <param name="color"></param>
        /// <param name="texCoord"></param>
        public Vertex(Vector3 position, Vector3 normal, Color color, Vector2 texCoord)
        {
            _position = position;
            _normal = normal;
            _color = color;
            _texCoord = texCoord;
        }
    }
}
