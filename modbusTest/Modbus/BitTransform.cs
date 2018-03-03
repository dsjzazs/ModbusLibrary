using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modbusTest
{
    public static class BitTransform
    {
        public static bool[] ToBoolean(byte bytes)
        {
            var data = new bool[8];
            for (int j = 0; j < 8; j++)
                data[j] = Convert.ToBoolean(bytes & ((byte)Math.Pow(2, j)));
            return data;
        }
        public static bool[] ToBoolean(byte[] bytes, int bitLength)
        {
            return ToBoolean(bytes, 0, bitLength);
        }
        public static bool[] ToBoolean(byte[] bytes, int startIndex, int bitLength)
        {
            if ((bytes.Length - startIndex) * 8 < bitLength)
                throw new IndexOutOfRangeException();

            var bytesLength = bytes.Length - startIndex;
            var temdata = new bool[bytesLength * 8];
            for (int i = startIndex; i < bytes.Length; i++)
                ToBoolean(bytes[i]).CopyTo(temdata, (i - startIndex) * 8);
            var result = new bool[bitLength];
            Array.Copy(temdata, 0, result, 0, bitLength);
            return result;
        }
        public static byte[] ToBytes(bool[] bools, out int count)
        {
            return ToBytes(bools, 0, bools.Length, out count);
        }
        public static byte[] ToBytes(bool[] bools, int offset, int length, out int count)
        {
            if (bools.Length - offset < length)
                throw new IndexOutOfRangeException(nameof(bools));

            count = 0;
            var bytes = new byte[(int)Math.Ceiling((length) / 8f)];
            int index = 0;
            for (int j = 0; j < length; j += 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (offset + j + i < bools.Length)
                    {
                        bytes[index] = (byte)(bytes[index] | (byte)(Convert.ToInt16(bools[offset + j + i]) << i));
                        count++;
                    }
                }
                index++;
            }
            return bytes;
        }
        public static string ToHexString(this byte[] bytes)
        {
            return ToHexString(bytes, 0, bytes.Length);
        }
        public static string ToHexString(this byte[] bytes, int offset, int length)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 3);
            for (int i = offset; i < length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}