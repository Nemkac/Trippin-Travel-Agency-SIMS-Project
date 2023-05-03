using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class SendBookingDelaymentViewModel : ViewModelBase
    {
        public ViewModelCommand SendRequest { get; set; }
        private BookingDelaymentRequestService bookingDelaymentRequestService = new(new BookingDelaymentRequestRepository());
        public SendBookingDelaymentViewModel()
        {
            SelectedArrival = DateTime.Today;
            selectedDeparture = DateTime.Today;
            accommodationInfoLabels = "Booking ID:" + "\n\nInitial arrival date" + "\n\nInitiral departure date:";
            accommodationInfo = GuestOneStaticHelper.selectedBookingToDelay.Id + "\n\n" + GuestOneStaticHelper.selectedBookingToDelay.arrival + "\n\n" + GuestOneStaticHelper.selectedBookingToDelay.departure;
            SendRequest = new ViewModelCommand(SaveRequest);
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

        private void SaveRequest(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            BookingDelaymentRequest bookingDelaymentRequest = new BookingDelaymentRequest(GuestOneStaticHelper.selectedBookingToDelay.Id, selectedArrival, selectedDeparture, Status.Pending, new string(""));
            bookingDelaymentRequestService.Save(bookingDelaymentRequest);
        }


    }
}
