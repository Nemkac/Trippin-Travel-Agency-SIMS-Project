using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_TourStatisticsViewModel : ViewModelBase
    {
        public ViewModelCommand ShowToursCommand { get; private set; }
        public ViewModelCommand ShowDashboardCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_TourStatisticsViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowToursCommand = new ViewModelCommand(ShowTours);
            ShowDashboardCommand = new ViewModelCommand(ShowDashboard);
        }

        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);
        }
        public void ShowDashboard(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideDashboardViewCommand(null);
        }
    }
}
