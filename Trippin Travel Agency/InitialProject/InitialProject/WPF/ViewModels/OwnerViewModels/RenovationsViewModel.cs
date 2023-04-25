using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RenovationsViewModel : ViewModelBase
    {

        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ViewModelCommand ShowNewRenovationView { get; }

        public RenovationsViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowNewRenovationView = new ViewModelCommand(ExecuteShowNewRenovationView);
        }

        public void ExecuteShowNewRenovationView(object obj)
        {
            _mainViewModel.ExecuteShowNewRenovationViewCommand(null);
        }

    }
}
