using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary.Request
{
    public class WriteRegistersRequest : RequestBase
    {
        private System.IO.MemoryStream _stream;
        public override byte Command => 0x10;
        public ushort Address { get; set; }
        public HLBinaryWriter Data { get; }
        public WriteRegistersRequest()
        {
            _stream = new System.IO.MemoryStream();
            Data = new HLBinaryWriter(_stream);
        }
        public override void Serialize(HLBinaryWriter stream)
        {
            var Bytes = _stream.ToArray();
            if (Bytes.Length == 0 || Bytes.Length > (0x78 * 2))
                throw new ArgumentException();

            stream.Write((ushort)Address);
            stream.Write((ushort)(Bytes.Length / 2));
            stream.Write((byte)(Bytes.Length));
            stream.WriteBytes(Bytes);
        }
    }
    public class WriteRegistersResponse : ResponseBase
    {
        public ushort Address { get; private set; }
        public ushort Length { get; private set; }

        public override void Deserialize(RequestBase request, HLBinaryReader stream)
        {
            this.Address = stream.ReadUInt16();
            this.Length = stream.ReadUInt16();
        }
    }
}
