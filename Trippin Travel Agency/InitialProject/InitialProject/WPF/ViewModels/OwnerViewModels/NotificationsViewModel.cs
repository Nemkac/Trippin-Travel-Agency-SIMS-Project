using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    internal class NotificationsViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
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

        public void ShowNewForumView(object obj)
        {
            _mainViewModel.ExecuteShowForumCommand(obj);
        }

        public void ShowGuestRatingView(object obj)
        {
            _mainViewModel.ExecuteShowGuestRatingCommand(obj);
        }

        private void SendNotification()
        {
            GuestNotRatedNotification();
            BookingCanceledNotification();
            NewForumIsOpenedNotification();
        }

        private void GuestNotRatedNotification()
        {
            BookingService bookingService = new BookingService(new BookingRepository());
            DataBaseContext bookingContext = new DataBaseContext();
            foreach (Booking booking in bookingContext.Bookings.ToList())
            {
                if (BookingValidForNotification(booking))
                {
                    string guestUsername = bookingService.GetGuestName(booking.Id);
                    Notifications.Add($"Guest {guestUsername} has not been rated yet for booking: " + $"{booking.Id}!");
                }
            }
        }

        private void NewForumIsOpenedNotification()
        {
            DataBaseContext forumNotificationContext = new DataBaseContext();
            List<ForumMessage> forumMessages = forumNotificationContext.ForumMessages.ToList();

            foreach(ForumMessage forumMessage in forumMessages)
            {
                if (ForumValidForNotification(forumMessage))
                {
                    Notifications.Add($"Forum {forumMessage.id} is opened at location Sremska Mitrovica, Serbia!");
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

        private static bool ForumValidForNotification(ForumMessage forumMessage)
        {
            AccommodationService accommodationService = new(new AccommodationRepository());
            AccommodationLocationService accommodationLocationService = new AccommodationLocationService();
            List<Accommodation> ownersAccommodations = accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);


            foreach(Accommodation accommodation in ownersAccommodations)
            {
                int accommodationLocationId = accommodationService.GetAccommodationLocationId(accommodation.id);

                if (forumMessage.locationId == accommodationLocationId && forumMessage.seen == false) return true;
            }

            return false;
        }

        private static bool BookingValidForNotification(Booking booking)
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
            int newForumMessageId = MarkNewForumNotificationAsSeen();

            ShowGuestRatingView(canceledBookingId, newForumMessageId);

            ShowNewForumView(null);
        }

        private void ShowGuestRatingView(int canceledBookingId, int newForumMessageId)
        {
            if (SelectedNotification != null && canceledBookingId == -1 && newForumMessageId == -1)
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

        private int MarkNewForumNotificationAsSeen()
        {
            if (SelectedNotification == null)
            {
                return -1;
            }


            string newForumNotification = SelectedNotification;

            int index = newForumNotification.IndexOf(' ');
            string forumIdString = newForumNotification.Substring(index + 1, newForumNotification.IndexOf(' ', index + 1) - index - 1);
            int newForumMessageId;

            bool isForumIdValid = int.TryParse(forumIdString, out newForumMessageId);
            if (!isForumIdValid)
            {
                return -1;
            }


            DataBaseContext forumMessageContext = new DataBaseContext();
            List<ForumMessage> newForumMessages = forumMessageContext.ForumMessages.ToList();
            
            foreach (ForumMessage forumMessage in newForumMessages.ToList())
             {
                 if (forumMessage.forumId == newForumMessageId)
                 {
                     forumMessage.seen = true;
                     forumMessageContext.ForumMessages.Update(forumMessage);
                     forumMessageContext.SaveChanges();
                 }
             }

            LoggedUser.VisitedForumId = newForumMessageId;

            Notifications.Remove(SelectedNotification);

            return newForumMessageId;
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
