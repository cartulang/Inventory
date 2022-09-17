using Inventory.Dto;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class Device
    {
        private DeviceService _deviceService = null!;
        public string Id { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public string DeviceStatus { get; set; } = null!;
        public int Quantity { get; set; }

        public IEnumerable<Device> GetAllDevice()
        {
            _deviceService = new();
            return Task.Run(_deviceService.GetAllDevice).Result;
        }

        public async Task<bool> AddDevice(DeviceDto device)
        {
            _deviceService = new();
            return await _deviceService.AddDevice(device);
        }
    }
}
