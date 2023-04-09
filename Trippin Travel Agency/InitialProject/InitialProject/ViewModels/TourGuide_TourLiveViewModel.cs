using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Model;

namespace InitialProject.ViewModels
{
    public class TourGuide_TourLiveViewModel : ViewModelBase
    {
        private TourGuide_MainViewModel _mainViewModel;

        public TourGuide_MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
            set { _mainViewModel = value; }
        }
        public ViewModelCommand ShowToursTodayCommand { get; private set; }

        public TourGuide_TourLiveViewModel(TourGuide_MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            ShowToursTodayCommand = new ViewModelCommand(ShowToursToday);
        }
        public TourGuide_TourLiveViewModel() { }

        public void ShowToursToday(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursTodayViewCommand(null);
        }
    }
}
