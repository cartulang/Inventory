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
            UpdateCommand = new AsyncRelayCommand(UpdateDevice, CanUpdate);

            _deviceName = _device.DeviceName;
            _deviceStatus = _deviceStatusSelection.FindIndex(s => s == _device.Status);
            _quantity = _device.Quantity;
        }

        private async Task UpdateDevice()
        {
            _device.DeviceName = _deviceName;
            _device.Status = _deviceStatusSelection[_deviceStatus];
            _device.Quantity = _quantity;
            await _deviceStore.UpdateDevice(_device);
            Cancel();
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
    }
}
