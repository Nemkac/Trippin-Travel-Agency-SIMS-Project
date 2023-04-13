using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_ToursTodayViewModel : ViewModelBase
    {
        public ViewModelCommand ShowToursCommands { get; private set; }
        public ViewModelCommand ShowTourLiveCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursTodayViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowToursCommands = new ViewModelCommand(ShowTours);
            ShowTourLiveCommand = new ViewModelCommand(ShowTourLive);
        }

        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);

        }
        public void ShowTourLive(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideTourLiveViewCommand(null);
        }









    }

}
