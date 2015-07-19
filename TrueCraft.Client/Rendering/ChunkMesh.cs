using System;
using Microsoft.Xna.Framework.Graphics;
using TrueCraft.Core.World;
using TrueCraft.Client.Graphics;
using TrueCraft.API;

namespace TrueCraft.Client.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class ChunkMesh : Mesh
    {
        public ReadOnlyChunk Chunk { get; set; }

        public ChunkMesh(ReadOnlyChunk chunk, TrueCraftGame game, Vertex[] vertices, uint[] indices)
            : base(1)
        {
            Chunk = chunk;
            Vertices = vertices;
            SetSubmesh(0, indices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="device"></param>
        /// <param name="vertices"></param>
        /// <param name="opaqueIndices"></param>
        /// <param name="transparentIndices"></param>
        public ChunkMesh(ReadOnlyChunk chunk, TrueCraftGame game,
            Vertex[] vertices, uint[] opaqueIndices, uint[] transparentIndices)
            : base(2)
        {
            Chunk = chunk;
            Vertices = vertices;
            SetSubmesh(0, opaqueIndices);
            SetSubmesh(1, transparentIndices);
        }

        protected BoundingBox RecalculateBounds(Vertex[] vertices)
        {
            return new BoundingBox(
                new Vector3(Chunk.X * TrueCraft.Core.World.Chunk.Width, 0, Chunk.Z * TrueCraft.Core.World.Chunk.Depth),
                new Vector3(Chunk.X * TrueCraft.Core.World.Chunk.Width
                    + TrueCraft.Core.World.Chunk.Width, TrueCraft.Core.World.Chunk.Height,
                    Chunk.Z * TrueCraft.Core.World.Chunk.Depth + TrueCraft.Core.World.Chunk.Depth));
        }
    }
}