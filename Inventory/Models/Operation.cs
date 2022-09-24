using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class Operation
    {
        public int Id { get; set; }
        public string OperationName { get; set; } = null!;
    }
}
