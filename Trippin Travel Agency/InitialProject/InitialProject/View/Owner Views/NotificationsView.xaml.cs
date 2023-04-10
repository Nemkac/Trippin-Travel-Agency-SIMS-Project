using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using InitialProject.ViewModels;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : UserControl
    {
        public NotificationsView()
        {
            InitializeComponent();
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
                    notificationsListBox.Items.Add($"Guest {guestUsername} has not been rated yet for booking: " + $"{booking.Id}!");
                }
            }
        }

        private static bool ValidForNotification(GuestRateService guestRateService, Booking booking)
        {
            DateTime departureDate = DateTime.ParseExact(booking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSinceDeparture = currentDate - departureDate;
            if (timeSinceDeparture.TotalDays <= 5 && timeSinceDeparture.TotalDays >= 0 && !guestRateService.IsRated(booking.Id)) return true;
            return false;
        }

        private void ShowGuestRatingInterface(object sender, SelectionChangedEventArgs e)
        {
            if (notificationsListBox.SelectedItem != null)
            {
                BookingService bookingService = new BookingService();
                string selectedItem = notificationsListBox.SelectedItem.ToString().TrimEnd('!');
                int bookingId = int.Parse(selectedItem.Substring(selectedItem.LastIndexOf(": ") + 2));

                TransferSelectedBooking(bookingService, bookingId);

                (DataContext as NotificationsViewModel)?.ShowGuestRatingView(null);
            }
        }

        private static void TransferSelectedBooking(BookingService bookingService, int bookingId)
        {
            DataBaseContext transferContext = new DataBaseContext();
            DataBaseContext ratingContext = new DataBaseContext();
            Booking selectedBookingRatingNotification = bookingService.GetById(bookingId);

            var transfers = transferContext.SelectedRatingNotificationTransfer.ToList();
            transferContext.SelectedRatingNotificationTransfer.RemoveRange(transfers);
            transferContext.SaveChanges();

            ratingContext.SelectedRatingNotificationTransfer.Add(selectedBookingRatingNotification);
            ratingContext.SaveChanges();
        }
    }
}
