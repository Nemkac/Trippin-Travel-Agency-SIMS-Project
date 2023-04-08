using InitialProject.Model;
using InitialProject.Service;
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

        private object _selectedTour;
        public object SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                MessageBox.Show(SelectedTour.ToString());
            }
        }

        public ViewModelCommand ShowToursCommands { get; private set; }
        public ViewModelCommand ShowTourLiveCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;
        private readonly TourService tourService; 

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
        public void ShowTourLive(object tourid)
        {
            if (tourid != null)
            {
                _mainViewModel.ExecuteShowTourGuideTourLiveViewCommand((int)tourid);
            }
            else
            {
                MessageBox.Show("There is no selected tour.");
            }
        }









    }

}
