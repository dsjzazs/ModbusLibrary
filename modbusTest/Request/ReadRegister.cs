using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest.Request
{
    public class ReadRegisterRequest : RequestBase
    {
        public override byte Command => 0x03;
        public ushort Address { get; set; }
        public ushort Length { get; set; }
        public override void Serialize(BinaryWriter stream)
        {
            stream.Write(BitConverter.GetBytes(this.Address).Reverse().ToArray());
            stream.Write(BitConverter.GetBytes(this.Length).Reverse().ToArray());
        }
    }
    public class ReadRegisterResponse : ResponseBase
    {
        public byte[] Bytes { get; private set; }
        public override void Deserialize(RequestBase request, BinaryReader stream)
        {
            var req = request as ReadRegisterRequest;
            Bytes = stream.ReadBytes(stream.ReadByte());
        }
    }
}
