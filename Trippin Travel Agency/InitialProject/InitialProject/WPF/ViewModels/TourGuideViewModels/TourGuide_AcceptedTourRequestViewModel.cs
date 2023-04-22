using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_AcceptedTourRequestViewModel : ViewModelBase
    {
        public ViewModelCommand ShowRequestsCommand { get; private set; }
        public ViewModelCommand ShowFullTourRequestsCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_AcceptedTourRequestViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowRequestsCommand = new ViewModelCommand(ShowRequests);
            ShowFullTourRequestsCommand = new ViewModelCommand(ShowFullTourRequests);
        }

        public void ShowRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideRequestsViewCommand(null);
        }

        public void ShowFullTourRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideFullTourRequestsViewCommand(null);
        }
    }
}
