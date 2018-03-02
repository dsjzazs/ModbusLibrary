using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest.Request
{
    public class WriteRegistersRequest : RequestBase
    {
        public override byte Command => 0x10;
        public ushort Address { get; set; }
        public ushort[] Data { get; set; }
        public override void Serialize(HLBinaryWriter stream)
        {
            if (Data == null)
                throw new ArgumentNullException();

            if (Data.Length == 0 || Data.Length > 0x78)
                throw new ArgumentException();

            stream.Write((ushort)Address);
            stream.Write((ushort)Data.Length);
            stream.Write((byte)(Data.Length * 2));
            foreach (var item in Data)
                stream.Write((ushort)item);
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
