using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.View.Owner_Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AcceptDenyViewModel : ViewModelBase
    {
        private BookingService bookingService = new(new BookingRepository());
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private readonly OwnerInterfaceViewModel _mainViewModel;

        private string oldArrival;
        public string OldArrival
        {
            get { return oldArrival; }
            set
            {
                if (oldArrival != value)
                {
                    oldArrival = value;
                    OnPropertyChanged(nameof(OldArrival));
                }
            }
        }

        private string oldDeparture;
        public string OldDeparture
        {
            get { return oldDeparture; }
            set
            {
                if (oldDeparture != value)
                {
                    oldDeparture = value;
                    OnPropertyChanged(nameof(OldDeparture));
                }
            }
        }

        private string newArrival;
        public string NewArrival
        {
            get { return newArrival; }
            set
            {
                if (newArrival != value)
                {
                    newArrival = value;
                    OnPropertyChanged(nameof(NewArrival));
                }
            }
        }

        private string newDeparture;
        public string NewDeparture
        {
            get { return newDeparture; }
            set
            {
                if (newDeparture != value)
                {
                    newDeparture = value;
                    OnPropertyChanged(nameof(NewDeparture));
                }
            }
        }

        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set
            {
                if (guestName != value)
                {
                    guestName = value;
                    OnPropertyChanged(nameof(GuestName));
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

        private string accommodationType;
        public string AccommodationType
        {
            get { return accommodationType; }
            set
            {
                if (accommodationType != value)
                {
                    accommodationType = value;
                    OnPropertyChanged(nameof(AccommodationType));
                }
            }
        }

        private int bookingId;
        public int BookingId
        {
            get { return bookingId; }
            set
            {
                if (bookingId != value)
                {
                    bookingId = value;
                    OnPropertyChanged(nameof(BookingId));
                }
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        private string acceptFeedBack;
        public string AcceptFeedBack
        {
            get { return acceptFeedBack; }
            set
            {
                if (acceptFeedBack != value)
                {
                    acceptFeedBack = value;
                    OnPropertyChanged(nameof(AcceptFeedBack));
                }
            }
        }

        private string denyFeedBack;
        public string DenyFeedBack
        {
            get { return denyFeedBack; }
            set
            {
                if (denyFeedBack != value)
                {
                    denyFeedBack = value;
                    OnPropertyChanged(nameof(DenyFeedBack));
                }
            }
        }

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }

        public ViewModelCommand AcceptRequestCommand { get; set; }
        public ViewModelCommand DenyRequestCommand { get; set; }

        public AcceptDenyViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            AcceptRequestCommand = new ViewModelCommand(AcceptRequest);
            DenyRequestCommand = new ViewModelCommand(DenyRequest);

            DisplayData();

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }


        public void DisplayData()
        {
            DataBaseContext acceptDenyContext = new DataBaseContext();
            List<RequestDTO> requests = acceptDenyContext.SelectedRequestTransfers.ToList();
            FillBookingDataFields(requests);
        }

        private void FillBookingDataFields(List<RequestDTO> requests)
        {
            OldArrival = requests.First().oldArrival.ToString();
            OldDeparture = requests.First().oldDeparture.ToString();
            NewArrival = requests.First().newArrival.ToString();
            NewDeparture = requests.First().newDeparture.ToString();
            GuestName = bookingService.GetGuestName(requests.First().bookingId);
            Booking booking = bookingService.GetById(requests.First().bookingId);
            AccommodationName = (accommodationService.GetById(booking.accommodationId)).name;
            AccommodationType = (accommodationService.GetById(booking.accommodationId)).type.ToString();
            BookingId = booking.Id;
            List<string> location = accommodationService.GetAccommodationLocation(booking.accommodationId);
            Location = location[0] + ", " + location[1];
        }

        private void AcceptRequest(object obj)
        {
            DataBaseContext bookingContext = new DataBaseContext();
            DataBaseContext acceptContext = new DataBaseContext();

            List<Booking> bookings = bookingContext.Bookings.ToList();
            List<RequestDTO> selectedRequest = acceptContext.SelectedRequestTransfers.ToList();

            UpdateChanges(bookingContext, bookings, selectedRequest);

            DataBaseContext requestContext = new DataBaseContext();
            List<BookingDelaymentRequest> bookingDelaymentRequests = requestContext.BookingDelaymentRequests.ToList();

            UpdateAcceptedRequestStatus(selectedRequest, requestContext, bookingDelaymentRequests);

            AcceptFeedBack = "Request accepted!";
            acceptContext.SelectedRequestTransfers.Remove(acceptContext.SelectedRequestTransfers.First());
            acceptContext.SaveChanges();
            ClearInput();
        }

        private static void UpdateAcceptedRequestStatus(List<RequestDTO> selectedRequest, DataBaseContext requestContext, List<BookingDelaymentRequest> bookingDelaymentRequests)
        {
            BookingService bookingService = new(new BookingRepository());
            DataBaseContext delayedContext = new DataBaseContext();
            foreach (BookingDelaymentRequest request in bookingDelaymentRequests.ToList())
            {
                if (request.bookingId == selectedRequest.First().bookingId)
                {
                    request.status = Status.Accepted;
                    //requestContext.BookingDelaymentRequests.Remove(request);
                    requestContext.BookingDelaymentRequests.Update(request);
                    requestContext.SaveChanges();
                    Booking booking = bookingService.GetById(request.bookingId);
                    DateTime oldArrival = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                    DelayedBookings delayedBooking = new DelayedBookings(request.bookingId, booking.accommodationId, oldArrival);
                    delayedContext.DelayedBookings.Add(delayedBooking);
                    delayedContext.SaveChanges();
                }
            }
        }

        private static void UpdateChanges(DataBaseContext bookingContext, List<Booking> bookings, List<RequestDTO> selectedRequest)
        {
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.Id == selectedRequest.First().bookingId)
                {
                    booking.arrival = selectedRequest.First().newArrival.ToString("M/dd/yyyy");
                    booking.departure = selectedRequest.First().newDeparture.ToString("M/dd/yyyy");
                    bookingContext.SaveChanges();
                }
            }
        }

        private void DenyRequest(object obj)
        {
            DataBaseContext acceptContext = new DataBaseContext();
            List<RequestDTO> selectedRequest = acceptContext.SelectedRequestTransfers.ToList();
            DataBaseContext requestContext = new DataBaseContext();
            List<BookingDelaymentRequest> bookingDelaymentRequests = requestContext.BookingDelaymentRequests.ToList();

            UpdateDeniedRequestStatus(selectedRequest, requestContext, bookingDelaymentRequests);

            DenyFeedBack = "Request denied!";
            acceptContext.SelectedRequestTransfers.Remove(acceptContext.SelectedRequestTransfers.First());
            acceptContext.SaveChanges();
            ClearInput();
        }

        private static void UpdateDeniedRequestStatus(List<RequestDTO> selectedRequest, DataBaseContext requestContext, List<BookingDelaymentRequest> bookingDelaymentRequests)
        {
            foreach (BookingDelaymentRequest request in bookingDelaymentRequests.ToList())
            {
                if (request.bookingId == selectedRequest.First().bookingId)
                {
                    request.status = Status.Denied;
                    //requestContext.BookingDelaymentRequests.Remove(request);
                    requestContext.BookingDelaymentRequests.Update(request);
                    requestContext.SaveChanges();
                }
            }
        }

        private void ClearInput()
        {
            NewArrival = null;
            NewDeparture = null;
            OldArrival = null;
            OldDeparture = null;
            AccommodationName = null;
            Location = null;
            AccommodationType = null;
            GuestName = null;
            BookingId = -1;
        }
    }
}
