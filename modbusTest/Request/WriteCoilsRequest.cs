using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary.Request
{
    public class WriteCoilsRequest : RequestBase
    {
        public override byte Command => 0x0f;
        public ushort Address { get; set; }
        public bool[] Coils { get; set; }
        public override void Serialize(HLBinaryWriter stream)
        {
            if (Coils == null)
                throw new ArgumentNullException();

            var bytes = BitTransform.ToBytes(Coils, out int count);
            if (Coils.Length == 0 || count > 0x07B0)
                throw new ArgumentException();

            stream.Write((ushort)Address);
            stream.Write((ushort)count);
            stream.Write((byte)bytes.Length);
            stream.WriteBytes(bytes);
        }
    }
    public class WriteCoilsResponse : ResponseBase
    {
        public ushort Address { get; private set; }
        public ushort CoilCount { get; private set; }
        public override void Deserialize(RequestBase request, HLBinaryReader stream)
        {
            this.Address = stream.ReadUInt16();
            this.CoilCount = stream.ReadUInt16();
        }
    }
}
