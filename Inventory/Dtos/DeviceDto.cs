using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = null!;
        public int Available { get; set; }
        public int InUse { get; set; }
        public int Defective { get; set; }
        public int Total { get; set; }
    }
}
