using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest
{
    public static class BitTransform
    {
        public static bool[] ToBoolean(byte[] bytes, int bitLength)
        {
            if (bytes.Length * 8 < bitLength)
                throw new IndexOutOfRangeException();

            var temdata = new bool[bytes.Length * 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                    temdata[i * 8 + j] = Convert.ToBoolean(bytes[i] & ((byte)Math.Pow(2, j)));
            }
            var resultData = new bool[bitLength];
            Array.Copy(temdata, 0, resultData, 0, bitLength);
            return resultData;
        }
        public static Int32 ToInt32(byte[] bytes, int startIndex)
        {
            var data = new byte[]
            {
                bytes[startIndex + 1],
                bytes[startIndex + 0],
                bytes[startIndex + 3],
                bytes[startIndex + 2]
            };
            return BitConverter.ToInt32(data, 0);
        }
        public static UInt32 ToUInt32(byte[] bytes, int startIndex)
        {
            var data = new byte[]
            {
                bytes[startIndex + 1],
                bytes[startIndex + 0],
                bytes[startIndex + 3],
                bytes[startIndex + 2]
            };
            return BitConverter.ToUInt32(data, 0);
        }
        public static Int32[] ToInt32(byte[] bytes, int startIndex, int length)
        {
            var value = new Int32[length];
            for (int i = 0; i < length; i++)
                value[i] = ToInt32(bytes, startIndex + i * 4);
            return value;
        }
        public static UInt32[] ToUInt32(byte[] bytes, int startIndex, int length)
        {
            var value = new UInt32[length];
            for (int i = 0; i < length; i++)
                value[i] = ToUInt32(bytes, startIndex + i * 4);
            return value;
        }
        public static Int16 ToInt16(byte[] bytes, int startIndex)
        {
            var data = new byte[]
            {
                bytes[startIndex + 1],
                bytes[startIndex + 0],
            };
            return BitConverter.ToInt16(data, 0);
        }
        public static UInt16 ToUInt16(byte[] bytes, int startIndex)
        {
            var data = new byte[]
            {
                bytes[startIndex + 1],
                bytes[startIndex + 0],
            };
            return BitConverter.ToUInt16(data, 0);
        }
        public static Int16[] ToInt16(byte[] bytes, int startIndex, int length)
        {
            var value = new Int16[length];
            for (int i = 0; i < length; i++)
                value[i] = ToInt16(bytes, startIndex + i * 4);
            return value;
        }
        public static UInt16[] ToUInt16(byte[] bytes, int startIndex, int length)
        {
            var value = new UInt16[length];
            for (int i = 0; i < length; i++)
                value[i] = ToUInt16(bytes, startIndex + i * 4);
            return value;
        }

    }
}
