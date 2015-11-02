﻿using System;
using TrueCraft.API.Logic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TrueCraft.Client.Rendering
{
    public abstract class FlatQuadRenderer : BlockRenderer
    {
        protected virtual Vector2 TextureMap { get { return Vector2.Zero; } }
        protected Vector2[] Texture;

        protected FlatQuadRenderer()
        {
            Texture = new[]
            {
                TextureMap + Vector2.UnitX + Vector2.UnitY,
                TextureMap + Vector2.UnitY,
                TextureMap,
                TextureMap + Vector2.UnitX,
            };
            for (int i = 0; i < Texture.Length; i++)
                Texture[i] *= new Vector2(16f / 256f);
        }

        public override VertexPositionNormalColorTexture[] Render(BlockDescriptor descriptor, Vector3 offset,
            VisibleFaces faces, Tuple<int, int> textureMap, int indiciesOffset, out int[] indicies)
        {
            return RenderQuads(descriptor, offset, Texture, indiciesOffset, out indicies, Color.White);
        }

        protected VertexPositionNormalColorTexture[] RenderQuads(BlockDescriptor descriptor, Vector3 offset,
            Vector2[] textureMap, int indiciesOffset, out int[] indicies, Color color)
        {
            indicies = new int[6 * 4];
            var verticies = new VertexPositionNormalColorTexture[4 * 4];
            int[] _indicies;
            int textureIndex = 0;
            for (int side = 0; side < 4; side++)
            {
                var quad = CreateAngledQuad(side, offset, textureMap, textureIndex % textureMap.Length, indiciesOffset, out _indicies, color);
                Array.Copy(quad, 0, verticies, side * 4, 4);
                Array.Copy(_indicies, 0, indicies, side * 6, 6);
                textureIndex += 4;
            }
            return verticies;
        }

        protected static VertexPositionNormalColorTexture[] CreateAngledQuad(int face, Vector3 offset, Vector2[] texture, int textureOffset,
            int indiciesOffset, out int[] indicies, Color color)
        {
            indicies = new[] { 0, 1, 3, 1, 2, 3 };
            for (int i = 0; i < indicies.Length; i++)
                indicies[i] += (face * 4) + indiciesOffset;
            var quad = new VertexPositionNormalColorTexture[4];
            var unit = QuadMesh[face];
            var normal = CubeNormals[face];
            for (int i = 0; i < 4; i++)
            {
                quad[i] = new VertexPositionNormalColorTexture(offset + unit[i], normal, color, texture[textureOffset + i]);
            }
            return quad;
        }

        protected static readonly Vector3[] QuadNormals =
        {
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0)
        };

        protected static readonly Vector3[][] QuadMesh;

        static FlatQuadRenderer()
        {
            QuadMesh = new Vector3[4][];

            QuadMesh[0] = new[]
            {
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 1)
            };

            QuadMesh[1] = new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(1, 1, 1),
                new Vector3(0, 1, 0)
            };

            QuadMesh[2] = new[]
            {
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 0),
                new Vector3(1, 1, 0),
                new Vector3(0, 1, 1)
            };

            QuadMesh[3] = new[]
            {
                new Vector3(1, 0, 0),
                new Vector3(0, 0, 1),
                new Vector3(0, 1, 1),
                new Vector3(1, 1, 0)
            };
        }
    }
}