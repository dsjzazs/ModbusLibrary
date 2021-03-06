﻿using ModbusLibrary.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusLibrary
{
    public abstract class Modbus
    {
        public event EventHandler<ErrorResponse> ErrorEvent;
        public WriteRegistersResponse WriteRegister(WriteRegistersRequest request)
        {
            return this.WriteRegister(request, out ErrorResponse error);
        }
        public WriteRegistersResponse WriteRegister(WriteRegistersRequest request, out ErrorResponse error)
        {
            return this._request<WriteRegistersResponse>(request, out error);
        }
        public WriteCoilResponse WriteCoil(byte slave, ushort address, bool Status) { return WriteCoil(slave, address, Status, out ErrorResponse error); }
        public WriteCoilResponse WriteCoil(byte slave, ushort address, bool Status, out ErrorResponse error)
        {
            var request = new WriteCoilRequest
            {
                SlaveAddress = slave,
                Address = address,
                CoilStatus = Status
            };
            return this._request<WriteCoilResponse>(request, out error);
        }
        public WriteCoilsResponse WriteCoils(byte slave, ushort address, bool[] Coils) { return WriteCoils(slave, address, Coils, out ErrorResponse error); }
        public WriteCoilsResponse WriteCoils(byte slave, ushort address, bool[] Coils, out ErrorResponse error)
        {
            var request = new WriteCoilsRequest
            {
                SlaveAddress = slave,
                Address = address,
                Coils = Coils
            };
            return this._request<WriteCoilsResponse>(request, out error);
        }
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
            return this._request<T>(request, out error);
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
            return this._request<ReadInputStatusResponse>(request, out error);
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
            return this._request<ReadCoilsResponse>(request, out error);
        }
        private T _request<T>(RequestBase obj, out ErrorResponse errorResponse) where T : ResponseBase, new()
        {
            var res = this.Request<T>(obj, out errorResponse);
            if (res == null && errorResponse != null)
                ErrorEvent?.Invoke(obj, errorResponse);
            return res;
        }
        public T Request<T>(RequestBase obj) where T : ResponseBase, new()
        {
            return this._request<T>(obj, out ErrorResponse error);
        }
        public abstract T Request<T>(RequestBase obj, out ErrorResponse errorResponse) where T : ResponseBase, new();
    }
}
