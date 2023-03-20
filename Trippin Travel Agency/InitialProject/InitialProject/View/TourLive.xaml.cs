using Dapper;
using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using System;
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
        private TourService tourService = new TourService();
        private Tour selectedTour;

        public TourLive(Tour selectedTour)
        {
            InitializeComponent();

            this.selectedTour = selectedTour;

            DataContext = selectedTour;

            List<KeyPoint> keyPointsList = new List<KeyPoint>();
            using (var db = new DataBaseContext())
            {
                keyPointsList = db.KeyPoints.Where(kp => kp.tourId == selectedTour.id).ToList();
            }

            foreach (var keyPoint in keyPointsList)
            {
                keyPoint.PropertyChanged += KeyPoint_PropertyChanged;
            }

            keyPointsDataGrid.ItemsSource = keyPointsList;
            keyPointsList[0].visited = true;

            using (var db = new DataBaseContext())
            {
                db.KeyPoints.Update(keyPointsList[0]);
                db.SaveChanges();
            }

        }

        private void LeadCreateTour(object sender, RoutedEventArgs e)
        {
            TourInterface TourInterface = new TourInterface();
            TourInterface.Show();
        }

        private void LeadTrackTourLive(object sender, RoutedEventArgs e)
        {
            TrackTourLiveInterface TrackTourLiveInterface = new TrackTourLiveInterface();
            TrackTourLiveInterface.Show();
        }

        public void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 208));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(64, 115, 158));
            }
        }

        public void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(64, 115, 158));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(245, 246, 250));
            }
        }

        public void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint selectedKeyPoint = (KeyPoint)keyPointsDataGrid.SelectedItem;
            selectedKeyPoint.visited = true;

            using (var db = new DataBaseContext())
            {
                db.KeyPoints.Update(selectedKeyPoint);
                db.SaveChanges();
            }

            List<KeyPoint> keyPointsList = new List<KeyPoint>();
            using (var db = new DataBaseContext())
            {
                keyPointsList = db.KeyPoints.Where(kp => kp.tourId == selectedTour.id).ToList();
            }

            keyPointsDataGrid.ItemsSource = keyPointsList;

            if (tourService.IsTourFinished(keyPointsList))
            {
                MessageBox.Show("Tour finished");
                this.Close();  // close the window
                selectedTour.active = false;  // update the tour's active property
                TourManager.ActiveTours.Clear();  // clear the list in TourManager
            }
        }


        private void KeyPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "visited")
            {
                keyPointsDataGrid.Items.Refresh();
            }
        }

        private void EndTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to end the tour?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                List<KeyPoint> keyPointsList = new List<KeyPoint>();
                using (var db = new DataBaseContext())
                {
                    keyPointsList = db.KeyPoints.Where(kp => kp.tourId == selectedTour.id).ToList();
                }

                if (tourService.IsTourFinished(keyPointsList))
                {
                    MessageBox.Show("Tour finished");
                    this.Close();  // close the window
                    selectedTour.active = false;  // update the tour's active property
                    TourManager.ActiveTours.Clear();  // clear the list in TourManager
                }
                else
                {
                    MessageBox.Show("The tour is not finished yet.");
                }
            }
        }

    }
}
