using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    class BookingService
    {
        public BookingDTO CreateDTO(Booking booking)
        {
            UserService userService = new UserService();
            AccommodationService accommodationService = new AccommodationService();
            DataBaseContext dtoContext = new DataBaseContext();
            BookingDTO bookingDto = new BookingDTO();

            Accommodation tmpAccommodation = accommodationService.GetById(booking.accommodationId);
            User tmpUser = userService.GetById(booking.guestId);
            bookingDto = new BookingDTO(tmpUser.username, booking.Id, tmpAccommodation.name, booking.arrival, booking.departure, booking.stayingPeriod);
            return bookingDto;
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

<<<<<<< Updated upstream:Trippin Travel Agency/InitialProject/InitialProject/Services/BookingService.cs
=======
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
>>>>>>> Stashed changes:Trippin Travel Agency/InitialProject/InitialProject/Service/BookingService.cs
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
