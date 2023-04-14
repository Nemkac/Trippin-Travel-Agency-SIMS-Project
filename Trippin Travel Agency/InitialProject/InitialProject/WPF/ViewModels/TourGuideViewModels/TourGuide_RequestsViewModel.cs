using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_RequestsViewModel : ViewModelBase
    {
        public ViewModelCommand ShowDashboardCommand { get; private set; }
        public ViewModelCommand ShowFullTourRequestsCommand { get; private set; }
        public ViewModelCommand ShowTourPartRequestsCommand { get; private set; }
        public ViewModelCommand ShowRequestStatisticsCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_RequestsViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
            ShowFullTourRequestsCommand = new ViewModelCommand(ShowFullTourRequests);
            ShowTourPartRequestsCommand = new ViewModelCommand(ShowTourPartRequests);
            ShowRequestStatisticsCommand = new ViewModelCommand(ShowRequestStatistics);
        }

        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }

        public void ShowFullTourRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideFullTourRequestsViewCommand(null);
        }
        public void ShowTourPartRequests(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideTourPartRequestsViewCommand(null);
        }
        public void ShowRequestStatistics(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideRequestStatisticsViewCommand(null);
        }

    }
}
