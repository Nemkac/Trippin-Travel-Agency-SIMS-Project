using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View
{
    public partial class TourGuide_TourLive : UserControl
    {
        private readonly List<KeyPoint> keyPointsList = new List<KeyPoint>();
        List<TourReservationsTodayDTO> tourReservationDtosToday = new List<TourReservationsTodayDTO>();
        private readonly TourService tourService;
        private readonly TourReservationService tourReservationService;
        private readonly TourGuide_TourLiveViewModel tourLiveViewModel = new TourGuide_TourLiveViewModel();
        public TourGuide_TourLive()
        {
            InitializeComponent();
            this.Loaded += tourDataLoaded;
            this.tourReservationService = new(new TourReservationRepository());
            this.tourService = new(new TourRepository());
        }
        
        public void tourDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            tour.active = true;
            context.Update(tour);
            context.SaveChanges(); 
            this.headerTextBlock.Text = tour.name;
            List<TourReservation> reservations = this.tourService.GetTourReservationsById(tour.id);
            foreach (TourReservation tr in reservations)
            {
                tourReservationDtosToday.Add(tourReservationService.transformTourReservationToDTO(tr));
            }
            guestReservationsDataGrid.ItemsSource = tourReservationDtosToday;
            LoadKeyPoints(tour);
            SubscribeToKeyPointChanges();
            DisplayKeyPoints();
            UpdateFirstKeyPointToVisited();
        }
        public void LoadKeyPoints(Tour tour)
        {
            using (var db = new DataBaseContext())
            {
                keyPointsList.AddRange(db.KeyPoints.Where(kp => kp.tourId == tour.id));
            }
        }
        private void KeyPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(KeyPoint.visited))
            {
                keyPointsDataGrid.Items.Refresh();
            }
        }
        private void SubscribeToKeyPointChanges()
        {
            foreach (var keyPoint in keyPointsList)
            {
                keyPoint.PropertyChanged += KeyPoint_PropertyChanged;
            }
        }
        public void DisplayKeyPoints()
        {
            keyPointsDataGrid.ItemsSource = keyPointsList;
        }
        private void UpdateFirstKeyPointToVisited()
        {
            keyPointsList[0].visited = true;
            using (var db = new DataBaseContext())
            {
                db.KeyPoints.Update(keyPointsList[0]);
                db.SaveChanges();
            }
        }
        public void VisitCheckpointButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedKeyPoint = (KeyPoint)keyPointsDataGrid.SelectedItem;
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

            RefreshKeyPoints(tour);
            if (this.tourService.IsTourFinished(keyPointsList))
            {
                tourLiveViewModel.EndTour(tour);
            }
        }

        public void GuideConfirmed_ButtonClick(object sender, RoutedEventArgs e)
        {
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            var selectedReservation = (TourReservationsTodayDTO)guestReservationsDataGrid.SelectedItem;
            TourReservation tr = tourReservationService.GetById(selectedReservation.id);
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

        private void GetExact(out DataBaseContext context, out Tour tour)
        {
            context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            tour = this.tourService.GetById(requests.Last().tourId);
        }

        private void CreateMessage(DataBaseContext context, Tour tour, TourReservation tr)
        {
            KeyPoint nextKeyPoint = tourService.GetNextUnvisitedKeyPoint(keyPointsList);
            if (nextKeyPoint != null)
            {
                TourMessage message = new TourMessage(tour.id, tr.guestId, nextKeyPoint.id, tr.guestNumber);
                context.Add(message);
            }
        }

        public void endTourButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext context;
            Tour tour;
            GetExact(out context, out tour);
            if (MessageBox.Show("Are you sure you want to end the tour?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                tourLiveViewModel.EndTour(tour);
            }
        }
        private void RefreshKeyPoints(Tour tour)
        {
            using (var db = new DataBaseContext())
            {
                keyPointsList.Clear();
                keyPointsList.AddRange(db.KeyPoints.Where(kp => kp.tourId == tour.id));
            }
            keyPointsDataGrid.Items.Refresh();
        }
        private void RefreshTourReservations(Tour tour)
        {
            List<TourReservationsTodayDTO> dtos = new List<TourReservationsTodayDTO>();
            List<TourReservation> reservations = this.tourService.GetTourReservationsById(tour.id);
            foreach (TourReservation tr in reservations)
            {
                dtos.Add(tourReservationService.transformTourReservationToDTO(tr));
            }

            guestReservationsDataGrid.ItemsSource = dtos;
        }

    }
}
