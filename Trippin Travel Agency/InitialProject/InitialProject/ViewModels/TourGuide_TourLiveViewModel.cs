using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Model;
using InitialProject.Context;
using InitialProject.Model.TransferModels;
using System.Windows;

namespace InitialProject.ViewModels
{
    public class TourGuide_TourLiveViewModel : ViewModelBase
    {
        private readonly TourService tourService = new TourService();
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

        public void EndTour(Tour tour)
        {
            MessageBox.Show("Tour finished");
            DataBaseContext dbContext = new DataBaseContext();
            tour.active = false;
            tour.finished = true;
            TourManager.ActiveTours.Remove(tour);
            dbContext.Update(tour);
            dbContext.SaveChanges();

        }
    }
}
