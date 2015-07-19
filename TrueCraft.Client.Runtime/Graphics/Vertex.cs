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

        public Vector3 Position;
        public Vector3 Normal;
        public Color Color;
        public Vector2 TexCoord;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="normal"></param>
        /// <param name="color"></param>
        /// <param name="texCoord"></param>
        public Vertex(Vector3 position, Vector3 normal, Color color, Vector2 texCoord)
        {
            Position = position;
            Normal = normal;
            Color = color;
            TexCoord = texCoord;
        }
    }
}
