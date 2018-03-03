using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary.Request
{
    /// <summary>
    /// Modbus请求基类
    /// </summary>
    public abstract class RequestBase
    {
        /// <summary>
        /// 功能码
        /// </summary>
        public abstract byte Command { get; }
        /// <summary>
        /// 站号
        /// </summary>
        public byte SlaveAddress { get; set; }
        /// <summary>
        /// 序列化 - 向流写入数据 (站号、功能码会在此之前自动写入,CRC校验会在此之后自动写入)
        /// </summary>
        /// <param name="stream"></param>
        public abstract void Serialize(HLBinaryWriter stream);
    }
    /// <summary>
    /// Modbus响应基类
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// 站号
        /// </summary>
        public byte SlaveAddress { get; set; }
        /// <summary>
        /// 反序列化 - 解析接收到的数据(站号、功能码会在此之前自动读取,CRC校验会在此之后自动读取)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stream"></param>
        public abstract void Deserialize(RequestBase request, HLBinaryReader stream);
    }
}
