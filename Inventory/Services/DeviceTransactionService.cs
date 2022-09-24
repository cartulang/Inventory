using Inventory.DbContexts;
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

        public async Task<List<DeviceTransaction>> GetAllTransactions()
        {
            try
            {
                /*         var transactions = await _context.DeviceTransactions.ToListAsync();
                          return transactions.OrderByDescending(x => x.Id).ToList();*/
                return new List<DeviceTransaction>();
            }

            catch(Exception)
            {
                MessageBox.Show("Error fetching transactions");
                return new List<DeviceTransaction>();
            }
        }

        public async Task<bool> CreateTransaction(string operation, Device device)
        {
            try
            {
                DeviceTransaction transaction = new()
                {
            /*        DeviceName = device.DeviceName,
                    Date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                    Operation = operation,
                    LastUser = device.LastUser,
                    Quantity = device.Quantity,
                    DeviceId = device.Id*/
                   
                };
/*
                await _context.DeviceTransactions.AddAsync(transaction);
                await _context.SaveChangesAsync();*/
                return true;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
