using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class DeviceTransaction
    {
        private DeviceTransactionService _transactionService = null!;
        public string Id { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public string Date { get; set; } = null!;

        public Task<List<DeviceTransaction>> GetAllTransactions()
        {
            _transactionService = new();
            return Task.Run(_transactionService.GetAllTransactions);
        }
    }

}
