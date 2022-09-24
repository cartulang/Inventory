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

        public Device GetDevice(int deviceId)
        {
            return _deviceService.GetDevice(deviceId).Result;
        }

        public async Task<bool> AddDevice(Device device)
        {
            return await _deviceService.AddDevice(device);
        }

        public async Task<bool> WithdrawDevice(DeviceDto deviceDto, int quantity)
        {
            return await Task.Run(() => _deviceService.WithdrawDevice(deviceDto, quantity));
        }

        public async Task<bool> ReturnDevice(DeviceDto deviceDto, int quantity)
        {
            return await Task.Run(() => _deviceService.ReturnDevice(deviceDto, quantity));
        }

        public async Task<IEnumerable<DeviceDto>> DeleteDevice(int deviceId)
        {
             return await _deviceService.DeleteDevice(deviceId);
        }
    }
}
