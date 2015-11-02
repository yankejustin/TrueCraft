﻿using System;
using TrueCraft.API.Networking;

namespace TrueCraft.Core.Networking.Packets
{
    /// <summary>
    /// Sent by clients when clicking on an inventory window.
    /// </summary>
    public struct ClickWindowPacket : IPacket
    {
        public byte ID { get { return 0x66; } }

        public ClickWindowPacket(sbyte windowID, short slotIndex, bool rightClick, short transactionID, bool shift,
            short itemID, sbyte count, short metadata)
        {
            WindowID = windowID;
            SlotIndex = slotIndex;
            RightClick = rightClick;
            TransactionID = transactionID;
            Shift = shift;
            ItemID = itemID;
            Count = count;
            Metadata = metadata;
        }

        public sbyte WindowID;
        public short SlotIndex;
        public bool RightClick;
        public short TransactionID;
        public bool Shift;
        /// <summary>
        /// You should probably ignore this.
        /// </summary>
        public short ItemID;
        /// <summary>
        /// You should probably ignore this.
        /// </summary>
        public sbyte Count;
        /// <summary>
        /// You should probably ignore this.
        /// </summary>
        public short Metadata;

        public void ReadPacket(IMinecraftStream stream)
        {
            WindowID = stream.ReadInt8();
            SlotIndex = stream.ReadInt16();
            RightClick = stream.ReadBoolean();
            TransactionID = stream.ReadInt16();
            Shift = stream.ReadBoolean();
            ItemID = stream.ReadInt16();
            if (ItemID != -1)
            {
                Count = stream.ReadInt8();
                Metadata = stream.ReadInt16();
            }
        }

        public void WritePacket(IMinecraftStream stream)
        {
            stream.WriteInt8(WindowID);
            stream.WriteInt16(SlotIndex);
            stream.WriteBoolean(RightClick);
            stream.WriteInt16(TransactionID);
            stream.WriteBoolean(Shift);
            stream.WriteInt16(ItemID);
            if (ItemID != -1)
            {
                stream.WriteInt8(Count);
                stream.WriteInt16(Metadata);
            }
        }
    }
}