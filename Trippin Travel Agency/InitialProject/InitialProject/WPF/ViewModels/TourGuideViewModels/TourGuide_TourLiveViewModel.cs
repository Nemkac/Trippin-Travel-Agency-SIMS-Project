using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Context;
using InitialProject.Model.TransferModels;
using System.Windows;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using System.Security.Cryptography.X509Certificates;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_TourLiveViewModel : ViewModelBase
    {
        private readonly TourService tourService = new(new TourRepository());
        private readonly TourReservationService tourReservationService = new(new TourReservationRepository());

        public TourGuide_MainViewModel _mainViewModel;
        public ViewModelCommand ToursTodayCommand { get; set; }

        public TourGuide_TourLiveViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ToursTodayCommand = new ViewModelCommand(ToursToday);
        }
        public void ToursToday(object obj)
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
