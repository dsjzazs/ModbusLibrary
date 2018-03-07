using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModbusLibrary
{
    public class HLBinaryReader : System.IO.Stream
    {
        private byte[] _buffer = new byte[16];
        private System.IO.Stream OutStream;
        public Encoding Encoding { get; set; } = new UTF8Encoding(false, true);
        public override bool CanRead => OutStream.CanRead;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => OutStream.Length;
        public override long Position { get => OutStream.Position; set => OutStream.Position = value; }
        public override long Seek(long offset, SeekOrigin origin) => OutStream.Seek(offset, origin);
        public override void SetLength(long value) => OutStream.SetLength(value);
        public override void Flush() => OutStream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => OutStream.Read(buffer, offset, count);
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public HLBinaryReader(byte[] bytes)
        {
            OutStream = new System.IO.MemoryStream(bytes);
        }
        public HLBinaryReader(System.IO.Stream baseStream)
        {
            OutStream = baseStream;
        }
        public HLBinaryReader ReadBytesToBinaryReader(int count)
        {
            return new HLBinaryReader(this.ReadBytes(count));
        }
        public byte[] ToArray()
        {
            if (OutStream is MemoryStream)
                return (OutStream as MemoryStream).ToArray();
            throw new NotSupportedException();
        }
        public new byte ReadByte()
        {
            return (byte)OutStream.ReadByte();
        }
  
        /// <summary>
        /// 从基础流读取
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public byte[] ReadBytes(int numBytes, int setPosition)
        {
            this.Position = setPosition;
            return ReadBytes(numBytes);
        }
        /// <summary>
        /// 从基础流读取
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public byte[] ReadBytes(int numBytes)
        {
            var buffer = new byte[numBytes];
            int bytesRead = 0;
            int n = 0;
            if (OutStream == null)
                throw new ArgumentNullException(nameof(OutStream));
            if (this.ReadTimeout > 0)
            {
                var timeout = this.ReadTimeout + System.Environment.TickCount;
                do
                {
                    n = OutStream.Read(buffer, bytesRead, numBytes - bytesRead);
                    bytesRead += n;
                    if (System.Environment.TickCount > timeout)
                        throw new TimeoutException();
                } while (bytesRead < numBytes);
            }
            else
            {
                do
                {
                    n = OutStream.Read(buffer, bytesRead, numBytes - bytesRead);
                    bytesRead += n;
                } while (bytesRead < numBytes);
            }

            return buffer;
        }
        public double ReadDouble(int setPosition)
        {
            this.Position = setPosition;
            return ReadDouble();
        }
        public double ReadDouble()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToDouble(new byte[]
            {   bytes[1],
                bytes[0],
                bytes[3],
                bytes[2],
                bytes[5],
                bytes[4],
                bytes[7],
                bytes[6]}, 0);
        }
        public short ReadInt16(int setPosition)
        {
            this.Position = setPosition;
            return ReadInt16();
        }
        public short ReadInt16()
        {
            var bytes = ReadBytes(2);
            return BitConverter.ToInt16(new byte[]
            {   bytes[1],
                bytes[0]}, 0);
        }
        public int ReadInt32(int setPosition)
        {
            this.Position = setPosition;
            return ReadInt32();
        }
        public int ReadInt32()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToInt32(new byte[]
            {   bytes[1],
                bytes[0],
                bytes[3],
                bytes[2]}, 0);
        }
        public long ReadInt64(int setPosition)
        {
            this.Position = setPosition;
            return ReadInt64();
        }
        public long ReadInt64()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToInt64(new byte[]
            {   bytes[1],
                bytes[0],
                bytes[3],
                bytes[2],
                bytes[5],
                bytes[4],
                bytes[7],
                bytes[6]}, 0);
        }
        public sbyte ReadSByte(int setPosition)
        {
            this.Position = setPosition;
            return ReadSByte();
        }
        public sbyte ReadSByte()
        {
            return (sbyte)ReadBytes(1)[0];
        }
        public float ReadSingle(int setPosition)
        {
            this.Position = setPosition;
            return ReadSingle();
        }
        public float ReadSingle()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToSingle(new byte[]
            {   bytes[1],
                bytes[0],
                bytes[3],
                bytes[2]}, 0);
        }
        public ushort ReadUInt16(int setPosition)
        {
            this.Position = setPosition;
            return ReadUInt16();
        }
        public ushort ReadUInt16()
        {
            var bytes = ReadBytes(2);
            return BitConverter.ToUInt16(new byte[]
            {   bytes[1],
                bytes[0]}, 0);
        }
        public uint ReadUInt32(int setPosition)
        {
            this.Position = setPosition;
            return ReadUInt32();
        }
        public uint ReadUInt32()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToUInt32(new byte[]
            {   bytes[1],
                bytes[0],
                bytes[3],
                bytes[2]}, 0);
        }
        public ulong ReadUInt64(int setPosition)
        {
            this.Position = setPosition;
            return ReadUInt64();
        }
        public ulong ReadUInt64()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToUInt64(new byte[]
            {   bytes[1],
                bytes[0],
                bytes[3],
                bytes[2],
                bytes[5],
                bytes[4],
                bytes[7],
                bytes[6]}, 0);
        }
        public bool ReadBoolean()
        {
            throw new NotSupportedException();
        }
        public char ReadChar()
        {
            throw new NotSupportedException();
        }
        public char[] ReadChars(int count)
        {
            throw new NotSupportedException();
        }
        public decimal ReadDecimal()
        {
            throw new NotSupportedException();
        }
        public string ReadString()
        {
            throw new NotSupportedException();
        }
    }
}
