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
        BackgroundWorker bgWork = new();
        private readonly NavigationStore _navigationStore = null!;
        private readonly Device _device = null!;
        private IEnumerable<Device> _allDevice = null!;

        [ObservableProperty]
        private ObservableCollection<Device> _deviceList = null!;

        [ObservableProperty]
        private string _id = null!;

        [ObservableProperty]
        private string _deviceName = null!;

        [ObservableProperty]
        private string _deviceStatus = null!;

        [ObservableProperty]
        private int _quantity;

        public DeviceListViewModel(NavigationStore navigationStore)
        {
            _device = new();
            _navigationStore = navigationStore;
            _deviceList = new();
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


        private void DoneFetching(object? sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Device device in _allDevice)
            {
                _deviceList.Add(device);
            }
        }

        private void FetchDeviceList(object? sender, DoWorkEventArgs e)
        {
            _allDevice = _device.GetAllDevice();
        }
    }
}
