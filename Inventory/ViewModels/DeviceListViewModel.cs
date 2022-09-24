using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Dtos;
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
        private readonly DeviceTransactionStore _deviceTransactionStore = null!;
        private IEnumerable<DeviceDto> _allDevice = null!;

        [ObservableProperty]
        private ObservableCollection<DeviceDto> _deviceList = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ToUpdateDeviceCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteDeviceCommand))]
        private DeviceDto _selectedDevice = null!;

        public DeviceListViewModel(NavigationStore navigationStore)
        {
            _deviceStore = new();
            _deviceList = new();
            _deviceTransactionStore = new();
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

        [RelayCommand(CanExecute = nameof(CanUpdateOrDeleteDevice))]
        private void ToUpdateDevice()
        {
            _navigationStore.CurrentViewModel = new WithdrawOrReturnDeviceViewModel(_navigationStore, _selectedDevice);
        }

        [RelayCommand(CanExecute = nameof(CanUpdateOrDeleteDevice))]
        private void DeleteDevice()
        {
            _allDevice = _deviceStore.DeleteDevice(_selectedDevice.Id).Result;
            _deviceList.Clear();
            ToDeviceList();
        }

        private bool CanUpdateOrDeleteDevice() => _selectedDevice is not null;
        private void DoneFetching(object? sender, RunWorkerCompletedEventArgs e)
        {
            ToDeviceList();
        }

        private void FetchDeviceList(object? sender, DoWorkEventArgs e)
        {
            _allDevice = _deviceStore.GetAllDevice().Result;
        }

        private void ToDeviceList()
        {
            foreach (DeviceDto device in _allDevice)
            {
                _deviceList.Add(device);
            }
        }
    }
}
