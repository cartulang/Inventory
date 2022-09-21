using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public partial class UpdateDeviceViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly Device _device = null!;
        private readonly DeviceStore _deviceStore = null!;
        private readonly DeviceTransactionStore _deviceTransactionStore = null!;

        [ObservableProperty]
        private List<string> _deviceStatusSelection = new()
        { "Available", "In-Use", "Malfunctioning"};

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private string _deviceName = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private int _deviceStatus;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
        private int _quantity = 1;

        public IAsyncRelayCommand UpdateCommand { get; }

        public UpdateDeviceViewModel(NavigationStore navigationStore, Device device)
        {
            _navigationStore = navigationStore;
            _device = device;
            _deviceStore = new();
            _deviceTransactionStore = new();
            UpdateCommand = new AsyncRelayCommand(UpdateDevice, CanUpdate);

            _deviceName = _device.DeviceName;
            _deviceStatus = _deviceStatusSelection.FindIndex(s => s == _device.Status);
            _quantity = _device.Quantity;
        }

        private async Task UpdateDevice()
        {
            var device = new Device()
            {
                Id = _device.Id,
                DeviceName = _deviceName,
                Status = _deviceStatusSelection[_deviceStatus],
                Quantity = _quantity
            };
            
            var isSuccess = await SetUpdateOperation(device) && await _deviceStore.UpdateDevice(device) ;

            if(isSuccess)
            {
                Cancel();
            }
        }

        private bool CanUpdate()
        {
            bool isSameName = _deviceName == _device.DeviceName;
            bool isSameStatus = _deviceStatus == _deviceStatusSelection.FindIndex(s => s == _device.Status);
            bool isSameQuantity = _quantity == _device.Quantity;

            return !isSameName || !isSameQuantity || !isSameStatus;
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationStore.CurrentViewModel = new DeviceListViewModel(_navigationStore);
        }

        private async Task<bool> SetUpdateOperation(Device device)
        {
            bool isSameName = _deviceName == _device.DeviceName;
            bool isSameStatus = device.Status == _deviceStatusSelection[_deviceStatusSelection.FindIndex(s => s == _device.Status)];
            bool isSameQuantity = _quantity == _device.Quantity;
            List<bool> areSuccess = new();

            if (!isSameName)
            {
                areSuccess.Add(await _deviceTransactionStore.CreateTransaction($"Name To {device.DeviceName}", device));
            }

            if(!isSameStatus)
            {
                areSuccess.Add(await _deviceTransactionStore.CreateTransaction($"Status To {device.Status}", device));
            }

            if(!isSameQuantity)
            {
                areSuccess.Add(_quantity > _device.Quantity ? 
                    await _deviceTransactionStore.CreateTransaction("Restock", device) : 
                    await _deviceTransactionStore.CreateTransaction("Withdraw", device));
            }

            return areSuccess.TrueForAll(s => s);
        }
    }
}
