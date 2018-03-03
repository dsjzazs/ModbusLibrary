using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary
{
    public class ErrorResponse : System.EventArgs
    {
        public ErrorResponse(byte code)
        {
            this.Code = code;
            switch (code)
            {
                case 0x1:
                    this.Titile = "非法功能";
                    this.Content = @"对于服务器(或从站)来说，询问中接收到的功能码是不可允许的操作。这也许是因为功能码仅仅适用于新设备而在被选单元中是不可实现的。同时，还指出服务器(或从站)在错误状态中处理这种请求，例如：因为它是未配置的，并且要求返回寄存器值。";
                    break;
                case 0x2:
                    this.Titile = "非法数据地址";
                    this.Content = @"对于服务器(或从站)来说，询问中接收到的数据地址是不可允许的地址。特别是，参考号和传输长度的组合是无效的。对于带有 100 个寄存器的控制器来说，带有偏移量 96 和长度 4 的请求会成功，带有偏移量 96 和长度 5 的请求将产生异常码 02。";
                    break;
                case 0x3:
                    this.Titile = "非法数据值";
                    this.Content = @"对于服务器(或从站)来说，询问中包括的值是不可允许的值。这个值指示了组合请求剩余结构中的故障，例如：隐含长度是不正确的。并不意味着，因为MODBUS 协议不知道任何特殊寄存器的任何特殊值的重要意义，寄存器中被提交存储的数据项有一个应用程序期望之外的值。";
                    break;
                case 0x4:
                    this.Titile = "从站设备故障";
                    this.Content = @"当服务器(或从站)正在设法执行请求的操作时，产生不可重新获得的差错。";
                    break;
                case 0x5:
                    this.Titile = "确认";
                    this.Content = @"与编程命令一起使用。服务器(或从站)已经接受请求，并切正在处理这个请求，但是需要长的持续时间进行这些操作。返回这个响应防止在客户机(或主站)中发生超时错误。客户机(或主站)可以继续发送轮询程序完成报文来确定是否完成处理。";
                    break;
                case 0x6:
                    this.Titile = "从属设备忙";
                    this.Content = @"与编程命令一起使用。服务器(或从站)正在处理长持续时间的程序命令。当服务器(或从站)空闲时，用户(或主站)应该稍后重新传输报文。";
                    break;
                case 0x8:
                    this.Titile = "存储奇偶性差错";
                    this.Content = @"与功能码 20 和 21 以及参考类型 6 一起使用，指示扩展文件区不能通过一致性校验。服务器(或从站)设法读取记录文件，但是在存储器中发现一个奇偶校验错误。客户机(或主方)可以重新发送请求，但可以在服务器(或从站)设备上要求服务。";
                    break;
                case 0xA:
                    this.Titile = "不可用网关路径";
                    this.Content = @"与网关一起使用，指示网关不能为处理请求分配输入端口至输出端口的内部通信路径。通常意味着网关是错误配置的或过载的。";
                    break;
                case 0xB:
                    this.Titile = "网关目标设备响应失败";
                    this.Content = @"与网关一起使用，指示没有从目标设备中获得响应。通常意味着设备未在网络中。";
                    break;
                default:
                    this.Titile = $"未知的错误代码 : {code}";
                    break;
            }
        }
        public string Titile { get; }
        public string Content { get; }
        public byte Code { get; }
    }
    public class CRCException : Exception
    {
        public CRCException() : base("CRC 校验失败!") { }
    }
}
