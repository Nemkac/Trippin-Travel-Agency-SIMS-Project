using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class RenovationSuggestionViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private BookingService bookingService = new(new BookingRepository());
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand GoToPreviousWindow { get; set; }
        public ViewModelCommand SendRenovationSuggestion { get; set; }
        bool isHelpOn = false;
        public RenovationSuggestionViewModel()
        {
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            GoToPreviousWindow = new ViewModelCommand(ShowPastBookings);
            Help = new ViewModelCommand(ShowHelp);
            AccommodationName = (accommodationService.GetById((bookingService.GetById(GuestOneStaticHelper.selectedBookingIdToRate)).accommodationId)).name;
            SendRenovationSuggestion = new ViewModelCommand(SendSuggestion);
        }

        public void SendSuggestion(object sender)
        {
            if (Message == string.Empty || Message == null)
            {
                WarningMessage = "Please insert commentary";
            } 
            else
            {
                string messageToSend = "Renovation suggestion for accommodation with id: " + (accommodationService.GetById((bookingService.GetById(GuestOneStaticHelper.selectedBookingIdToRate)).accommodationId)).id.ToString();
                RenovationSuggestion renovationSuggestion = new RenovationSuggestion(Message, UrgencyRate,DateTime.Today);
                RenovationSuggestionMessage renovationSuggestionMessage = new RenovationSuggestionMessage(messageToSend, (accommodationService.GetById((bookingService.GetById(GuestOneStaticHelper.selectedBookingIdToRate)).accommodationId)).id);
                DataBaseContext saveContext = new DataBaseContext();
                saveContext.Attach(renovationSuggestion);
                saveContext.Attach(renovationSuggestionMessage);
                saveContext.SaveChanges();
                WarningMessage = string.Empty;
                SendSuggestionConfirmationInterface sendSuggestionConfirmationInterface = new SendSuggestionConfirmationInterface();
                sendSuggestionConfirmationInterface.Left = GuestOneStaticHelper.renovationSuggestionInterface.Left + (GuestOneStaticHelper.renovationSuggestionInterface.Width - sendSuggestionConfirmationInterface.Width) / 2;
                sendSuggestionConfirmationInterface.Top = GuestOneStaticHelper.renovationSuggestionInterface.Top + (GuestOneStaticHelper.renovationSuggestionInterface.Height - sendSuggestionConfirmationInterface.Height) / 2;
                GuestOneStaticHelper.renovationSuggestionInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                sendSuggestionConfirmationInterface.Show();

            }
        }

        public void ShowNavigator(object sender)
        {
            GuestOneStaticHelper.InterfaceToGoBack = GuestOneStaticHelper.renovationSuggestionInterface;
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.renovationSuggestionInterface.Left + (GuestOneStaticHelper.renovationSuggestionInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.renovationSuggestionInterface.Top + (GuestOneStaticHelper.renovationSuggestionInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.renovationSuggestionInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        public void ShowPastBookings(object sender)
        {
            GuestOneStaticHelper.pastBookingsInterface.Top = GuestOneStaticHelper.renovationSuggestionInterface.Top;
            GuestOneStaticHelper.pastBookingsInterface.Left = GuestOneStaticHelper.renovationSuggestionInterface.Left;
            GuestOneStaticHelper.renovationSuggestionInterface.Hide();
            GuestOneStaticHelper.pastBookingsInterface.Show();
        }

        public void ShowHelp(object sedner)
        {
            if (isHelpOn)
            {
                HelpIterate = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpIterate = "You can go from writing a comment to adjusting the rate of urgency, and vice versa, by pressing TAB";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private string helpIterate;
        public string HelpIterate
        {
            get { return helpIterate; }
            set
            {
                if (helpIterate != value)
                {
                    helpIterate = value;
                    OnPropertyChanged(nameof(HelpIterate));
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

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private string warningMessage;
        public string WarningMessage
        {
            get { return warningMessage; }
            set
            {
                if (warningMessage != value)
                {
                    warningMessage = value;
                    OnPropertyChanged(nameof(WarningMessage));
                }
            }
        }

        private int urgencyRate;
        public int UrgencyRate
        {
            get { return urgencyRate; }
            set
            {
                if (urgencyRate != value)
                {
                    urgencyRate = value;
                    OnPropertyChanged(nameof(UrgencyRate));
                }
            }
        }

    }
}
