﻿using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class DeviceTransaction
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int OperationId { get; set; }
        public string Date { get; set; } = null!;
    }
}
