using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class DeviceTransaction
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int OperationId { get; set; }
        public int Quantity { get; set; }
        public string CurrentUser { get; set; } = null!;
        public string TransactionDate { get; set; } = null!;
    }
}
