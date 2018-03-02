using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest
{
    public class HLBinaryWriter : System.IO.Stream
    {
        private System.IO.Stream OutStream;
        public Encoding Encoding { get; set; } = new UTF8Encoding(false, true);
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => OutStream.CanWrite;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Flush() => OutStream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => OutStream.Write(buffer, offset, count);
        private byte[] _buffer = new byte[16];
        public HLBinaryWriter(System.IO.Stream baseStream)
        {
            OutStream = baseStream;
        }
        public void Write(short value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            OutStream.Write(_buffer, 0, 2);
        }
        public void Write(ushort value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            OutStream.Write(_buffer, 0, 2);
        }
        public unsafe void Write(float value)
        {
            uint TmpValue = *(uint*)&value;
            _buffer[0] = (byte)TmpValue;
            _buffer[1] = (byte)(TmpValue >> 8);
            _buffer[2] = (byte)(TmpValue >> 16);
            _buffer[3] = (byte)(TmpValue >> 24);
            OutStream.Write(_buffer, 0, 4);
        }
        public void Write(ulong value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            _buffer[3] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 24);
            _buffer[5] = (byte)(value >> 32);
            _buffer[4] = (byte)(value >> 40);
            _buffer[7] = (byte)(value >> 48);
            _buffer[6] = (byte)(value >> 56);
            OutStream.Write(_buffer, 0, 8);
        }
        public void Write(long value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            _buffer[3] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 24);
            _buffer[5] = (byte)(value >> 32);
            _buffer[4] = (byte)(value >> 40);
            _buffer[7] = (byte)(value >> 48);
            _buffer[6] = (byte)(value >> 56);
            OutStream.Write(_buffer, 0, 8);
        }
        public void Write(uint value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            _buffer[3] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 24);
            OutStream.Write(_buffer, 0, 4);
        }
        public void Write(int value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            _buffer[3] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 24);
            OutStream.Write(_buffer, 0, 4);
        }
        public unsafe void Write(double value)
        {
            ulong TmpValue = *(ulong*)&value;
            _buffer[1] = (byte)TmpValue;
            _buffer[0] = (byte)(TmpValue >> 8);
            _buffer[3] = (byte)(TmpValue >> 16);
            _buffer[2] = (byte)(TmpValue >> 24);
            _buffer[5] = (byte)(TmpValue >> 32);
            _buffer[4] = (byte)(TmpValue >> 40);
            _buffer[7] = (byte)(TmpValue >> 48);
            _buffer[8] = (byte)(TmpValue >> 56);
            OutStream.Write(_buffer, 0, 8);
        }
        /// <summary>
        /// 向基础流写入,Bytes将始终保持不变
        /// </summary>
        /// <param name="value"></param>
        public void WriteBytes(byte[] value)
        {
            OutStream.Write(value, 0, value.Length);
        }
        public void Write(sbyte value)
        {
            OutStream.WriteByte((byte)value);
        }
        public void Write(byte value)
        {
            OutStream.WriteByte(value);
        }
        public void Write(bool value)
        {
            throw new NotImplementedException();
        }
        public void Write(string value)
        {
            throw new NotImplementedException();
        }
        public void Write(char[] chars)
        {
            throw new NotImplementedException();
        }
        public unsafe void Write(char ch)
        {
            throw new NotImplementedException();
        }
        public void Write(decimal value)
        {
            throw new NotImplementedException();
        }
    }
}
