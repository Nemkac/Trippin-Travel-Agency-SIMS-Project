using InitialProject.DTO;
using InitialProject.View.Owner_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModels
{
    public class AcceptDenyViewModel : ViewModelBase
    {
        private RequestDTO _selectedRequest;
        public RequestDTO SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }
    }
}
