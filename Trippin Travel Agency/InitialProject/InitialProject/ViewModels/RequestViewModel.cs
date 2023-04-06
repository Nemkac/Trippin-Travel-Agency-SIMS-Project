using InitialProject.DTO;
using InitialProject.View;
using InitialProject.View.Owner_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        private RequestDTO _selectedRequest;
        public RequestDTO SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                _selectedRequest = value;
                OnPropertyChanged("SelectedRequest");
            }
        }

        public ViewModelCommand ShowAcceptDenyViewCommand { get; private set; }
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public RequestViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowAcceptDenyViewCommand = new ViewModelCommand(ShowAcceptDenyView);
        }

        public void ShowAcceptDenyView(object obj)
        {
            _mainViewModel.ExecuteShowAcceptDenyViewCommand(null);
        }
    }
}
