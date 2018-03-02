using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest.SerialPort
{
    /// <summary>
    /// RTU缓冲流
    /// </summary>
    public class ModbusRTUBuffer : System.IO.Stream
    {
        public ModbusRTUBuffer(System.IO.Stream stream)
        {
            _stream = stream;
        }
        private Stream _stream;
        private MemoryStream Buffer { get; } = new MemoryStream();
        public byte[] ToArray()
        {
            return this.Buffer.ToArray();
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this._stream.CanRead == false)
                throw new NotSupportedException();

            var len = _stream.Read(buffer, offset, count);
            Buffer.Write(buffer, offset, len);
            Console.WriteLine($"RX : {buffer.ToHexString(offset, len)}");
            return len;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this._stream.CanWrite == false)
                throw new NotSupportedException();
            Buffer.Write(buffer, offset, count);
        }
        public override void Flush()
        {
            Buffer.Position = 0;
            var bytes = Buffer.ToArray();
            Console.WriteLine($"TX : {bytes.ToHexString()}");
            this._stream.Write(bytes, 0, bytes.Length);
        }
        public override bool CanRead => this._stream.CanRead;
        public override bool CanSeek => false;
        public override bool CanWrite => this._stream.CanWrite;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        protected override void Dispose(bool disposing)
        {
            this.Buffer.Dispose();
            base.Dispose(disposing);
        }
    }

}
