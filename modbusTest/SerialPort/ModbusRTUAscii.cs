using modbusTest.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest.SerialPort
{
    public class ModbusRTUAscii : ModbusRTUBase
    {
        public ModbusRTUAscii(System.IO.Ports.SerialPort sp) : base(sp) { }
        protected override T Read<T>(RequestBase obj, out ErrorResponse errorResponse)
        {
            throw new NotSupportedException("手头上的PLC 不支持ascii模式,暂时不写了");
        }

        private byte[] AsciiTobytes(byte[] data)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("X2"));
            Console.WriteLine(sb.ToString());
            return System.Text.Encoding.ASCII.GetBytes(sb.ToString());
        }
        protected override void Write(RequestBase obj)
        {
            var buffer = new ModbusRTUBuffer(serialPort.BaseStream);
            var bw = new System.IO.BinaryWriter(buffer);
            bw.Write(':');
            using (var ms = new System.IO.MemoryStream())
            {
                var bw2 = new System.IO.BinaryWriter(ms);
                bw2.Write((byte)obj.SlaveAddress);
                bw2.Write((byte)obj.Command);
                obj.Serialize(bw2);
                bw2.Write(CalculateLRC(buffer.ToArray()));
                bw.Write(AsciiTobytes(ms.ToArray()));
            }
            bw.Write(System.Text.Encoding.ASCII.GetBytes("\r\n"));
            buffer.Flush();
        }
        private byte CalculateLRC(byte[] data)
        {
            byte lrc = 0;
            foreach (byte b in data)
                lrc += b;
            lrc = (byte)((lrc ^ 0xFF) + 1);
            return lrc;
        }
    }
}
