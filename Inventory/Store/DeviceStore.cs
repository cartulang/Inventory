using Inventory.Dtos;
using Inventory.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Store
{
    public class DeviceStore
    {
        private readonly DeviceService _deviceService = null!;

        public DeviceStore()
        {
            _deviceService = new();
        }

        public async Task<IEnumerable<DeviceDto>> GetAllDevice()
        {
            return await Task.Run(_deviceService.GetAllDevice);
        }

        public async Task<bool> AddDevice(Device device, int quantity, string user)
        {
            return await _deviceService.AddDevice(device, quantity, user);
        }

        

        public async Task<bool> WithdrawDevice(DeviceDto deviceDto, int quantity, string user)
        {
            return await Task.Run(() => _deviceService.WithdrawDevice(deviceDto, quantity, user));
        }

        public async Task<bool> ReturnDevice(DeviceDto deviceDto, int quantity, string user)
        {
            return await Task.Run(() => _deviceService.ReturnDevice(deviceDto, quantity, user));
        }

        public async Task<IEnumerable<DeviceDto>> DeleteDevice(int deviceId)
        {
             return await _deviceService.DeleteDevice(deviceId);
        }

        public async Task<bool> RestockDevice(int quantity, int deviceId, string user)
        {
            return await _deviceService.RestockDevice(quantity, deviceId, user);
        }

        public async Task<bool> UnloadDevice(int deviceId, string user)
        {
            return await _deviceService.UnloadDevice(deviceId, user);
        }
    }
}
