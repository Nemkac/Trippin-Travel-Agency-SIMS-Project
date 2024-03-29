﻿using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class SendBookingDelaymentViewModel : ViewModelBase
    {
        public ViewModelCommand SendRequest { get; set; }
        private BookingDelaymentRequestService bookingDelaymentRequestService = new(new BookingDelaymentRequestRepository());
        public ViewModelCommand GoToPreviousWindow { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help { get; set; }
        bool isHelpOn = false;
        public SendBookingDelaymentViewModel()
        {
            SelectedArrival = DateTime.Parse(GuestOneStaticHelper.selectedBookingToDelay.arrival);
            selectedDeparture = DateTime.Parse(GuestOneStaticHelper.selectedBookingToDelay.departure);
            accommodationInfoLabels = "Booking ID:" + "\n\nInitial arrival date" + "\n\nInitiral departure date:";
            accommodationInfo = GuestOneStaticHelper.selectedBookingToDelay.Id + "\n\n" + GuestOneStaticHelper.selectedBookingToDelay.arrival + "\n\n" + GuestOneStaticHelper.selectedBookingToDelay.departure;
            SendRequest = new ViewModelCommand(SaveRequest);
            GoToPreviousWindow = new ViewModelCommand(GoBack);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            Help = new ViewModelCommand(ShowHelp);

        }



        private DateTime selectedArrival;
        public DateTime SelectedArrival
        {
            get { return selectedArrival; }
            set
            {
                if (selectedArrival != value)
                {
                    selectedArrival = value;
                    OnPropertyChanged(nameof(SelectedArrival));
                }
            }
        }

        private DateTime selectedDeparture;
        public DateTime SelectedDeparture
        {
            get { return selectedDeparture; }
            set
            {
                if (selectedDeparture != value)
                {
                    selectedDeparture = value;
                    OnPropertyChanged(nameof(SelectedDeparture));
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

        private string accommodationInfoLabels;
        public string AccommodationInfoLabels
        {
            get { return accommodationInfoLabels; }
            set
            {
                if (accommodationInfoLabels != value)
                {
                    accommodationInfoLabels = value;
                    OnPropertyChanged(nameof(AccommodationInfoLabels));
                }
            }
        }

        private string accommodationInfo;
        public string AccommodationInfo
        {
            get { return accommodationInfo; }
            set
            {
                if (accommodationInfo != value)
                {
                    accommodationInfo = value;
                    OnPropertyChanged(nameof(AccommodationInfo));
                }
            }
        }

        private string helpDelay;
        public string HelpDelay
        {
            get { return helpDelay; }
            set
            {
                if (helpDelay != value)
                {
                    helpDelay = value;
                    OnPropertyChanged(nameof(HelpDelay));
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

        private void ShowHelp(object sender)
        {
            if(isHelpOn)
            {
                HelpDelay = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpDelay = "Here you can send a delayment request for one of your bookings.\n\nBy pressing TAB, you access arrival/departure date input.\nWhen date is entered, press ENTER to confirm it.\n\nOnce you entered dates you want, press S to send a request to owner.";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private void GoBack(object sender)
        {
            GuestOneStaticHelper.futureBookingsInterface.Show();
            GuestOneStaticHelper.sendBookingDelaymentInterface.Close();
        }

        private void ShowNavigator(object sender)
        {
            GuestOneStaticHelper.InterfaceToGoBack = GuestOneStaticHelper.sendBookingDelaymentInterface;
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.sendBookingDelaymentInterface.Left + (GuestOneStaticHelper.sendBookingDelaymentInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.sendBookingDelaymentInterface.Top + (GuestOneStaticHelper.sendBookingDelaymentInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.sendBookingDelaymentInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private void SaveRequest(object sender)
        {
            if (SelectedArrival == null && SelectedDeparture == null)
            {
                WarningText = "You must pick dates";
            }
            else if (SelectedArrival == null)
            {
                WarningText = "You must pick arrival date";
            }
            else if (SelectedDeparture == null)
            {
                WarningText = "You must pick departure date";
            }
            else if (SelectedArrival.Date <= DateTime.Today.Date)
            {
                WarningText = "Arrival date set to a past";
            }
            else if (SelectedArrival >= SelectedDeparture)
            {
                WarningText = "Departure date must be after arrival date";
            }
            else
            {
                DataBaseContext context = new DataBaseContext();
                List<Booking> bookings = context.Bookings.ToList();
                BookingDelaymentRequest bookingDelaymentRequest = new BookingDelaymentRequest(GuestOneStaticHelper.selectedBookingToDelay.Id, selectedArrival, selectedDeparture, Status.Pending, new string(""));
                bookingDelaymentRequestService.Save(bookingDelaymentRequest);
                WarningText = string.Empty;

                BookingDelaymentConfirmationInterface bookingDelaymentConfirmationInterface = new BookingDelaymentConfirmationInterface();
                bookingDelaymentConfirmationInterface.Left = GuestOneStaticHelper.sendBookingDelaymentInterface.Left + (GuestOneStaticHelper.sendBookingDelaymentInterface.Width - bookingDelaymentConfirmationInterface.Width) / 2;
                bookingDelaymentConfirmationInterface.Top = GuestOneStaticHelper.sendBookingDelaymentInterface.Top + (GuestOneStaticHelper.sendBookingDelaymentInterface.Height - bookingDelaymentConfirmationInterface.Height) / 2;
                GuestOneStaticHelper.sendBookingDelaymentInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                bookingDelaymentConfirmationInterface.Show();

            }
        }


    }
}
