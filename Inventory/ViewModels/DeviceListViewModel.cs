using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.ViewModels
{
    public partial class DeviceListViewModel : ViewModelBase
    {
        private readonly BackgroundWorker bgWork = new();
        private readonly NavigationStore _navigationStore = null!;
        private readonly DeviceStore _deviceStore = null!;
        private IEnumerable<Device> _allDevice = null!;

        [ObservableProperty]
        private ObservableCollection<Device> _deviceList = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ToUpdateDeviceCommand))]
        private Device _selectedDevice = null!;

        public DeviceListViewModel(NavigationStore navigationStore)
        {
            _deviceStore = new();
            _deviceList = new();
            _navigationStore = navigationStore;
            bgWork.DoWork += FetchDeviceList;
            bgWork.RunWorkerCompleted += DoneFetching;
            bgWork.RunWorkerAsync();
        }

        [RelayCommand]
        private void ToAddDevice()
        {
            _navigationStore.CurrentViewModel = new AddDeviceViewModel(_navigationStore);
        }

        [RelayCommand]
        private void ToHome()
        {
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
        }

        [RelayCommand(CanExecute = nameof(CanUpdateDevice))]
        private void ToUpdateDevice()
        {
            _navigationStore.CurrentViewModel = new UpdateDeviceViewModel(_navigationStore, _selectedDevice);
        }

        private bool CanUpdateDevice() => _selectedDevice is not null;

        private void DoneFetching(object? sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Device device in _allDevice)
            {
                _deviceList.Add(device);
            }
        }

        private void FetchDeviceList(object? sender, DoWorkEventArgs e)
        {
            _allDevice = _deviceStore.GetAllDevice().Result;
        }
    }
}
