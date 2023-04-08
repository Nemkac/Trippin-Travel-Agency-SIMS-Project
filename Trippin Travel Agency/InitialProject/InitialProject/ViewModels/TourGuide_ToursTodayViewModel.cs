using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModels
{
    public class TourGuide_ToursTodayViewModel : ViewModelBase
    {
        /*private string ?_selectedTourName { get; set;  }

        public string SelectedTourName
        {
            get { return _selectedTourName; }
            set
            {
                _selectedTourName = value;
                OnPropertyChanged(nameof(SelectedTourName));
            }
        }*/

        public ViewModelCommand ShowToursCommands { get; private set; }
        public ViewModelCommand ShowTourLiveCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursTodayViewModel(TourGuide_MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowToursCommands = new ViewModelCommand(ShowTours);
            ShowTourLiveCommand = new ViewModelCommand(ShowTourLive);
        }

        //public TourGuide_ToursTodayViewModel() { }

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
