using System;
using TrueCraft.API.Logic;

namespace TrueCraft.Core.Logic.Items
{
    public class FlintItem : ItemProvider
    {
        public static readonly short ItemID = 0x13E;

        public override short ID { get { return 0x13E; } }

        public override Tuple<int, int> GetIconTexture(byte metadata)
        {
            return new Tuple<int, int>(6, 0);
        }

        public override string DisplayName { get { return "Flint"; } }
    }
}