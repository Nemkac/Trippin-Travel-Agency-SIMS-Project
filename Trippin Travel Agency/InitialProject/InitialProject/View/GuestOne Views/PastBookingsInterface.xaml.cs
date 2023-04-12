﻿using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Model;


namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for PastBookingsInterface.xaml
    /// </summary>
    public partial class PastBookingsInterface : Window
    {
        public PastBookingsInterface()
        {
            InitializeComponent();
            this.Loaded += ShowPastBookings;
        }

        private void ShowPastBookings(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            UserService userService = new UserService();
            List<Booking> bookings = userService.GetGuestsPastBookings(LoggedUser.id);
            this.pastBookingsGrid.ItemsSource = bookings;
        }

        private void ShowRateInterface(object sender, RoutedEventArgs e)
        {   
            BookingService bookingService = new BookingService();
            AccommodationRateService accommodationRateService = new AccommodationRateService();
            if (bookingService.CheckIfValidForRating((Booking)pastBookingsGrid.SelectedItem) && !accommodationRateService.isPreviouslyRated(((Booking)pastBookingsGrid.SelectedItem).Id))
            {
                RateAccommodationInterface RateAccommodationInterface = new RateAccommodationInterface();
                RateAccommodationInterface.SetAttributes(((Booking)pastBookingsGrid.SelectedItem).Id);
                RateAccommodationInterface.WindowStartupLocation = WindowStartupLocation.Manual;
                RateAccommodationInterface.Left = this.Left;
                RateAccommodationInterface.Top = this.Top;
                RateAccommodationInterface.Show();
                this.Close();
            } else
            {
                warningBlock.Text = "ne moze";
            }
        }
    }
}