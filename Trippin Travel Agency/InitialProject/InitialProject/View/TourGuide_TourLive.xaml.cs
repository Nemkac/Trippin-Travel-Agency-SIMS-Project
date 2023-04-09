using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
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
        private readonly TourService tourService = new TourService();
        private readonly TourReservationService tourReservationService = new TourReservationService();
        private readonly TourGuide_TourLiveViewModel tourLiveViewModel = new TourGuide_TourLiveViewModel();
        public TourGuide_TourLive()
        {
            InitializeComponent();
            this.Loaded += tourDataLoaded;    
        }
        
        public void tourDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = tourService.GetById(requests.Last().tourId);
            tour.active = true;
            context.Update(tour);
            context.SaveChanges(); 
            this.headerTextBlock.Text = tour.name;
            List<TourReservation> reservations = tourService.GetTourReservationsById(tour.id);
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

            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = tourService.GetById(requests.Last().tourId);
            tour.active = true;
            context.Update(tour);
            context.SaveChanges();

            RefreshKeyPoints(tour);
            if (tourService.IsTourFinished(keyPointsList))
            {
                tourLiveViewModel.EndTour(tour);
            }
        }

        public void GuideConfirmed_ButtonClick(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = tourService.GetById(requests.Last().tourId);
            var selectedReservation = (TourReservationsTodayDTO)guestReservationsDataGrid.SelectedItem;
            TourReservation tr = tourReservationService.GetById(selectedReservation.id);
            tr.guideConfirmed = true; 
            using (var db = new DataBaseContext())
            {
                db.TourReservations.Update(tr);
                db.SaveChanges();
            }
            RefreshTourReservations(tour);  
        }
        public void endTourButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = tourService.GetById(requests.Last().tourId);
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
            List<TourReservation> reservations = tourService.GetTourReservationsById(tour.id);
            foreach (TourReservation tr in reservations)
            {
                dtos.Add(tourReservationService.transformTourReservationToDTO(tr));
            }

            guestReservationsDataGrid.ItemsSource = dtos;
        }

    }
}
