using ModbusLibrary.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary.SerialPort
{
    public class ModbusRTU : ModbusRTUBase
    {
        public ModbusRTU(System.IO.Ports.SerialPort sp) : base(sp) { }
        protected override void Write(RequestBase obj)
        {
            var buffer = new ModbusRTUBuffer(serialPort.BaseStream);
            var bw = new HLBinaryWriter(buffer);
            bw.Write((byte)obj.SlaveAddress);
            bw.Write((byte)obj.Command);
            obj.Serialize(bw);
            var bytes = buffer.ToArray();
            var crcBytes = CalculateCRC(bytes, bytes.Length);
            bw.WriteBytes(crcBytes);
            buffer.Flush();
        }
        protected override T Read<T>(RequestBase obj, out ErrorResponse errorResponse)
        {
            T res = new T();
            errorResponse = null;
            var buffer = new ModbusRTUBuffer(serialPort.BaseStream);
            var br = new HLBinaryReader(buffer);
            res.SlaveAddress = br.ReadByte();
            var command = br.ReadByte();
            var crc = new byte[2];

            if (command == obj.Command)
            {
                res.Deserialize(obj, br);
                br.Read(crc, 0, crc.Length);
                var bytes = buffer.ToArray();
                var crc2 = CalculateCRC(bytes, bytes.Length - 2);
                if (BitConverter.ToUInt16(crc, 0) != BitConverter.ToUInt16(crc2, 0))
                    throw new CRCException();
                return res;
            }
            else if (command == 0x80 + obj.Command)
            {
                errorResponse = new ErrorResponse(br.ReadByte());
                Console.WriteLine($"错误 : {errorResponse.Titile }");
                Console.WriteLine($"详情 : {errorResponse.Content}");
                br.Read(crc, 0, crc.Length);
                var bytes = buffer.ToArray();
                var crc2 = CalculateCRC(bytes, bytes.Length - 2);
                if (BitConverter.ToUInt16(crc, 0) != BitConverter.ToUInt16(crc2, 0))
                    throw new CRCException();
                return null;
            }
            else
                throw new Exception();
        }
        private byte[] CalculateCRC(byte[] data, int length)
        {
            byte CRC16Lo, CRC16Hi;   //CRC寄存器 
            byte CL, CH;       //多项式码&HA001 
            byte SaveHi, SaveLo;
            byte[] tmpData;
            int Flag;
            CRC16Lo = 0xFF;
            CRC16Hi = 0xFF;
            CL = 0x01;
            CH = 0xA0;
            tmpData = data;
            for (int i = 0; i < length; i++)
            {
                CRC16Lo = (byte)(CRC16Lo ^ tmpData[i]); //每一个数据与CRC寄存器进行异或 
                for (Flag = 0; Flag <= 7; Flag++)
                {
                    SaveHi = CRC16Hi;
                    SaveLo = CRC16Lo;
                    CRC16Hi = (byte)(CRC16Hi >> 1);      //高位右移一位 
                    CRC16Lo = (byte)(CRC16Lo >> 1);      //低位右移一位 
                    if ((SaveHi & 0x01) == 0x01) //如果高位字节最后一位为1 
                    {
                        CRC16Lo = (byte)(CRC16Lo | 0x80);   //则低位字节右移后前面补1 
                    }             //否则自动补0 
                    if ((SaveLo & 0x01) == 0x01) //如果LSB为1，则与多项式码进行异或 
                    {
                        CRC16Hi = (byte)(CRC16Hi ^ CH);
                        CRC16Lo = (byte)(CRC16Lo ^ CL);
                    }
                }
            }
            byte[] ReturnData = new byte[2];
            ReturnData[0] = CRC16Lo;       //CRC低位 
            ReturnData[1] = CRC16Hi;       //CRC高位 
            return ReturnData;
        }
    }
}
