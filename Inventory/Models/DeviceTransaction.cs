using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class DeviceTransaction
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Operation { get; set; } = null!;
        public string Date { get; set; } = null!;
    }
}
