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
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class PastBookingsViewModel : ViewModelBase
    {
        private UserService userService = new UserService();
        private BookingService bookingService = new(new BookingRepository());
        private AccommodationRateService accommodationRateService = new(new AccommodationRateRepository());
        bool isHelpOn = false;
        public ViewModelCommand GoToRate { get; set; }
        public ViewModelCommand GoToPreviousWindow { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand GoToRenovation { get; set; }
        public ViewModelCommand GoStatistics { get;set; }

        public PastBookingsViewModel()
        {
            PastBookingsGrid = new ObservableCollection<Booking>(userService.GetGuestsPastBookings(LoggedUser.id));
            GoToRate = new ViewModelCommand(ShowRateInterface);
            GoToPreviousWindow = new ViewModelCommand(GoBack);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            Help = new ViewModelCommand(ShowHelp);
            GoToRenovation = new ViewModelCommand(ShowRenovation);
            GoStatistics = new ViewModelCommand(GoToStatistics);
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

        private string helpGrid;
        public string HelpGrid
        {
            get { return helpGrid; }
            set
            {
                if (helpGrid != value)
                {
                    helpGrid = value;
                    OnPropertyChanged(nameof(HelpGrid));
                }
            }
        }

        private string helpChoose;
        public string HelpChoose
        {
            get { return helpChoose; }
            set
            {
                if (helpChoose != value)
                {
                    helpChoose = value;
                    OnPropertyChanged(nameof(HelpChoose));
                }
            }
        }

        private string helpExit;
        public string HelpExit
        {
            get { return helpExit; }
            set
            {
                if (helpExit != value)
                {
                    helpExit = value;
                    OnPropertyChanged(nameof(helpExit));
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

        public void GoToStatistics(object sender)
        {
            GenerateReportInterface generateReportInterface = new GenerateReportInterface();
            generateReportInterface.Top = GuestOneStaticHelper.pastBookingsInterface.Top;
            generateReportInterface.Left = GuestOneStaticHelper.pastBookingsInterface.Left;
            generateReportInterface.Show();
            GuestOneStaticHelper.pastBookingsInterface.Hide();
        }

        public void ShowRenovation(object sender)
        {
            if (SelectedBooking != null)
            {
                GuestOneStaticHelper.selectedBookingIdToRate = SelectedBooking.Id;
                RenovationSuggestionInterface renovationSuggestionInterface = new RenovationSuggestionInterface();
                renovationSuggestionInterface.Top = GuestOneStaticHelper.pastBookingsInterface.Top;
                renovationSuggestionInterface.Left = GuestOneStaticHelper.pastBookingsInterface.Left;
                renovationSuggestionInterface.Show();
                GuestOneStaticHelper.pastBookingsInterface.Hide();
            } else
            {
                WarningText = "You must first select booking";
            }
        }

        public void ShowHelp(object sender)
        {
            if(isHelpOn)
            {
                HelpGrid = string.Empty;
                HelpExit = string.Empty;
                HelpChoose = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpGrid = "Access the list of bookings by pressing TAB, then go through them with UP and DOWN arrows";
                HelpChoose = "When booking is selected you can press R to leave a review of it or press S to send a renovation idea to owner";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private void ShowNavigator(object sender)
        {
            GuestOneStaticHelper.InterfaceToGoBack = GuestOneStaticHelper.pastBookingsInterface;
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.pastBookingsInterface.Left + (GuestOneStaticHelper.pastBookingsInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.pastBookingsInterface.Top + (GuestOneStaticHelper.pastBookingsInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.pastBookingsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private void GoBack(object sender)
        {
            GuestOneStaticHelper.futureBookingsInterface.Show();
            GuestOneStaticHelper.pastBookingsInterface.Close();
        }

        private void ShowRateInterface(object sender)
        {
            if (SelectedBooking == null)
            {
                WarningText = "You must first select booking";
            }
            else if (!bookingService.CheckIfValidForRating(SelectedBooking))
            {
                WarningText = "You cannot leave a review of a booking older then 5 days";
            }
            else if (accommodationRateService.isPreviouslyRated((SelectedBooking).Id))
            {
                WarningText = "You have already left a review of a selected booking";
            }
            else
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
        }
    }
}
