using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_TourLiveViewModel : ViewModelBase
    {
        private readonly TourService _tourService = new(new TourRepository());
        private readonly TourReservationService _tourReservationService = new(new TourReservationRepository());
        public ObservableCollection<TourReservationsTodayDTO> _tourReservationDtosToday { get; set; } = new ObservableCollection<TourReservationsTodayDTO>();
        public TourReservationsTodayDTO selectedTourReservationDTO;
        public TourReservationsTodayDTO SelectedTourReservationDTO
        {
            get { return selectedTourReservationDTO; }
            set
            {
                if (selectedTourReservationDTO != value)
                {
                    selectedTourReservationDTO = value;
                    OnPropertyChanged(nameof(SelectedTourReservationDTO));
                }
            }
        }
        public ObservableCollection<KeyPoint> _keyPointsList { get; set; } = new ObservableCollection<KeyPoint>();
        public KeyPoint selectedKeyPoint;
        public KeyPoint SelectedKeyPoint
        {
            get { return selectedKeyPoint; }
            set
            {
                if (selectedKeyPoint != value)
                {
                    selectedKeyPoint = value;
                    OnPropertyChanged(nameof(SelectedKeyPoint));
                }
            }
        }
        private string _tourName;
        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }
        public TourGuide_MainViewModel _mainViewModel;
        public ViewModelCommand ToursTodayCommand { get; set; }
        public ViewModelCommand VisitCheckpointCommand { get; set; }
        public ViewModelCommand GuideConfirmedCommand { get; set; }
        public ViewModelCommand EndTourCommand { get; set; }

        public TourGuide_TourLiveViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ToursTodayCommand = new ViewModelCommand(ToursToday);
            VisitCheckpointCommand = new ViewModelCommand(VisitCheckpoint);
            GuideConfirmedCommand = new ViewModelCommand(GuideConfirmed);
            EndTourCommand = new ViewModelCommand(EndTour);
            LoadData();
        }

        public void ToursToday(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursTodayViewCommand(null);
        }

        private void LoadData()
        {
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            tour.active = true;
            context.Update(tour);
            context.SaveChanges();
            TourName = tour.name;
            List<TourReservation> reservations = this._tourService.GetTourReservationsById(tour.id);
            foreach (TourReservation tr in reservations)
            {
                _tourReservationDtosToday.Add(_tourReservationService.transformTourReservationToDTO(tr));
            }
            LoadKeyPoints(tour);
            UpdateFirstKeyPointToVisited();
        }
        private void GetExact(out DataBaseContext context, out Tour tour)
        {
            context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            tour = this._tourService.GetById(requests.Last().tourId);
        }
        public void LoadKeyPoints(Tour tour)
        {
            using (var context = new DataBaseContext())
            {
                var keyPoints = context.KeyPoints.Where(kp => kp.tourId == tour.id);

                foreach (var keyPoint in keyPoints)
                {
                    _keyPointsList.Add(keyPoint);
                }
            }

        }
        private void UpdateFirstKeyPointToVisited()
        {
            _keyPointsList[0].visited = true;
            using (var db = new DataBaseContext())
            {
                db.KeyPoints.Update(_keyPointsList[0]);
                db.SaveChanges();
            }
        }
        public void VisitCheckpoint(object obj)
        {
            // Add your logic for visiting a checkpoint here
            var selectedKeyPoint = SelectedKeyPoint;
            selectedKeyPoint.visited = true;

            using (var db = new DataBaseContext())
            {
                db.KeyPoints.Update(selectedKeyPoint);
                db.SaveChanges();
            }

            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            tour.active = true;
            context.Update(tour);
            context.SaveChanges();

            //RefreshKeyPoints(tour);
            if (this._tourService.IsTourFinished(_keyPointsList))
            {
                this.EndTour(tour);
            }
        }

        public void GuideConfirmed(object obj)
        {
            // Add your logic for guide confirmation here
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            var selectedReservation = SelectedTourReservationDTO;
            TourReservation tr = _tourReservationService.GetById(selectedReservation.id);
            if (tr.guestJoined == true)
            {
                CreateMessage(context, tour, tr);
                tr.guideConfirmed = true;
                context.TourReservations.Update(tr);
                context.SaveChanges();
                RefreshTourReservations(tour);
            }
            else
            {
                MessageBox.Show("Guest must join in order to check him as arrived.");
            }
        }
        private void RefreshTourReservations(Tour tour)
        {
            ObservableCollection<TourReservationsTodayDTO> dtos = new ObservableCollection<TourReservationsTodayDTO>();
            List<TourReservation> reservations = this._tourService.GetTourReservationsById(tour.id);
            foreach (TourReservation tr in reservations)
            {
                dtos.Add(_tourReservationService.transformTourReservationToDTO(tr));
            }

            _tourReservationDtosToday = dtos;
        }
        private void CreateMessage(DataBaseContext context, Tour tour, TourReservation tr)
        {
            KeyPoint nextKeyPoint = _tourService.GetNextUnvisitedKeyPoint(_keyPointsList);
            if (nextKeyPoint != null)
            {
                TourMessage message = new TourMessage(tour.id, tr.guestId, nextKeyPoint.id, tr.guestNumber);
                context.Add(message);
            }
        }
        public void EndTour(object obj)
        {
            // Add your logic for ending the tour here
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            if (MessageBox.Show("Are you sure you want to end the tour?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
}
