using Inventory.DbContexts;
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

        public async Task<IEnumerable<DeviceTransaction>> GetAllTransactions()
        {
            return await Task.Run(_deviceTransactionService.GetAllTransactions);
        }

        public Task<bool> CreateTransaction(string operation, Device device)
        {
           return Task.Run(() => _deviceTransactionService.CreateTransaction(operation, device));
        }

    }
}
