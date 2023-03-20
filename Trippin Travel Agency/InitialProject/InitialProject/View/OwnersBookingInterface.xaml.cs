using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Services;
using System;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnersBookingInterface.xaml
    /// </summary>
    public partial class OwnersBookingInterface : UserControl
    {
        public OwnersBookingInterface()
        {
            InitializeComponent();
            List<BookingDTO> dataGridData = ShowBookings();
            bookingDataGrid.ItemsSource = dataGridData;
            SendNotification();
        }

        private void SendNotification()
        {
            GuestRateService guestRateService = new GuestRateService();
            DataBaseContext bookingContext = new DataBaseContext();
            BookingService bookingService = new BookingService();
            foreach (Booking booking in bookingContext.Bookings.ToList())
            {
                if (ValidForNotification(guestRateService, booking))
                {
                    string guestUsername = bookingService.GetGuestName(booking.Id);
                    //MessageBox.Show($"Guest {guestUsername} has not been rated yet for booking: " + $"{booking.Id}!");
                    var msg = $"Guest {guestUsername} has not been rated yet for booking: " + $"{booking.Id}!";
                    Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(msg)));
                }
            }
        }

        private List<BookingDTO> ShowBookings()
        {
            GuestRateService guestRateService = new GuestRateService();
            BookingService bookingService = new BookingService();
            DataBaseContext bookingContext = new DataBaseContext();
            List<BookingDTO> dataList = new List<BookingDTO>();
            BookingDTO dto = new BookingDTO();

            foreach (Booking booking in bookingContext.Bookings.ToList())
            {
                dto = bookingService.CreateDTO(booking);
                dataList.Add(dto);
            }

            return dataList;
        }

        private static bool ValidForNotification(GuestRateService guestRateService, Booking booking)
        {
            DateTime departureDate = DateTime.ParseExact(booking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSinceDeparture = currentDate - departureDate;
            if (timeSinceDeparture.TotalDays <= 5 && timeSinceDeparture.TotalDays >= 0 && !guestRateService.IsRated(booking.Id)) return true;
            return false;
        }
        
        private void RateGuest(object sender, RoutedEventArgs e)
        {
            DataBaseContext isAlreadyRated = new DataBaseContext();
            List<GuestRate> rates = isAlreadyRated.GuestRate.ToList();
            Button button = sender as Button;
            BookingDTO bookingDTO = button.DataContext as BookingDTO;
            int bookingId = bookingDTO.bookingId;
            foreach (GuestRate rate in rates)
            {
                if (rate.bookingId == bookingDTO.bookingId)
                {
                    MessageBox.Show("Guest is already rated!");
                    return;
                }
            }
            RateGuestInterface rateGuestInterface = new RateGuestInterface(bookingId);
            rateGuestInterface.Show();
        }
    }
}
