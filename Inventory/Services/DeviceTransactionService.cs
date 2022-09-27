using Inventory.DbContexts;
using Inventory.Dtos;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;

namespace Inventory.Services
{
    public class DeviceTransactionService
    {
        private readonly DeviceInventoryContext _context = null!;
        public DeviceTransactionService()
        {
            _context = new();
        }

        public async Task<List<DeviceTransactionDto>> GetAllTransactions()
        {
            try
            {
                var transactions = await _context.DeviceTransactionsDto.FromSqlRaw("EXEC GetTransactions").ToListAsync();
                return transactions.OrderByDescending(x => x.Id).ToList();
            }

            catch(Exception ex)
            {
                MessageBox.Show("Error fetching transactions");
                return new List<DeviceTransactionDto>();
            }
        }
    }
}
