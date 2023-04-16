using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InitialProject.Service.BookingServices
{
    class BookingService
    {
        private readonly IBookingRepository iBookingRepository;
        private AccommodationService accommodationService;
        private IAccommodationRepository accommodationRepository;

        public BookingService(IBookingRepository iBookingRepository)
        {
            this.iBookingRepository = iBookingRepository;
            accommodationRepository = new AccommodationRepository();
            accommodationService = new AccommodationService(accommodationRepository);

        }

        public List<CanceledBooking> GetAllCanceledBookings()
        {
            return this.iBookingRepository.GetAllCanceledBookings();
        }
        public List<Booking> GetAllBookings()
        {
            return this.iBookingRepository.GetAll();
        }

        public Booking GetById(int bookingId)
        {
            return this.iBookingRepository.GetById(bookingId);
        }

        public BookingDTO CreateBookingDTO(Booking booking)
        {
            UserService userService = new UserService();
            DataBaseContext dtoContext = new DataBaseContext();
            BookingDTO bookingDto = new BookingDTO();

            Accommodation tmpAccommodation = accommodationService.GetById(booking.accommodationId);
            User tmpUser = userService.GetById(booking.guestId);
            bookingDto = new BookingDTO(tmpUser.username, booking.Id, tmpAccommodation.name, booking.arrival, booking.departure, booking.daysToStay);
            return bookingDto;
        }

        public void Delete(Booking booking)
        {
            this.iBookingRepository.Delete(booking);
        }

        public RequestDTO CreateRequestDTO(BookingDelaymentRequest bookingDelaymentRequest)
        {
            UserService userService = new UserService();
            DataBaseContext dtoContext = new DataBaseContext();
            RequestDTO requestDto = new RequestDTO();

            Booking tmpBooking = iBookingRepository.GetById(bookingDelaymentRequest.bookingId);
            User tmpUser = userService.GetById(tmpBooking.guestId);

            DateTime oldArrival = DateTime.ParseExact(tmpBooking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime oldDeparture = DateTime.ParseExact(tmpBooking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);
            string isPossible = "No";
            if (IsDelaymentPossible(bookingDelaymentRequest.newArrival, bookingDelaymentRequest.newDeparture, tmpBooking.accommodationId)) isPossible = "Yes";
            requestDto = new RequestDTO(tmpUser.username, tmpBooking.Id, tmpBooking.accommodationId, oldArrival, oldDeparture, bookingDelaymentRequest.newArrival, bookingDelaymentRequest.newDeparture, isPossible);

            return requestDto;
        }

        private bool IsDelaymentPossible(DateTime newArrival, DateTime newDeparture, int accommodationId)
        {
            DataBaseContext datesContext = new DataBaseContext();
            List<Booking> bookingDates = datesContext.Bookings.ToList();

            foreach (var booking in bookingDates.Where(x => x.accommodationId == accommodationId))
            {
                var tmpArrival = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                var tmpDeparture = DateTime.ParseExact(booking.departure, "M/d/yyyy", CultureInfo.InvariantCulture);

                if (CheckDaysOverlaping(newArrival, newDeparture, tmpArrival, tmpDeparture))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckDaysOverlaping(DateTime newArrival, DateTime newDeparture, DateTime tmpArrival, DateTime tmpDeparture)
        {
            return tmpArrival <= newArrival && tmpDeparture >= newArrival ||
                                newDeparture >= tmpArrival && newDeparture <= tmpDeparture ||
                                newArrival <= tmpArrival && newDeparture >= tmpDeparture;
        }

        public int GetGuestId(int bookingId)
        {
            return this.iBookingRepository.GetGuestId(bookingId);
        }

        public string GetGuestName(int bookingId)
        {
            return this.iBookingRepository.GetGuestName(bookingId);
        }

        public void Save(Booking booking)
        {
            this.iBookingRepository.Save(booking);
        }

        public static List<string> FormDisplayableDates(List<List<DateTime>> availableDates)
        {
            List<string> displayableDates = new List<string>();
            foreach (List<DateTime> checkInCheckOut in availableDates)
            {
                string arrivalPart = checkInCheckOut[0].Date.ToShortDateString() + "  -  ";
                string departurePart = checkInCheckOut[1].Date.ToShortDateString();
                string date = arrivalPart + departurePart;
                displayableDates.Add(date);
            }
            return displayableDates;
        }

        public bool CheckIfValidForRating(Booking booking)
        {
            return !(DateTime.Today.Subtract(DateTime.Parse(booking.departure)).Days > 5);
        }
    }
}


