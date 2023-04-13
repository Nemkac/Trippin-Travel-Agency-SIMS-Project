using InitialProject.DTO;
using InitialProject.View.Owner_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModels.OwnerViewModels
{
    public class AcceptDenyViewModel : ViewModelBase
    {

        private readonly OwnerInterfaceViewModel _mainViewModel;
        private readonly RequestDTO _selectedRequest;

        public AcceptDenyViewModel(OwnerInterfaceViewModel mainViewModel, RequestDTO selectedRequest)
        {
            _mainViewModel = mainViewModel;
            _selectedRequest = selectedRequest;
        }

        private string _oldArrival;

        public string OldArrival
        {
            get { return _oldArrival; }
            set
            {
                _oldArrival = _selectedRequest.oldArrival.ToString();
                OnPropertyChanged(nameof(OldArrival));
            }
        }
    }
}
