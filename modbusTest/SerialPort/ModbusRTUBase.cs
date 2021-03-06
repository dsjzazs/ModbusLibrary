﻿using ModbusLibrary.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary.SerialPort
{
    public abstract class ModbusRTUBase : Modbus
    {
        public short MaxError { get; set; } = 5;
        private object lockObject = new object();
        protected System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort();
        public ModbusRTUBase(System.IO.Ports.SerialPort sp)
        {
  
            this.serialPort = sp;
            if (this.serialPort.IsOpen == false)
                this.serialPort.Open();
        }
        protected abstract void Write(RequestBase obj);
        protected abstract T Read<T>(RequestBase obj, out ErrorResponse errorResponse) where T : ResponseBase, new();
        public override T Request<T>(RequestBase obj, out ErrorResponse errorResponse)
        {
            short error = 0;
            while (true)
            {
                try
                {
                    lock (lockObject)
                    {
                        serialPort.DiscardOutBuffer();
                        serialPort.DiscardInBuffer();
                        Write(obj);
                        if (obj.SlaveAddress > 0)
                            return this.Read<T>(obj, out errorResponse);
                        else
                        {
                            errorResponse = null;
                            return null;
                        }
                    }
                }
                catch (TimeoutException ex)
                {
                    error++;
                    Console.WriteLine(ex.Message);
                    if (error > MaxError)
                        throw ex;
                }
                catch (CRCException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
