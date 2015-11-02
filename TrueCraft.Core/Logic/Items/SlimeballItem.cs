using System;
using TrueCraft.API.Logic;

namespace TrueCraft.Core.Logic.Items
{
    public class SlimeballItem : ItemProvider
    {
        public static readonly short ItemID = 0x155;

        public override short ID { get { return 0x155; } }

        public override Tuple<int, int> GetIconTexture(byte metadata)
        {
            return new Tuple<int, int>(14, 1);
        }

        public override string DisplayName { get { return "Slimeball"; } }
    }
}