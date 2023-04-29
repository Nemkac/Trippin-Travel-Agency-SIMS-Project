using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.ViewModels;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    internal class NotificationsViewModel : ViewModelBase
    {
        private BookingService bookingService = new(new BookingRepository());
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ObservableCollection<string> Notifications
        {
            get { return notifications; }
            set
            {
                if (notifications != value)
                {
                    notifications = value;
                    OnPropertyChanged(nameof(Notifications));
                }
            }
        }
        private ObservableCollection<string> notifications = new ObservableCollection<string>();

        private string selectedNotification;
        public string SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                if (selectedNotification != value)
                {
                    selectedNotification = value;
                    HandleSelections();
                    OnPropertyChanged(nameof(SelectedNotification));
                }
            }
        }


        public ICommand ShowGuestRatingCommand;
        public NotificationsViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowGuestRatingCommand = new ViewModelCommand(ShowGuestRatingView);
            SendNotification();
        }

        public void ShowGuestRatingView(object obj)
        {
            _mainViewModel.ExecuteShowGuestRatingCommand(obj);
        }

        private void SendNotification()
        {
            GuestNotRatedNotification();
            BookingCanceledNotification();
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
                    Notifications.Add($"Guest {guestUsername} has not been rated yet for booking: " + $"{booking.Id}!");
                }
            }
        }
        public void BookingCanceledNotification()
        {
            DataBaseContext canceledContext = new DataBaseContext();
            List<CanceledBooking> canceledBookings = canceledContext.CanceledBookings.ToList();

            foreach (CanceledBooking booking in canceledBookings.ToList())
            {
                if (booking.seen == false)
                {
                    Notifications.Add($"Booking {booking.bookingId} has been canceled!");
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

        private void HandleSelections()
        {
            int canceledBookingId = MarkCanceledBookingNotificationAsSeen();

            ShowGuestRatingView(canceledBookingId);
        }

        private void ShowGuestRatingView(int canceledBookingId)
        {
            if (SelectedNotification != null && canceledBookingId == -1)
            {
                string selectedItem = SelectedNotification.TrimEnd('!');
                int bookingId = int.Parse(selectedItem.Substring(selectedItem.LastIndexOf(": ") + 2));

                TransferSelectedBooking(bookingService, bookingId);

                ShowGuestRatingView(null);
            }
        }

        private int MarkCanceledBookingNotificationAsSeen()
        {
            if (SelectedNotification == null)
            {
                return -1;
            }
            string canceledBookingNotification = SelectedNotification;
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

            Notifications.Remove(SelectedNotification);

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
