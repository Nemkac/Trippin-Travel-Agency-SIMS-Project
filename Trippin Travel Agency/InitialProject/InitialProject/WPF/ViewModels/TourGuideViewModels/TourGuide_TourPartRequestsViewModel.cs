using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_TourPartRequestsViewModel : ViewModelBase
    {
        public ViewModelCommand ShowRequestTimeSlotsCommand { get; private set; }
        public ViewModelCommand ShowRequestsCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_TourPartRequestsViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowRequestTimeSlotsCommand = new ViewModelCommand(ShowRequestTimeSlots);
            ShowRequestsCommand = new ViewModelCommand(ShowRequests);

        }

        public void ShowRequestTimeSlots(object obj)
        {

            _mainViewModel.ExecuteShowTourGuideRequestTimeSlotsViewCommand(null);
        }
        public void ShowRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideRequestsViewCommand(null);
        }

    }
}
