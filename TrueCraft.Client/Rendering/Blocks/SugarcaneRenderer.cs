using System;
using TrueCraft.Core.Logic.Blocks;
using TrueCraft.API.Logic;
using TrueCraft.Client.Maths;

namespace TrueCraft.Client.Rendering.Blocks
{
    public class SugarcaneRenderer : FlatQuadRenderer
    {
        static SugarcaneRenderer()
        {
            BlockRenderer.RegisterRenderer(SugarcaneBlock.BlockID, new SugarcaneRenderer());
        }

        protected override Vector2 TextureMap { get { return new Vector2(9, 4); } }
    }
}