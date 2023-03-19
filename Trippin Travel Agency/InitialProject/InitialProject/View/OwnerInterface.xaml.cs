using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerInterface.xaml
    /// </summary>
    public partial class OwnerInterface : Window
    {

        public OwnerInterface()
        {
            InitializeComponent();
        }

        private void ShowBookings(object sender, RoutedEventArgs e)
        {
            OwnersBookingDisplay bookingDisplay = new OwnersBookingDisplay();
            bookingDisplay.Show();
        }

        private void RegisterNewAccommodation(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationInterface accommodationRegistrationInterface = new AccommodationRegistrationInterface();
            accommodationRegistrationInterface.Show();
        }
    }
}