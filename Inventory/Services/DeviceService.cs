
using Inventory.DbContexts;
using Inventory.Dto;
using Inventory.Models;
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
        public DeviceService()
        {
            _context = new();
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
                await _context.AddAsync(device);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                MessageBox.Show("Error adding device.");
                return false;
            }
        }

        public async Task UpdateDevice(Device device)
        {
            try
            {
                var currentDevice = await _context.Devices.Where(x => x.Id == device.Id).FirstOrDefaultAsync();

                currentDevice.Status = device.Status;
                currentDevice.DeviceName = device.DeviceName;
                currentDevice.Quantity = device.Quantity;

                await _context.SaveChangesAsync();

                MessageBox.Show("Device updated!");
            }

            catch(Exception)
            {
                MessageBox.Show("Error updating device.");
            }
        }
    }
}
