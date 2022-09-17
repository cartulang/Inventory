using Inventory.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Dto
{
    public class DeviceDto
    {
        public string DeviceName { get; set; } = null!;
        public string DeviceStatus { get; set; } = null!;
        public int Quantity { get; set; }


    }
}
