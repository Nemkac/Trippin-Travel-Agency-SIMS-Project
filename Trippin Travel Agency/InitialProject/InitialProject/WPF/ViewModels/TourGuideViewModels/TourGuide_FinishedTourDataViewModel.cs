using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_FinishedTourDataViewModel : ViewModelBase
    {
        public ViewModelCommand ShowFinishedToursCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_FinishedTourDataViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowFinishedToursCommand = new ViewModelCommand(ShowFinishedTours);
        }

        public void ShowFinishedTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideFinishedToursViewCommand(null);
        }
    }
}
