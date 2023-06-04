using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GuestsAccountViewModel : ViewModelBase
    {
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help { get; set; }
        private UserService userService = new UserService();
        bool isHelpOn = false;
        public GuestsAccountViewModel()
        {
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            Help = new ViewModelCommand(ShowHelp);
            SuperGuestText = GenerateSuperGuestText();
            IsSuperGuest();
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.guestsAccountInterface.Left + (GuestOneStaticHelper.guestsAccountInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.guestsAccountInterface.Top + (GuestOneStaticHelper.guestsAccountInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.guestsAccountInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private string superGuestText;
        public string SuperGuestText
        {
            get { return superGuestText; }
            set
            {
                if (superGuestText != value)
                {
                    superGuestText = value;
                    OnPropertyChanged(nameof(SuperGuestText));
                }
            }
        }

        private string bookingsInLastYearText;
        public string BookingsInLastYearText
        {
            get { return bookingsInLastYearText; }
            set
            {
                if (bookingsInLastYearText != value)
                {
                    bookingsInLastYearText = value;
                    OnPropertyChanged(nameof(BookingsInLastYearText));
                }
            }
        }


        private bool test;
        public bool Test
        {
            get { return test; }
            set
            {
                if (test != value)
                {
                    test = value;
                    OnPropertyChanged(nameof(Test));
                }
            }
        }


        private string discountPointsText;
        public string DiscountPointsText
        {
            get { return discountPointsText; }
            set
            {
                if (discountPointsText != value)
                {
                    discountPointsText = value;
                    OnPropertyChanged(nameof(DiscountPointsText));
                }
            }
        }

        private string discountPoints;
        public string DiscountPoints
        {
            get { return discountPoints; }
            set
            {
                if (discountPoints != value)
                {
                    discountPoints = value;
                    OnPropertyChanged(nameof(DiscountPoints));
                }
            }
        }

        private int numberOfReservations;
        public int NumberOfReservations
        {
            get { return numberOfReservations; }
            set
            {
                if (numberOfReservations != value)
                {
                    numberOfReservations = value;
                    OnPropertyChanged(nameof(NumberOfReservations));
                }
            }
        }

        private int progressBarValue;
        public int ProgressBarValue
        {
            get { return progressBarValue; }
            set
            {
                if (progressBarValue != value)
                {
                    progressBarValue = value;
                    OnPropertyChanged(nameof(ProgressBarValue));
                }
            }
        }

        private string helpPoints;
        public string HelpPoints
        {
            get { return helpPoints; }
            set
            {
                if (helpPoints != value)
                {
                    helpPoints = value;
                    OnPropertyChanged(nameof(HelpPoints));
                }
            }
        }

        private string helpBookings;
        public string HelpBookings
        {
            get { return helpBookings; }
            set
            {
                if (helpBookings != value)
                {
                    helpBookings = value;
                    OnPropertyChanged(nameof(HelpBookings));
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
                    OnPropertyChanged(nameof(HelpExit));
                }
            }
        }
        public void ShowHelp(object sedner)
        {
            if (isHelpOn)
            {
                HelpBookings = string.Empty;
                HelpPoints = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpPoints = "This represents the amount of points that you have available for spending";
                HelpBookings = "This is how many bookings you have in last one year on since acquiring last Super-Guest title";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        public string GenerateSuperGuestText()
        {
            return "This is a place where you can see how many discount points are there left.\n\nWhat are discount points and how they work?\n\n" +
                "By booking 10 accommodation in a time span of one year, you are acquiring 5 discount points.\n\nIf you accomplish this, " +
                "you are becoming what call a \"Super Guest\".\n\nOne discount point means one discount on your next booking.\n\n" +
                "These points last one year after acquiring, unless you keep the title of super guest !";
        }

        public void IsSuperGuest()
        {
            if (userService.IsSuperGuest() != null)
            {
                DiscountPoints = userService.IsSuperGuest().points.ToString();
                DiscountPointsText = "Number of discount points\n(lasting until " + userService.IsSuperGuest().titleAcquisition.AddYears(1).ToString().ToString().Substring(0, Math.Max(0, userService.IsSuperGuest().titleAcquisition.AddYears(1).ToString().Length - 11)) + " )";
                NumberOfReservations = userService.BookingsSinceSuperGuestAcquisition();
                BookingsInLastYearText = "Bookings since acquiring Super Guest title";
            }
            else if (userService.BookingsInLastYear() > 0 && userService.BookingsInLastYear() < 10)
            {
                DiscountPoints = "0";
                DiscountPointsText = "Number of points";
                NumberOfReservations = userService.BookingsInLastYear();
                BookingsInLastYearText = "Bookings in last one year";
            }
            else
            {
                DiscountPoints = "0";
                DiscountPointsText = "Number of points";
                NumberOfReservations = 0;
                BookingsInLastYearText = "Bookings in last one year";
            }
        }
    }
}
