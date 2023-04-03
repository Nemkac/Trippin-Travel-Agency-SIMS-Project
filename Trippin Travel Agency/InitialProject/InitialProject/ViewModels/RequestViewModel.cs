using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModels
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

        public void ShowAcceptDenyView(object obj)
        {
            _mainViewModel.ExecuteShowAcceptDenyViewCommand(null);
        }
    }
}
