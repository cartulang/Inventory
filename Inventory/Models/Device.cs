using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = null!;
    }
}
