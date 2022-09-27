using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Dtos
{
    public class DeviceTransactionDto
    {
        [Key]
        public int Id { get; set; }
        public string DeviceName { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public string CurrentUser { get; set; } = null!;
        public int Quantity { get; set; }
        public string TransactionDate { get; set; } = null!;
    }
}
