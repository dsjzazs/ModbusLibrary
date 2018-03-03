using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModbusLibrary.Request
{
    public class WriteCoilRequest : RequestBase
    {
        public override byte Command => 0x05;
        public ushort Address { get; set; }
        public bool CoilStatus { get; set; }

        public override void Serialize(HLBinaryWriter stream)
        {
            stream.Write(this.Address);
            stream.WriteBytes(BoolConvert(CoilStatus));
        }
        protected byte[] BoolConvert(bool b)
        {
            return b ? new byte[] { 0xFF, 0x00 } : new byte[] { 0x00, 0x00 };
        }
    }
    public class WriteCoilResponse : ResponseBase
    {
        public ushort Address { get; private set; }
        public bool CoilStatus { get; private set; }
        public override void Deserialize(RequestBase request, HLBinaryReader stream)
        {
            Address = stream.ReadUInt16();
            CoilStatus = BoolConvert(stream.ReadBytes(2));
        }
        protected bool BoolConvert(byte[] b)
        {
            return b[0] == 0xff & b[1] == 0x00;
        }
    }
}
