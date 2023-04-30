using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    public class GuestTwoMesagesViewModel : ViewModelBase
    {
        public ViewModelCommand DetailedTourViewCommand { get; private set; }

        private readonly GuestTwoInterfaceViewModel _mainViewModel;
        public GuestTwoMesagesViewModel(GuestTwoInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            DetailedTourViewCommand = new ViewModelCommand(ShowDetailedTourView);
        }

        public void ShowDetailedTourView(object obj)
        {
            _mainViewModel.ExecuteShowDetailedTourView(null);
        }
    }
}
