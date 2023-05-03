using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class FutureBookingsViewModel : ViewModelBase
    {
        private BookingService bookingService = new(new BookingRepository());
        private UserService userService = new UserService();
        private AccommodationService accommodationService = new(new AccommodationRepository());
        public ViewModelCommand DeleteBooking { get; set; }
        public ViewModelCommand GoToDelayment { get; set; }
        public ViewModelCommand GoToPastBookings { get; set; }

        public FutureBookingsViewModel()
        {
            UpcomingBookingsGrid = new ObservableCollection<Booking>(userService.GetGuestsFutureBookings(LoggedUser.id));
            DeleteBooking = new ViewModelCommand(CancelBooking);
            GoToDelayment = new ViewModelCommand(GoToBookingDelayment);
            GoToPastBookings = new ViewModelCommand(ShowPastBookings);
        }


        private Booking selectedBooking;
        public Booking SelectedBooking
        {
            get { return selectedBooking; }
            set
            {
                if (selectedBooking != value)
                {
                    selectedBooking = value;
                    OnPropertyChanged(nameof(SelectedBooking));
                }
            }
        }

        private ObservableCollection<Booking> upcomingBookingsGrid;
        public ObservableCollection<Booking> UpcomingBookingsGrid
        {
            get { return upcomingBookingsGrid; }
            set
            {
                if (upcomingBookingsGrid != value)
                {
                    upcomingBookingsGrid = value;
                    OnPropertyChanged(nameof(UpcomingBookingsGrid));
                }
            }
        }

        private void CancelBooking(object sender)
        {
            BookingCancelationMessageService BookingCancelationMessageService = new BookingCancelationMessageService();
            bookingService.Delete(SelectedBooking);
            string message = "Booking with ID: " + SelectedBooking.Id + " has been canceled.";
            BookingCancelationMessage bookingCancelationMessage = new BookingCancelationMessage(message, SelectedBooking.Id);
            BookingCancelationMessageService.Save(bookingCancelationMessage);
            DataBaseContext canceledContext = new DataBaseContext();
            DateTime plannedArrival = DateTime.ParseExact(selectedBooking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
            CanceledBooking canceledBooking = new CanceledBooking(SelectedBooking.Id, SelectedBooking.accommodationId, plannedArrival);
            UpcomingBookingsGrid.Remove(SelectedBooking);
            canceledContext.CanceledBookings.Add(canceledBooking);
            canceledContext.SaveChanges();
        }

        private void GoToBookingDelayment(object sender)
        {
            if ((DateTime.Parse(SelectedBooking.arrival).Subtract(DateTime.Today)).Days >= (accommodationService.GetById(SelectedBooking.accommodationId)).bookingCancelPeriodDays)
            {
                GuestOneStaticHelper.selectedBookingToDelay = SelectedBooking;
                SendBookingDelaymentInterface sendBookingDelaymentInterface = new SendBookingDelaymentInterface();
                sendBookingDelaymentInterface.WindowStartupLocation = WindowStartupLocation.Manual;
                sendBookingDelaymentInterface.Left = GuestOneStaticHelper.futureBookingsInterface.Left;
                sendBookingDelaymentInterface.Top = GuestOneStaticHelper.futureBookingsInterface.Top;
                GuestOneStaticHelper.futureBookingsInterface.Close();
                sendBookingDelaymentInterface.Show();
            }
            else
            {
                // buking da ne moze da se otkaze
            }
        }
        private void ShowPastBookings(object sender)
        {
            PastBookingsInterface pastBookingsInterface = new PastBookingsInterface();
            pastBookingsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            pastBookingsInterface.Left = GuestOneStaticHelper.futureBookingsInterface.Left;
            pastBookingsInterface.Top = GuestOneStaticHelper.futureBookingsInterface.Top;
            pastBookingsInterface.Show();
            GuestOneStaticHelper.futureBookingsInterface.Close();
        }

    }
}
