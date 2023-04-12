using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModels
{
    public class TourGuide_FinishedToursViewModel : ViewModelBase
    {
        public ViewModelCommand ShowToursCommand { get; private set; }
        public ViewModelCommand ShowTourDataCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_FinishedToursViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowToursCommand = new ViewModelCommand(ShowTours);
            ShowTourDataCommand = new ViewModelCommand(ShowTourData);
        }

        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);
        }
        public void ShowTourData(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideTourDataViewCommand(null);
        }
    }
}
