using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest.Request
{
    public class ReadCoilsRequest : RequestBase
    {
        public  override byte Command => 0x01;
        public ushort Address { get; set; }
        public ushort Length { get; set; }
        public override void Serialize(System.IO.BinaryWriter stream)
        {
            stream.Write(BitConverter.GetBytes(this.Address).Reverse().ToArray());
            stream.Write(BitConverter.GetBytes(this.Length).Reverse().ToArray());
        }
    }
    public class ReadCoilsResponse : ResponseBase
    {
        public bool[] Coils { get; private set; }
        public byte[] Bytes { get; private set; }
        public override void Deserialize(RequestBase request, BinaryReader stream)
        {
            var req = request as ReadCoilsRequest;
            Bytes = stream.ReadBytes(stream.ReadByte());
            this.Coils = BitTransform.ToBoolean(Bytes, req.Length);
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
