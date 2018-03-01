using modbusTest.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusTest
{
    public abstract class Modbus
    {
        public ReadRegisterResponse ReadRegister(byte slave, ushort address, ushort length) { return this.ReadRegister<ReadRegisterResponse>(slave, address, length, out ErrorResponse error); }
        public ReadRegisterResponse ReadRegister(byte slave, ushort address, ushort length, out ErrorResponse error) { return this.ReadRegister<ReadRegisterResponse>(slave, address, length, out error); }
        public T ReadRegister<T>(byte slave, ushort address, ushort length) where T : ReadRegisterResponse, new() { return this.ReadRegister<T>(slave, address, length, out ErrorResponse error); }
        public T ReadRegister<T>(byte slave, ushort address, ushort length, out ErrorResponse error) where T : ReadRegisterResponse, new()
        {
            var request = new ReadRegisterRequest
            {
                SlaveAddress = slave,
                Address = address,
                Length = length
            };
            return this.Request<T>(request, out error);
        }
        public ReadInputStatusResponse ReadInputStatus(byte slave, ushort address, ushort length) { return this.ReadInputStatus(slave, address, length, out ErrorResponse error); }
        public ReadInputStatusResponse ReadInputStatus(byte slave, ushort address, ushort length, out ErrorResponse error)
        {
            var request = new ReadInputStatusRequest
            {
                SlaveAddress = slave,
                Address = address,
                Length = length
            };
            return this.Request<ReadInputStatusResponse>(request, out error);
        }
        public ReadCoilsResponse ReadCoils(byte slave, ushort address, ushort length) { return this.ReadCoils(slave, address, length, out ErrorResponse error); }
        public ReadCoilsResponse ReadCoils(byte slave, ushort address, ushort length, out ErrorResponse error)
        {
            var request = new ReadCoilsRequest
            {
                SlaveAddress = slave,
                Address = address,
                Length = length
            };
            return this.Request<ReadCoilsResponse>(request, out error);
        }
        public abstract T Request<T>(RequestBase obj, out ErrorResponse errorResponse) where T : ResponseBase, new();
    }
}
