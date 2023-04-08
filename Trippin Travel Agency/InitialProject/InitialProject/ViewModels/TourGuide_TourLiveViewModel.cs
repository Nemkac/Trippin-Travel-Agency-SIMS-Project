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
        private TourService _tourService;
        private Tour _selectedTour;

        public int TourId { get; set; }

        public TourGuide_MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
            set { _mainViewModel = value; }
        }

        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public ViewModelCommand ShowToursTodayCommand { get; private set; }

        public TourGuide_TourLiveViewModel(TourGuide_MainViewModel mainViewModel, int tourId)
        {
            MainViewModel = mainViewModel;
            TourId = tourId;
            Initialize();
        }

        public TourGuide_TourLiveViewModel() { }

        private void Initialize()
        {
            _tourService = new TourService();
            SelectedTour = _tourService.GetById(TourId);
            ShowToursTodayCommand = new ViewModelCommand(ShowToursToday);
        }

        public void ShowToursToday(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursTodayViewCommand(null);
        }
    }
}
