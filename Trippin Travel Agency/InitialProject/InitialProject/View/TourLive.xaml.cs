using Dapper;
using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.View
{
    public partial class TourLive : Window
    {
        private readonly TourService tourService = new TourService();
        private readonly Tour selectedTour;
        private readonly List<KeyPoint> keyPointsList = new List<KeyPoint>();

        public TourLive(Tour selectedTour)
        {
            InitializeComponent();

            this.selectedTour = selectedTour;
            DataContext = selectedTour;

            LoadKeyPoints();
            DisplayKeyPoints();
            UpdateFirstKeyPointToVisited();
        }

        private void LoadKeyPoints()
        {
            using (var db = new DataBaseContext())
            {
                keyPointsList.AddRange(db.KeyPoints.Where(kp => kp.tourId == selectedTour.id));
            }
        }

        private void SubscribeToKeyPointChanges()
        {
            foreach (var keyPoint in keyPointsList)
            {
                keyPoint.PropertyChanged += KeyPoint_PropertyChanged;
            }
        }

        private void DisplayKeyPoints()
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

        private void LeadCreateTour(object sender, RoutedEventArgs e)
        {
            new TourInterface().Show();
        }

        private void LeadTrackTourLive(object sender, RoutedEventArgs e)
        {
            new TrackTourLiveInterface().Show();
        }

        public void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 208));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(64, 115, 158));
            }
        }

        public void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(64, 115, 158));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(245, 246, 250));
            }
        }

        public void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedKeyPoint = (KeyPoint)keyPointsDataGrid.SelectedItem;
            selectedKeyPoint.visited = true;

            using (var db = new DataBaseContext())
            {
                db.KeyPoints.Update(selectedKeyPoint);
                db.SaveChanges();
            }

            RefreshKeyPoints();
            if (tourService.IsTourFinished(keyPointsList))
            {
                EndTour();
            }
        }

        private void RefreshKeyPoints()
        {
            using (var db = new DataBaseContext())
            {
                keyPointsList.Clear();
                keyPointsList.AddRange(db.KeyPoints.Where(kp => kp.tourId == selectedTour.id));
            }
            keyPointsDataGrid.Items.Refresh();
        }

        private void EndTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to end the tour?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                EndTour();
            }
        }

        private void EndTour()
        {
            MessageBox.Show("Tour finished");
            //Close();
            DataBaseContext dbContext = new DataBaseContext();
            selectedTour.active = false;
            TourManager.ActiveTours.Remove(selectedTour);
            dbContext.Update(selectedTour);
            dbContext.SaveChanges();

        }

        private void KeyPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(KeyPoint.visited))
            {
                keyPointsDataGrid.Items.Refresh();
            }
        }
    }
}
