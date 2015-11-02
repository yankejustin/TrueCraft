using System;
using TrueCraft.API.Logic;

namespace TrueCraft.Core.Logic.Blocks
{
    public class SoulSandBlock : BlockProvider
    {
        public static readonly byte BlockID = 0x58;
        
        public override byte ID { get { return 0x58; } }
        
        public override double BlastResistance { get { return 2.5; } }

        public override double Hardness { get { return 0.5; } }

        public override byte Luminance { get { return 0; } }
        
        public override string DisplayName { get { return "Soul Sand"; } }

        public override SoundEffectClass SoundEffect
        {
            get
            {
                return SoundEffectClass.Sand;
            }
        }

        public override Tuple<int, int> GetTextureMap(byte metadata)
        {
            return new Tuple<int, int>(8, 6);
        }
    }
}