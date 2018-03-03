using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModbusLibrary.Request
{
    public class ReadCoilsRequest : RequestBase
    {
        public override byte Command => 0x01;
        public ushort Address { get; set; }
        public ushort Length { get; set; }
        public override void Serialize(HLBinaryWriter stream)
        {
            if (Length > 0x7D0)
                throw new ArgumentException();

            stream.Write(this.Address);
            stream.Write(this.Length);
        }
    }
    public class ReadCoilsResponse : ResponseBase
    {
        public bool[] Coils { get; private set; }
        public override void Deserialize(RequestBase request, HLBinaryReader stream)
        {
            var req = request as ReadCoilsRequest;
            var bytes = stream.ReadBytes(stream.ReadByte());
            this.Coils = BitTransform.ToBoolean(bytes, req.Length);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Coils.Length; i++)
                sb.Append(Coils[i] ? "■" : "□");
            return sb.ToString();
        }
    }
}
