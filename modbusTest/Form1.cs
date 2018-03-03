using ModbusLibrary.Request;
using ModbusLibrary.SerialPort;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModbusLibrary
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
        private Modbus modbus;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox3.Text);
            var length = Convert.ToUInt16(textBox4.Text);
            var res = modbus.ReadInputStatus(slave, address, length);
            if (res == null)
                return;
            textBox5.Text = res.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox6.Text);
            var length = Convert.ToUInt16(textBox2.Text);
            var res = modbus.ReadCoils(slave, address, length);
            if (res == null)
                return;
            textBox5.Text = res.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sp.PortName = "COM4";
            sp.BaudRate = 115200;
            sp.Parity = System.IO.Ports.Parity.Odd;
            sp.DataBits = 8;
            sp.StopBits = System.IO.Ports.StopBits.One;
            sp.ReadTimeout = 100;
            modbus = new ModbusRTU(sp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox9.Text);
            var length = Convert.ToUInt16(textBox8.Text);
            var res = modbus.ReadRegister<monitorState>(slave, address, length);
            if (res == null)
                return;
            textBox5.Text = $"(测试自定义类型) ID : {res.ID} , 温度 : { res.temper}\r\n{res.Data.ToArray().ToHexString()}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox12.Text);
            var status = checkBox1.Checked;
            var res = modbus.WriteCoil(slave, address, status);
            if (res == null)
                return;
            textBox5.Text = res.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox7.Text);
            var b = checkBox2.Checked;
            var res = modbus.WriteCoils(slave, address, new bool[] { b, b, b, b, b });
            if (res == null)
                return;
            textBox5.Text = res.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //简单的无类型写入

            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox10.Text);
            ushort length = 10;
            var random = new Random();

            var req = new WriteRegistersRequest() { SlaveAddress = slave, Address = address };
            for (int i = 0; i < length; i++)
                req.Data.Write((ushort)random.Next(ushort.MinValue, ushort.MaxValue));

            var res = modbus.Request<WriteRegistersResponse>(req);
            if (res?.Length != length)
            {
                textBox5.Text = "写入失败!";
                return;
            }
            var res2 = modbus.ReadRegister(slave, address, length);
            textBox5.Text = res2.Data.ToArray().ToHexString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox1.Text);
            var address = Convert.ToUInt16(textBox10.Text);

            var config = new configParameter()
            {
                PN = 12345,
                Speed = 50000,
                Temperature = 99.71f
            };
            var req = new WriteRegistersRequest()
            {
                SlaveAddress = slave,
                Address = address,
            };
            req.Data.Write(config.Temperature);
            req.Data.Write(config.PN);
            req.Data.Write(config.Speed);

            ushort length = 10;
            var res = modbus.WriteRegister(req, out ErrorResponse error);

            if (res == null)
            {
                if (error != null)
                    textBox5.Text = $"写入失败 : {error.Titile}\r\n{error.Content}";
                return;
            }
            var res2 = modbus.ReadRegister(slave, address, length);
            textBox5.Text = $"温度:{res2.Data.ReadSingle(0)}  产品编号:{res2.Data.ReadUInt16(4)}  速度:{res2.Data.ReadUInt32(6)}\r\n{res2.Data.ToArray().ToHexString()}";
        }
    }
    public class configParameter
    {
        public uint Speed { get; set; }
        public ushort PN { get; set; }
        public float Temperature { get; set; }
    }

    public class monitorState : ReadRegisterResponse
    {
        public int ID => this.Data.ReadInt32(0);
        public int temper => this.Data.ReadInt32(4);

    }
}
