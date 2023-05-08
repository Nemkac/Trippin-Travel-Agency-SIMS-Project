using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class PastBookingsViewModel : ViewModelBase
    {
        private UserService userService = new UserService();
        private BookingService bookingService = new(new BookingRepository());
        private AccommodationRateService accommodationRateService = new(new AccommodationRateRepository());
        public ViewModelCommand GoToRate { get; set; }
        public ViewModelCommand GoToPreviousWindow { get; set; }


        public PastBookingsViewModel()
        {
            PastBookingsGrid = new ObservableCollection<Booking>(userService.GetGuestsPastBookings(LoggedUser.id));
            GoToRate = new ViewModelCommand(ShowRateInterface);
            GoToPreviousWindow = new ViewModelCommand(GoBack);

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

        private ObservableCollection<Booking> pastBookingsGrid;
        public ObservableCollection<Booking> PastBookingsGrid
        {
            get { return pastBookingsGrid; }
            set
            {
                if (pastBookingsGrid != value)
                {
                    pastBookingsGrid = value;
                    OnPropertyChanged(nameof(PastBookingsGrid));
                }
            }
        }

        private void GoBack(object sender)
        {
            GuestOneStaticHelper.futureBookingsInterface.Show();
            GuestOneStaticHelper.pastBookingsInterface.Close();
        }

        private void ShowRateInterface(object sender)
        {
            if (bookingService.CheckIfValidForRating(SelectedBooking) && !accommodationRateService.isPreviouslyRated((SelectedBooking).Id))
            {
                RateAccommodationInterface rateAccommodationInterface = new RateAccommodationInterface();
                GuestOneStaticHelper.selectedBookingIdToRate = SelectedBooking.Id;
                rateAccommodationInterface.WindowStartupLocation = WindowStartupLocation.Manual;
                rateAccommodationInterface.Left = GuestOneStaticHelper.pastBookingsInterface.Left;
                rateAccommodationInterface.Top = GuestOneStaticHelper.pastBookingsInterface.Top;
                rateAccommodationInterface.Show();
                GuestOneStaticHelper.pastBookingsInterface.Hide();
                WarningText = string.Empty;
            }
            else
            {
                WarningText = "You cannot leave a review of a booking older then 5 days";
            }
        }
    }
}
