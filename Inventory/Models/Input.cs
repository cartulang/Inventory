using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class Input
    {
        private readonly InventoryService _inventoryService = null!;
        public string Id { get; set; }
        public string DeviceName { get; set; } = null!;
        public string LastUser { get; set; } = null!;
        public string DeviceStatus { get; set; } = null!;
        public string Date { get; set; } = null!;

        public Input()
        {
            _inventoryService = new();
        }

        public async Task<List<Input>> GetInventory()
        {
            return await _inventoryService.GetInventory();
        }

        public async Task<bool> AddInput(Input input)
        {
            return await _inventoryService.AddInput(input);
        }
    }
}
