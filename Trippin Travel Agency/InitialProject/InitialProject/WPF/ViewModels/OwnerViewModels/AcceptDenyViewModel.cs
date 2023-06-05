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




        private string _accommodationNameText;
        public string AccommodationNameText
        {
            get { return _accommodationNameText; }
            set
            {
                if (_accommodationNameText != value)
                {
                    _accommodationNameText = value;
                    OnPropertyChanged(nameof(AccommodationNameText));
                }
            }
        }

        private string locationText;
        public string LocationText
        {
            get { return locationText; }
            set
            {
                if (locationText != value)
                {
                    locationText = value;
                    OnPropertyChanged(nameof(LocationText));
                }
            }
        }

        private string _accommodationTypeText;
        public string AccommodationTypeText
        {
            get { return _accommodationTypeText; }
            set
            {
                if (_accommodationTypeText != value)
                {
                    _accommodationTypeText = value;
                    OnPropertyChanged(nameof(AccommodationTypeText));
                }
            }
        }
        private string _bookingIdText;
        public string BookingIdText
        {
            get { return _bookingIdText; }
            set
            {
                if (_bookingIdText != value)
                {
                    _bookingIdText = value;
                    OnPropertyChanged(nameof(BookingIdText));
                }
            }
        }
        private string _guestText;
        public string GuestText
        {
            get { return _guestText; }
            set
            {
                if (_guestText != value)
                {
                    _guestText = value;
                    OnPropertyChanged(nameof(GuestText));
                }
            }
        }

        private string _oldArrivalText;
        public string OldArrivalText
        {
            get { return _oldArrivalText; }
            set
            {
                if (_oldArrivalText != value)
                {
                    _oldArrivalText = value;
                    OnPropertyChanged(nameof(OldArrivalText));
                }
            }
        }

        private string _oldDepartureText;
        public string OldDepartureText
        {
            get { return _oldDepartureText; }
            set
            {
                if (_oldDepartureText != value)
                {
                    _oldDepartureText = value;
                    OnPropertyChanged(nameof(OldDepartureText));
                }
            }
        }

        private string _newArrivalText;
        public string NewArrivalText
        {
            get { return _newArrivalText; }
            set
            {
                if (_newArrivalText != value)
                {
                    _newArrivalText = value;
                    OnPropertyChanged(nameof(NewArrivalText));
                }
            }
        }
        private string _newDepartureText;
        public string NewDepartureText
        {
            get { return _newDepartureText; }
            set
            {
                if (_newDepartureText != value)
                {
                    _newDepartureText = value;
                    OnPropertyChanged(nameof(NewDepartureText));
                }
            }
        }
        private string _denyText;
        public string DenyText
        {
            get { return _denyText; }
            set
            {
                if (_denyText != value)
                {
                    _denyText = value;
                    OnPropertyChanged(nameof(DenyText));
                }
            }
        }
        private string _acceptText;
        public string AcceptText
        {
            get { return _acceptText; }
            set
            {
                if (_acceptText != value)
                {
                    _acceptText = value;
                    OnPropertyChanged(nameof(AcceptText));
                }
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
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";

            AccommodationNameText = Mediator.GetCurrentIsLanguageChecked() ? "Naziv Smestaja:" : "Accommodation Name:";
            LocationText = Mediator.GetCurrentIsLanguageChecked() ? "Lokacija:" : "Location:";
            AccommodationTypeText = Mediator.GetCurrentIsLanguageChecked() ? "Tip Smestaja:" : "Accommodation Type:";
            BookingIdText = Mediator.GetCurrentIsLanguageChecked() ? "ID Rezervacije:" : "Booking ID:";
            GuestText = Mediator.GetCurrentIsLanguageChecked() ? "Korisnik:" : "Guest:";
            OldArrivalText = Mediator.GetCurrentIsLanguageChecked() ? "Prethodni Dolazak:" : "Old Arrival:";
            OldDepartureText = Mediator.GetCurrentIsLanguageChecked() ? "Prethodni Odlazak:" : "Old Departure:";
            NewArrivalText = Mediator.GetCurrentIsLanguageChecked() ? "Novi Dolazak:" : "New Arrival:";
            NewDepartureText = Mediator.GetCurrentIsLanguageChecked() ? "Novi Odlazak:" : "New Departure:";
            AcceptText = Mediator.GetCurrentIsLanguageChecked() ? "Prihvati" : "Accept";
            DenyText = Mediator.GetCurrentIsLanguageChecked() ? "Zatvori postojeci smestaj" : "Close existing accommodation";
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            AccommodationNameText = isChecked ? "Naziv Smestaja:" : "Accommodation Name:";
            LocationText = isChecked ? "Lokacija:" : "Location:";
            AccommodationTypeText = isChecked ? "Tip Smestaja:" : "Accommodation Type:";
            BookingIdText = isChecked ? "ID Rezervacije:" : "Booking ID:";
            GuestText = isChecked ? "Korisnik:" : "Guest:";
            OldArrivalText = isChecked ? "Prethodni Dolazak:" : "Old Arrival:";
            OldDepartureText = isChecked ? "Prethodni Odlazak:" : "Old Departure:";
            NewArrivalText = isChecked ? "Novi Dolazak:" : "New Arrival:";
            NewDepartureText = isChecked ? "NAJNEPOPULARNIJA LOKACIJA" : "LEAST POPULAR LOCATION";
            AcceptText = isChecked ? "Otvori novi smestaj" : "Open new accommodation";
            DenyText = isChecked ? "Odbij" : "Deny";
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
            bool checkValuesExistance = IsPropertyNull();
            if (checkValuesExistance)
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
            else
            {
                MessageBox.Show("Required data is not available!");
            }

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
            bool checkValuesExistance = IsPropertyNull();
            if (checkValuesExistance)
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
            else
            {
                MessageBox.Show("Required data is not available");
            }
        }

        private static void UpdateDeniedRequestStatus(List<RequestDTO> selectedRequest, DataBaseContext requestContext, List<BookingDelaymentRequest> bookingDelaymentRequests)
        {
            foreach (BookingDelaymentRequest request in bookingDelaymentRequests.ToList())
            {
                if (request.bookingId == selectedRequest.First().bookingId)
                {
                    request.status = Status.Denied;
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

        private bool IsPropertyNull()
        {
            if (NewArrival == null || NewDeparture == null || OldArrival == null || OldDeparture == null || 
                AccommodationName == null || Location == null || AccommodationType == null || GuestName == null || BookingId == -1) return false;
            return true;
        }
    }
}
