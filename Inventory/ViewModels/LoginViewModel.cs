using CommunityToolkit.Mvvm.ComponentModel;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;

        [ObservableProperty]
        private string? _username;

        [ObservableProperty]
        private string? _pass;

        public LoginViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
    }
}
