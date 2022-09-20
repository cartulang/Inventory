using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Rpc;
using Inventory.Dto;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.ViewModels
{
    partial class AddDeviceViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly DeviceStore _deviceStore = null!;
        private readonly DeviceDto _deviceDto = null!;

        [ObservableProperty]
        private List<string> _deviceStatusSelection = new()
        { "Available", "In-Use", "Malfunctioning"};

        [ObservableProperty]
        private string _deviceName = null!;

        [ObservableProperty]
        private int _deviceStatus;

        [ObservableProperty]
        private int _quantity = 1;

        public AddDeviceViewModel(NavigationStore navigationStore)
        {
            _deviceDto = new();
            _deviceStore = new();
            SubmitCommand = new AsyncRelayCommand(Submit);
            _navigationStore = navigationStore;
        }

        public IAsyncRelayCommand SubmitCommand { get; }
        private async Task Submit()
        {

            if (!ValidateInputs())
            {
                return;
            }




            /*_deviceDto.DeviceName = _deviceName;
            _deviceDto.Status = _deviceStatusSelection[_deviceStatus];
            _deviceDto.Quantity = _quantity;*/

            var device = new Device()
            {
                DeviceName = _deviceName,
                Status = _deviceStatusSelection[_deviceStatus],
                Quantity = _quantity
            };

            await _deviceStore.AddDevice(device);

    /*        if (!isSuccess)
            {
                return;
            }*/
            Cancel();
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationStore.CurrentViewModel = new DeviceListViewModel(_navigationStore);
        }

        private bool ValidateInputs()
        {
            if (String.IsNullOrEmpty(_deviceName))
            {
                MessageBox.Show("All input fields are required.");
                return false;
            }

            if (_quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.");
                return false;
            }

            return true;
        }
    }
}
