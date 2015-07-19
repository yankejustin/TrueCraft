﻿using System;
using TrueCraft.Core.Logic.Blocks;
using TrueCraft.API.Logic;
using TrueCraft.Client.Maths;
using TrueCraft.Client.Graphics;

namespace TrueCraft.Client.Rendering.Blocks
{
    public class TNTRenderer : BlockRenderer
    {
        static TNTRenderer()
        {
            BlockRenderer.RegisterRenderer(TNTBlock.BlockID, new TNTRenderer());
            for (int i = 0; i < Texture.Length; i++)
                Texture[i] *= new Vector2(16f / 256f);
        }

        private static Vector2 TopTexture = new Vector2(9, 0);
        private static Vector2 BottomTexture = new Vector2(10, 0);
        private static Vector2 SideTexture = new Vector2(8, 0);
        private static Vector2[] Texture =
        {
            // Positive Z
            SideTexture + Vector2.UnitX + Vector2.UnitY,
            SideTexture + Vector2.UnitY,
            SideTexture,
            SideTexture + Vector2.UnitX,
            // Negative Z
            SideTexture + Vector2.UnitX + Vector2.UnitY,
            SideTexture + Vector2.UnitY,
            SideTexture,
            SideTexture + Vector2.UnitX,
            // Positive X
            SideTexture + Vector2.UnitX + Vector2.UnitY,
            SideTexture + Vector2.UnitY,
            SideTexture,
            SideTexture + Vector2.UnitX,
            // Negative X
            SideTexture + Vector2.UnitX + Vector2.UnitY,
            SideTexture + Vector2.UnitY,
            SideTexture,
            SideTexture + Vector2.UnitX,
            // Negative Y
            TopTexture + Vector2.UnitX + Vector2.UnitY,
            TopTexture + Vector2.UnitY,
            TopTexture,
            TopTexture + Vector2.UnitX,
            // Negative Y
            BottomTexture + Vector2.UnitX + Vector2.UnitY,
            BottomTexture + Vector2.UnitY,
            BottomTexture,
            BottomTexture + Vector2.UnitX,
        };

        public override Vertex[] Render(BlockDescriptor descriptor, Vector3 offset,
            Tuple<int, int> textureMap, int indiciesOffset, out uint[] indicies)
        {
            return CreateUniformCube(offset, Texture, indiciesOffset, out indicies, Color.White);
        }
    }
}