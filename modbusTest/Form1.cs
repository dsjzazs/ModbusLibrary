using modbusTest.Request;
using modbusTest.SerialPort;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modbusTest
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
            textBox5.Text = DateTime.Now.ToString("MM:ss.fff");
            var res = modbus.ReadInputStatus(slave, address, length);
            if (res == null)
                return;
            textBox5.Text += "\r\n";
            textBox5.Text += DateTime.Now.ToString("MM:ss.fff");
            textBox5.Text += "\r\n";
            textBox5.Text += res.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox7.Text);
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
            modbus = new ModbusRTUbytes(sp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var slave = Convert.ToByte(textBox10.Text);
            var address = Convert.ToUInt16(textBox9.Text);
            var length = Convert.ToUInt16(textBox8.Text);
            var res = modbus.ReadRegister<monitorState>(slave, address, length);
            if (res == null)
                return;
            textBox5.Text = res.ToString();
        }
    }
    public class monitorState : ReadRegisterResponse
    {
        public int ID => BitTransform.ToInt32(this.Bytes, 0);
        public int temper => BitTransform.ToInt32(this.Bytes, 4);

    }
}
