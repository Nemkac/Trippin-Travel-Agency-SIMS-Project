using InitialProject.DTO;
using InitialProject.WPF.View.GuestOne_Views;
using InitialProject.WPF.View.Owner_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        public ViewModelCommand ShowAcceptDenyViewCommand { get; private set; }
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public RequestViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowAcceptDenyViewCommand = new ViewModelCommand(ShowAcceptDenyView);
        }

        public RequestDTO SelectedRequest
        {
            get => _mainViewModel.RequestDataGridSelectedItem;
            set
            {
                _mainViewModel.RequestDataGridSelectedItem = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public void ShowAcceptDenyView(object obj)
        {
            _mainViewModel.ExecuteShowAcceptDenyViewCommand(null);
        }
    }
}
