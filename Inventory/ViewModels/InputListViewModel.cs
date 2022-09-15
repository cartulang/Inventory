using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventory.Models;
using Inventory.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels
{
    partial class InputListViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore = null!;
        private readonly Input? _input = null!;
        [ObservableProperty]
        private ObservableCollection<Input>? _inputs = null!;

        public InputListViewModel(NavigationStore navigationStore)
        {
            _input = new();

            _inputs = new();

            var inputs = Task.Run(_input.GetInventory).Result.OrderByDescending(d => d.Date);
            foreach (var input in inputs)
            {
                _inputs.Add(input);
            }

            _navigationStore = navigationStore;
        }

        [RelayCommand]
        private void NavigateToInputView()
        {
            _navigationStore.CurrentViewModel = new InputViewModel(_navigationStore);
        }
    }
}
