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

        public async Task<bool> AddDevice(Device device, int quantity, string user)
        {
            try
            {
                var deviceExist = await _context.Devices.Where(d => d.DeviceName == device.DeviceName).FirstOrDefaultAsync();

                if(deviceExist != null)
                {
                    MessageBox.Show("Device already exist.");
                    return false;
                }

                await _context.Devices.AddAsync(device);
                await _context.SaveChangesAsync();

                var savedDevice = await _context.Devices.Where(d => d.DeviceName == device.DeviceName).FirstOrDefaultAsync();
                var deviceStatus = new DevicesStatus() { DeviceId = savedDevice.Id, StatusId = 1, Quantity = quantity };
                var transaction = CreateTransaction(savedDevice.Id, 6, quantity, user);
                await _context.DeviceTransactions.AddAsync(transaction);
                await _context.DevicesStatuses.AddAsync(deviceStatus);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                MessageBox.Show("Error adding device.");
                return false;
            }
        }

        public async Task<bool> WithdrawDevice(DeviceDto deviceDto, int quantity, string user)
        {
            try
            {
                var transaction = CreateTransaction(deviceDto.Id, 1, quantity, user);
                var availableDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 1)
                                                 .FirstOrDefaultAsync();

                var inUseDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 2)
                                                 .FirstOrDefaultAsync();

                if(inUseDevice == null)
                {
                   await _context.DevicesStatuses.AddAsync(new DevicesStatus()
                    {
                        StatusId = 2,
                        DeviceId = deviceDto.Id,
                        Quantity = 0,
                    });

                    await _context.SaveChangesAsync();
                    inUseDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 2)
                                                 .FirstOrDefaultAsync();
                }

                if (deviceDto.Available < quantity)
                {
                    MessageBox.Show("Cannot withdraw device with that quantity.");
                    return false;
                }

                availableDevice.Quantity -= quantity;
                inUseDevice.Quantity += quantity;
                await _context.DeviceTransactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return true;
            }

            catch(Exception)
            {
                MessageBox.Show("Error withdrawing device.");
                return false;
            }
        }

        public async Task<bool> ReturnDevice(DeviceDto deviceDto, int quantity, string user)
        {
            try
            {
                var transaction = CreateTransaction(deviceDto.Id, 2, quantity, user);
                var availableDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 1)
                                                 .FirstOrDefaultAsync();

                var inUseDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 2)
                                                 .FirstOrDefaultAsync();


                if (inUseDevice == null)
                {
                    await _context.DevicesStatuses.AddAsync(new DevicesStatus()
                    {
                        StatusId = 2,
                        DeviceId = deviceDto.Id,
                        Quantity = 0,
                    });

                    await _context.SaveChangesAsync();
                    inUseDevice = await _context.DevicesStatuses
                                                 .Where(d => d.DeviceId == deviceDto.Id && d.StatusId == 2)
                                                 .FirstOrDefaultAsync();
                }


                if (inUseDevice.Quantity == 0 || inUseDevice.Quantity < quantity)
                {
                    MessageBox.Show("Cannot return device with that quantity.");
                    return false;
                }

    

                availableDevice.Quantity += quantity;
                inUseDevice.Quantity -= quantity;
                await _context.DeviceTransactions.AddAsync(transaction);
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
                var transaction = CreateTransaction(deviceId, 6);
                await _context.DeviceTransactions.AddAsync(transaction);
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

        public async Task<bool> RestockDevice(int quantity, int deviceId, string user)
        {
            try
            {
                var device = await _context.DevicesStatuses.Where(d => d.DeviceId == deviceId && d.StatusId == 1).FirstOrDefaultAsync();
                var transaction = CreateTransaction(deviceId, 3, quantity, user);
                if (device == null)
                {
                    await _context.DevicesStatuses.AddAsync(new DevicesStatus()
                    {
                        Quantity = quantity,
                        StatusId = 1,
                        DeviceId = deviceId
                    });

                    await _context.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                    return true;
                }

                device.Quantity += quantity;
                await _context.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return true;
            }

            catch(Exception)
            {
                MessageBox.Show("Error Restocking device.");
                return false;
            }
        }

        public async Task<bool> UnloadDevice(int deviceId, string user)
        {
            try
            {
                var devices = await _context.DevicesStatuses.Where(d => d.DeviceId == deviceId).ToListAsync();
                var totalDevice = 0;
                foreach (var device in devices)
                {
                    totalDevice += device.Quantity;
                    device.Quantity = 0;
                }
                var transaction = CreateTransaction(deviceId, 4, totalDevice, user);
                await _context.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception)
            {
                MessageBox.Show("Error Unloading device.");
                return false;
            }
        }

        private DeviceTransaction CreateTransaction(int deviceId, int operationId, int quantity = 0, string user = "admin")
        {
           return new DeviceTransaction()
            {
                DeviceId = deviceId,
                OperationId = operationId,
                Quantity = quantity,
                CurrentUser = user,
                TransactionDate = DateTime.Now.ToString("dd/MM/yyyy h:mm tt")
            };
        }
    }
}
