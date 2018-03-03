using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modbusTest.Request
{
    public class WriteRegisterRequest : RequestBase
    {
        public override byte Command => 0x06;
        public ushort Address { get; set; }
        public ushort Value { get; set; }
        public override void Serialize(HLBinaryWriter stream)
        {
            stream.Write((ushort)this.Address);
            stream.Write((ushort)this.Value);
        }
    }
    public class WriteRegisterResponse : ResponseBase
    {
        public ushort Address { get; private set; }
        public ushort Value { get; private set; }
        public override void Deserialize(RequestBase request, HLBinaryReader stream)
        {
            Address = stream.ReadUInt16();
            Value = stream.ReadUInt16();
        }
    }
}