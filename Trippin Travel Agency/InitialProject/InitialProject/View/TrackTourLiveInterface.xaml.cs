using InitialProject.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InitialProject.Service;
using InitialProject.Context;
using System;

namespace InitialProject.View
{
    public partial class TrackTourLiveInterface : Window
    {
        public TrackTourLiveInterface()
        {
            InitializeComponent();
            /*TourService tourService = new TourService();
            List<String> toursToday = tourService.GetToursToday();
            tourDataGrid.ItemsSource = toursToday;*/
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

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 208));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(64, 115, 158));
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(64, 115, 158));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(245, 246, 250));
            }
        }

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            Tour selectedTour = tourDataGrid.SelectedItem as Tour;
            DataBaseContext dbContext = new DataBaseContext();

            if (selectedTour != null)
            {
                selectedTour.active = true;
                TourManager.ActiveTours.Add(selectedTour);
                dbContext.Update(selectedTour);
                dbContext.SaveChanges();    

                if (TourManager.ActiveTours.Count > 1)
                {
                    TourManager.ActiveTours.Remove(selectedTour);
                    MessageBox.Show("You cannot have more than one active tour");
                    return;
                }

                // Show the TourLive window
                TourLive tourLive = new TourLive(selectedTour);
                tourLive.DataContext = selectedTour;
                tourLive.Show();
            }
        }

    }
}
