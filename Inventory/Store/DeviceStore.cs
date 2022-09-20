using Inventory.Dto;
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

        public async Task<IEnumerable<Device>> GetAllDevice()
        {
            return await Task.Run(_deviceService.GetAllDevice);
        }

        public async Task<bool> AddDevice(Device device)
        {
            return await _deviceService.AddDevice(device);
        }

        public async Task UpdateDevice(Device device)
        {
            await _deviceService.UpdateDevice(device);
        }
    }
}
