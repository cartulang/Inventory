using Inventory.DbContexts;
using Inventory.Dtos;
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
        public DeviceService()
        {
            _context = new();
        }


        public async Task<List<DeviceDto>> GetAllDevice()
        {
            try
            {
                var deviceList = await _context.DevicesDto.FromSqlRaw("EXEC GetDevices").ToListAsync();
                return deviceList;
            }

            catch(Exception)
            {
                MessageBox.Show("Error fetching devices.");
                return new List<DeviceDto>();
            }
        }

        public async Task<Device> GetDevice(int? deviceId)
        {
            var device = await _context.Devices.Where(d => d.Id == deviceId).FirstAsync();
            return device;
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

        public async Task<bool> WithdrawDevice(DeviceDto deviceDto, int quantity)
        {
            try
            {
                var availableDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 1)
                                                 .FirstOrDefaultAsync();

                var inUseDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 2)
                                                 .FirstOrDefaultAsync();

                if (deviceDto.Available < quantity)
                {
                    MessageBox.Show("Cannot withdraw device with that quantity.");
                    return false;
                }


                availableDevice.Quantity -= quantity;
                inUseDevice.Quantity += quantity;
                await _context.SaveChangesAsync();
                return true;
            }

            catch(Exception)
            {
                MessageBox.Show("Error withdraw device.");
                return false;
            }
        }

        public async Task<bool> ReturnDevice(DeviceDto deviceDto, int quantity)
        {
            try
            {
                var availableDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 1)
                                                 .FirstOrDefaultAsync();

                var inUseDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 2)
                                                 .FirstOrDefaultAsync();


                availableDevice.Quantity += quantity;
                inUseDevice.Quantity -= quantity;
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                MessageBox.Show("Error returning device.");
                return false;
            }
        }

        public async Task<List<DeviceDto>> DeleteDevice(int deviceId)
        {
 
            try
            {
                var device = _context.Devices.Where(_x => _x.Id == deviceId).FirstOrDefault();
                _context.Devices.Remove(device);
                _context.SaveChanges();
                return  await _context.DevicesDto.FromSqlRaw("EXEC GetDevices").ToListAsync(); ;
            }

            catch (Exception)
            {
                MessageBox.Show("Error deleting device.");
                return new List<DeviceDto>();
            }
        }
    }
}
