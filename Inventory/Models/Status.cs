using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public partial class Status
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = null!;
    }
}
