using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModbusLibrary.Request
{

    public class ReadInputStatusRequest : RequestBase
    {
        public override byte Command => 0x02;
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
    public class ReadInputStatusResponse : ResponseBase
    {
        public bool[] InputStatus { get; private set; }
        public override void Deserialize(RequestBase request, HLBinaryReader stream)
        {
            var req = request as ReadInputStatusRequest;
            var bytes = new HLBinaryReader(stream.ReadBytes(stream.ReadByte()));
            this.InputStatus = BitTransform.ToBoolean(bytes.ToArray(), req.Length);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < InputStatus.Length; i++)
                sb.Append(InputStatus[i] ? "■" : "□");
            return sb.ToString();
        }
    }
}
