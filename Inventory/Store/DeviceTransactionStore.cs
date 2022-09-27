using Inventory.DbContexts;
using Inventory.Dtos;
using Inventory.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Store
{
    public class DeviceTransactionStore
    {
        private readonly DeviceTransactionService _deviceTransactionService = null!;

        public DeviceTransactionStore()
        {
            _deviceTransactionService = new();
        }

        public async Task<IEnumerable<DeviceTransactionDto>> GetAllTransactions()
        {
            return await Task.Run(_deviceTransactionService.GetAllTransactions);
        }
    }
}
