using Inventory.DbContexts;
using Inventory.Models;
using Inventory.Store;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.Services
{
    public class DeviceService
    {
        private readonly DeviceInventoryContext _context = null!;
        private readonly DeviceTransactionStore _transactionStore = null!;
        public DeviceService()
        {
            _context = new();
            _transactionStore = new();
        }


        public async Task<List<Device>> GetAllDevice()
        {
            try
            {
                var devices = await _context.Devices.ToListAsync();
                return devices.OrderByDescending(x => x.Id).ToList();
            }

            catch
            {
                MessageBox.Show("Error fetching devices.");
                return new List<Device>();
            }
        }

        public async Task<bool> AddDevice(Device device)
        {
            try
            {
                var deviceExist = _context.Devices.Where(x => x.DeviceName == device.DeviceName).FirstOrDefault();
                if (deviceExist is not null)
                {
                    MessageBox.Show("Device already exist.");
                    return false;
                }

                await _context.AddAsync(device);
                await _context.SaveChangesAsync();
                MessageBox.Show("Device added.");
                return true;
            }

            catch (Exception)
            {
                MessageBox.Show("Error adding device.");
                return false;
            }
        }

        public async Task<bool> UpdateDevice(Device device)
        {
            try
            {
                var currentDevice = await _context.Devices.Where(x => x.Id == device.Id).FirstOrDefaultAsync();

                currentDevice.Status = device.Status;
                currentDevice.DeviceName = device.DeviceName;
                currentDevice.Quantity = device.Quantity;

                await _context.SaveChangesAsync();

                MessageBox.Show("Device updated!");
                return true;
            }

            catch(Exception)
            {
                MessageBox.Show("Error updating device.");
                return false;
            }
        }

        public List<Device> DeleteDevice(int deviceId)
        {
            try
            {
                var device = _context.Devices.Where(_x => _x.Id == deviceId).FirstOrDefault();
                _context.Devices.Remove(device);
                _context.SaveChanges();
                return _context.Devices.ToList();
            }

            catch(Exception)
            {
                MessageBox.Show("Error deleting device.");
                return new List<Device>();
            }
        }
    }
}
