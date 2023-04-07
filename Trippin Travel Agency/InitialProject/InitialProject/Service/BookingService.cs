using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InitialProject.Service
{
    class BookingService
    {
        public Booking GetById(int bookingId)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Bookings.SingleOrDefault(b => b.Id == bookingId);
        }

        public BookingDTO CreateBookingDTO(Booking booking)
        {
            UserService userService = new UserService();
            AccommodationService accommodationService = new AccommodationService();
            DataBaseContext dtoContext = new DataBaseContext();
            BookingDTO bookingDto = new BookingDTO();

            Accommodation tmpAccommodation = accommodationService.GetById(booking.accommodationId);
            User tmpUser = userService.GetById(booking.guestId);
            bookingDto = new BookingDTO(tmpUser.username, booking.Id, tmpAccommodation.name, booking.arrival, booking.departure, booking.daysToStay);
            return bookingDto;
        }

        public void Delete(Booking booking)
        {
            DataBaseContext context = new DataBaseContext();
            context.Remove(booking);
            context.SaveChanges();
        }

        /*public Booking GetById(int bookingId)
        {
            DataBaseContext guestIdContext = new DataBaseContext();
            List<Booking> bookings = guestIdContext.Bookings.ToList();

            foreach (Booking booking in bookings.ToList())
            {
                if (booking.Id == bookingId)
                {
                    return booking;
                }
            }
            return null;
        }*/
        
        public RequestDTO CreateRequestDTO(BookingDelaymentRequest bookingDelaymentRequest)
        {
            UserService userService = new UserService();
            DataBaseContext dtoContext = new DataBaseContext();
            RequestDTO requestDto = new RequestDTO();

            Booking tmpBooking = GetById(bookingDelaymentRequest.bookingId);
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
            return (tmpArrival <= newArrival && tmpDeparture >= newArrival) ||
                                (newDeparture >= tmpArrival && newDeparture <= tmpDeparture) ||
                                (newArrival <= tmpArrival && newDeparture >= tmpDeparture);
        }

        public int GetGuestId(int bookingId)
        {
            int guestId;
            DataBaseContext guestIdContext = new DataBaseContext();
            List<Booking> bookings = guestIdContext.Bookings.ToList();

            foreach(Booking booking in bookings.ToList()) 
            {
                if(booking.Id ==  bookingId)
                {
                    guestId = booking.guestId;
                    return guestId;
                }
            }
            return -1;
        }

        public string GetGuestName(int bookingId)
        {
            UserService userService = new UserService();
            DataBaseContext bookingContext = new DataBaseContext();
            List<Booking> bookings = bookingContext.Bookings.ToList();
            User user = new User();

            foreach(Booking booking  in bookings.ToList())
            {
                if(booking.Id == bookingId)
                {
                    user = userService.GetById(booking.guestId);
                }
            }

            return user.username;
        }

        public static void Save(Booking booking)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(booking);
            saveContext.SaveChanges();
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
    }
}


