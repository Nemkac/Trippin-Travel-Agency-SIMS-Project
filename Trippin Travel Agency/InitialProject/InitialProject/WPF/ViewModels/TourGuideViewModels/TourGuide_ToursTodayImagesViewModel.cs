using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_ToursTodayImagesViewModel : ViewModelBase
    {
        public ViewModelCommand ToursTodayCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursTodayImagesViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ToursTodayCommand = new ViewModelCommand(ToursToday);
        }

        public void ToursToday(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursTodayViewCommand(null);
        }
    }
}
