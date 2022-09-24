using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class DevicesStatus
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int StatusId { get; set; }
        public int Quantity { get; set; }
    }
}
