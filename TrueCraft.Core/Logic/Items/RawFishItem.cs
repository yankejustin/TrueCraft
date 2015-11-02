using System;
using TrueCraft.API.Logic;

namespace TrueCraft.Core.Logic.Items
{
    public class RawFishItem : FoodItem
    {
        public static readonly short ItemID = 0x15D;

        public override short ID { get { return 0x15D; } }

        public override Tuple<int, int> GetIconTexture(byte metadata)
        {
            return new Tuple<int, int>(9, 5);
        }

        public override float Restores { get { return 1; } }

        public override string DisplayName { get { return "Raw Fish"; } }
    }
}