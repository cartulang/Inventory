using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string Operation { get; set; } = null!;
        public string Time { get; set; } = null!;
    }
}
