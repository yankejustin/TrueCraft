using System;
using TrueCraft.API.Logic;

namespace TrueCraft.Core.Logic.Items
{
    public class DiamondItem : ItemProvider
    {
        public static readonly short ItemID = 0x108;

        public override short ID { get { return 0x108; } }

        public override Tuple<int, int> GetIconTexture(byte metadata)
        {
            return new Tuple<int, int>(7, 3);
        }

        public override string DisplayName { get { return "Diamond"; } }
    }
}