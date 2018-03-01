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
            var len = _stream.Read(buffer, offset, count);
            Buffer.Write(buffer, offset, len);
            return len;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            Buffer.Write(buffer, offset, count);
        }
        public override void Flush()
        {
            Buffer.Position = 0;
            Buffer.CopyTo(this._stream);
        }
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => throw new NotImplementedException();
        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override long Seek(long offset, SeekOrigin origin) => throw new NotImplementedException();
        public override void SetLength(long value) => throw new NotImplementedException();
        protected override void Dispose(bool disposing)
        {
            this.Buffer.Dispose();
            base.Dispose(disposing);
        }
    }

}
