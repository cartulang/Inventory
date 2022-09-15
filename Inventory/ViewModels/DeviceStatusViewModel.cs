using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public class DeviceStatusViewModel
    {
        public ICollection<DeviceStatus> DeviceStatusList = null!;

        public DeviceStatusViewModel()
        {
            DeviceStatusList = new List<DeviceStatus>();

            foreach(DeviceStatus deviceStatus in Enum.GetValues(typeof(DeviceStatus)))
            {
                DeviceStatusList.Add(deviceStatus);
            }

        }
    }
}
