using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Services;
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
            this.Loaded += ShowBookings;
        }

        private void ShowBookings(object sender, RoutedEventArgs e)
        {
            BookingService bookingService = new BookingService();
            DataBaseContext bookingContext = new DataBaseContext();
            List<BookingDTO> dataList = new List<BookingDTO>();
            BookingDTO dto = new BookingDTO();

            foreach(Booking booking in bookingContext.Bookings.ToList())
            {
                dto = bookingService.CreateDTO(booking);
                dataList.Add(dto);
            }

            this.bookingDataGrid.ItemsSource = dataList;
        }

        private void RegisterNewAccommodation(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationInterface accommodationRegistrationInterface = new AccommodationRegistrationInterface();
            accommodationRegistrationInterface.Show();
            this.Close();
        }

        private void RateGuest(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            BookingDTO bookingDTO = button.DataContext as BookingDTO;
            int bookingId = bookingDTO.bookingId;
            RateGuestInterface rateGuestInterface = new RateGuestInterface(bookingId);
            rateGuestInterface.Show();
        }
    }
}
