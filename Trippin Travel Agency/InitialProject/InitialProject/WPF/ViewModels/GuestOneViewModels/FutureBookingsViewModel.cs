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
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class FutureBookingsViewModel : ViewModelBase
    {
        private BookingService bookingService = new(new BookingRepository());
        private UserService userService = new UserService();
        private AccommodationService accommodationService = new(new AccommodationRepository());
        public ViewModelCommand DeleteBooking { get; set; }
        public ViewModelCommand GoToDelayment { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand GoToPastBookings { get; set; }

        public FutureBookingsViewModel()
        {
            UpcomingBookingsGrid = new ObservableCollection<Booking>(userService.GetGuestsFutureBookings(LoggedUser.id));
            DeleteBooking = new ViewModelCommand(CancelBooking);
            GoToDelayment = new ViewModelCommand(GoToBookingDelayment);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
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

        private string warningText;
        public string WarningText
        {
            get { return warningText; }
            set
            {
                if (warningText != value)
                {
                    warningText = value;
                    OnPropertyChanged(nameof(WarningText));
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
            if (SelectedBooking != null)
            {
                if ((DateTime.Parse(SelectedBooking.arrival).Subtract(DateTime.Today)).Days >= (accommodationService.GetById(SelectedBooking.accommodationId)).bookingCancelPeriodDays)
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
                    WarningText = string.Empty;

                    GuestOneStaticHelper.futureBookingsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                    CancelationConfirmationMessageInterface cancelationConfirmationMessageInterface = new CancelationConfirmationMessageInterface();
                    cancelationConfirmationMessageInterface.Left = GuestOneStaticHelper.futureBookingsInterface.Left + (GuestOneStaticHelper.futureBookingsInterface.Width - cancelationConfirmationMessageInterface.Width) / 2;
                    cancelationConfirmationMessageInterface.Top = GuestOneStaticHelper.futureBookingsInterface.Top + (GuestOneStaticHelper.futureBookingsInterface.Height - cancelationConfirmationMessageInterface.Height) / 2; ;
                    cancelationConfirmationMessageInterface.Show();
                    cancelationConfirmationMessageInterface.Focus();
                }
                else
                {
                    WarningText = "Chosen booking cannot be canceled due to expired cancelation period";
                }
            } else
            {
                WarningText = "You must select one of your bookings";
            }
        }

        private void ShowPastBookings(object sender)
        {
            PastBookingsInterface pastBookingsInterface = new PastBookingsInterface();
            pastBookingsInterface.WindowStartupLocation = WindowStartupLocation.Manual;
            pastBookingsInterface.Left = GuestOneStaticHelper.futureBookingsInterface.Left;
            pastBookingsInterface.Top = GuestOneStaticHelper.futureBookingsInterface.Top;
            pastBookingsInterface.Show();
            WarningText = string.Empty;
            GuestOneStaticHelper.futureBookingsInterface.Hide();
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.futureBookingsInterface.Left + (GuestOneStaticHelper.futureBookingsInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.futureBookingsInterface.Top + (GuestOneStaticHelper.futureBookingsInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.futureBookingsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private void GoToBookingDelayment(object sender)
        {
            if (SelectedBooking != null)
            {
                GuestOneStaticHelper.selectedBookingToDelay = SelectedBooking;
                SendBookingDelaymentInterface sendBookingDelaymentInterface = new SendBookingDelaymentInterface();
                sendBookingDelaymentInterface.WindowStartupLocation = WindowStartupLocation.Manual;
                sendBookingDelaymentInterface.Left = GuestOneStaticHelper.futureBookingsInterface.Left;
                sendBookingDelaymentInterface.Top = GuestOneStaticHelper.futureBookingsInterface.Top;
                sendBookingDelaymentInterface.Show();
                WarningText = string.Empty;
                GuestOneStaticHelper.futureBookingsInterface.Hide();
            }
            else
            {
                WarningText = "You must select one of your bookings";
            }
        }
    }
}
