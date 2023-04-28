using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_FutureToursViewModel : ViewModelBase
    {
        public ViewModelCommand ShowToursCommand { get; private set; }
        public ICommand CancelTourCommand { get; private set; }

        private readonly TourGuide_MainViewModel _mainViewModel;

        private TourService tourService;

        public ObservableCollection<FutureToursDTO> _futureToursDtos { get; set; } = new ObservableCollection<FutureToursDTO>();

        private FutureToursDTO _selectedFutureTourDto;
        public FutureToursDTO SelectedFutureTourDto
        {
            get => _selectedFutureTourDto;
            set
            {
                _selectedFutureTourDto = value;
                OnPropertyChanged(nameof(SelectedFutureTourDto));
            }
        }

        public TourGuide_FutureToursViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            tourService = new(new TourRepository());
            ShowToursCommand = new ViewModelCommand(ShowTours);
            CancelTourCommand = new ViewModelCommand(CancelTour, CanCancelTour);
            RefreshFutureToursDataGrid();
        }

        private void RefreshFutureToursDataGrid()
        {
            _futureToursDtos.Clear();
            this.tourService = new(new TourRepository());
            List<Tour> futureTours = tourService.GetAllFutureTours();
            List<FutureToursDTO> futureToursDtos = new List<FutureToursDTO>();

            foreach (Tour tour in futureTours)
            {
                futureToursDtos.Add(this.tourService.createFutureToursDTO(tour));
            }
            foreach (FutureToursDTO f in futureToursDtos)
            {
                _futureToursDtos.Add(f);
            }
        }
        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);
        }
        private void CancelTour(object parameter)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Tour tourToDelete = dataBaseContext.Tours.FirstOrDefault(t => t.id == SelectedFutureTourDto.id);

                if (tourToDelete != null)
                {
                    TimeSpan timeUntilTourStart = tourToDelete.startDates - DateTime.Now;

                    if (timeUntilTourStart.TotalHours > 48)
                    {
                        dataBaseContext.Images.RemoveRange(dataBaseContext.Images.Where(i => i.tourId == tourToDelete.id));
                        dataBaseContext.KeyPoints.RemoveRange(dataBaseContext.KeyPoints.Where(k => k.tourId == tourToDelete.id));
                        dataBaseContext.TourAttendances.RemoveRange(dataBaseContext.TourAttendances.Where(ta => ta.tourId == tourToDelete.id));
                        dataBaseContext.TourLiveViewTransfers.RemoveRange(dataBaseContext.TourLiveViewTransfers.Where(tlt => tlt.tourId == tourToDelete.id));
                        dataBaseContext.TourMessages.RemoveRange(dataBaseContext.TourMessages.Where(tm => tm.tourId == tourToDelete.id));
                        dataBaseContext.TourReservations.RemoveRange(dataBaseContext.TourReservations.Where(tr => tr.tourId == tourToDelete.id));

                        dataBaseContext.Tours.Remove(tourToDelete);
                        foreach (TourReservation tr in dataBaseContext.TourReservations.ToList())
                        {
                            if (tr.id == tourToDelete.id)
                            {
                                Coupon coupon = new Coupon(tr.guestId, DateTime.Now.AddYears(1));
                                dataBaseContext.Coupons.Add(coupon);
                            }
                        }
                        dataBaseContext.SaveChanges();

                        MessageBox.Show("Tour has been cancelled.");

                        RefreshFutureToursDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("You cannot cancel tours that are less than 48 hours away.");
                    }
                }
                else
                {
                    MessageBox.Show("Error: The selected tour was not found in the database.");
                }
            }
        }
        private bool CanCancelTour(object parameter)
        {
            return SelectedFutureTourDto != null;
        }

        
    }
}
