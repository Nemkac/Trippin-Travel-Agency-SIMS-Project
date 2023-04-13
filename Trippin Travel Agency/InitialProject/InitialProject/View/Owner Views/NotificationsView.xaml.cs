using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels.OwnerViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingService bookingService;
        private GuestRateService guestRateService;
        public NotificationsView()
        {
            InitializeComponent();
            SendNotification();
            this.accommodationRepository = new AccommodationRepository();
            this.accommodationService = new AccommodationService(accommodationRepository);
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            this.guestRateService = new(new GuestRateRepository());
        }

        private void SendNotification()
        {
            GuestNotRatedNotification();
            BookingCanceledNotification();
        }

        public void BookingCanceledNotification()
        {
            DataBaseContext canceledContext = new DataBaseContext();
            List<CanceledBooking> canceledBookings = canceledContext.CanceledBookings.ToList();

            foreach(CanceledBooking booking in canceledBookings.ToList())
            {
                if(booking.seen == false)
                {
                    notificationsListBox.Items.Add($"Booking {booking.bookingId} has been canceled!");
                }
            }
        }

        private void GuestNotRatedNotification()
        {
            BookingService bookingService = new BookingService(new BookingRepository());
            DataBaseContext bookingContext = new DataBaseContext();
            foreach (Booking booking in bookingContext.Bookings.ToList())
            {
                if (ValidForNotification(booking))
                {
                    string guestUsername = bookingService.GetGuestName(booking.Id);
                    notificationsListBox.Items.Add($"Guest {guestUsername} has not been rated yet for booking: " + $"{booking.Id}!");
                }
            }
        }

        private static bool ValidForNotification(Booking booking)
        {
            GuestRateService guestRateService = new(new GuestRateRepository());
            DateTime departureDate = DateTime.ParseExact(booking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSinceDeparture = currentDate - departureDate;
            if (timeSinceDeparture.TotalDays <= 5 && timeSinceDeparture.TotalDays >= 0 && !guestRateService.IsRated(booking.Id)) return true;
            return false;
        }

        private void HandleSelections(object sender, SelectionChangedEventArgs e)
        {
            int canceledBookingId = MarkCanceledBookingNotificationAsSeen();

            ShowGuestRatingView(canceledBookingId);
        }

        private void ShowGuestRatingView(int canceledBookingId)
        {
            if (notificationsListBox.SelectedItem != null && canceledBookingId == -1)
            {
                string selectedItem = notificationsListBox.SelectedItem.ToString().TrimEnd('!');
                int bookingId = int.Parse(selectedItem.Substring(selectedItem.LastIndexOf(": ") + 2));

                TransferSelectedBooking(bookingService, bookingId);

                (DataContext as NotificationsViewModel)?.ShowGuestRatingView(null);
            }
        }

        private int MarkCanceledBookingNotificationAsSeen()
        {
            if(notificationsListBox.SelectedItem == null)
            {
                return -1;
            }
            string canceledBookingNotification = notificationsListBox.SelectedItem.ToString();
            int index = canceledBookingNotification.IndexOf(' ');
            string bookingIdString = canceledBookingNotification.Substring(index + 1, canceledBookingNotification.IndexOf(' ', index + 1) - index - 1);

            int canceledBookingId;
            bool isBookingIdValid = int.TryParse(bookingIdString, out canceledBookingId);

            if (!isBookingIdValid)
            {
                return -1;
            }

            DataBaseContext canceledContext = new DataBaseContext();
            List<CanceledBooking> canceledBookings = canceledContext.CanceledBookings.ToList();
            foreach (CanceledBooking canceledBooking in canceledBookings.ToList())
            {
                if (canceledBooking.bookingId == canceledBookingId)
                {
                    canceledBooking.seen = true;
                    canceledContext.CanceledBookings.Update(canceledBooking);
                    canceledContext.SaveChanges();
                }
            }

            notificationsListBox.Items.Remove(notificationsListBox.SelectedItem);

            return canceledBookingId;
        }

        private static void TransferSelectedBooking(BookingService bookingService, int bookingId)
        {
            DataBaseContext transferContext = new DataBaseContext();
            DataBaseContext ratingContext = new DataBaseContext();
            Booking selectedBooking = bookingService.GetById(bookingId);

            var transfers = transferContext.SelectedRatingNotificationTransfer.ToList();
            transferContext.SelectedRatingNotificationTransfer.RemoveRange(transfers);
            transferContext.SaveChanges();

            BookingTransfer selectedBookingRatingNotification = new BookingTransfer(selectedBooking.Id, selectedBooking.guestId);
            ratingContext.SelectedRatingNotificationTransfer.Add(selectedBookingRatingNotification);
            ratingContext.SaveChanges();
        }
    }
}
