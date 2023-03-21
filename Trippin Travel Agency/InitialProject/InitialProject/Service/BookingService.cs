﻿using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
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
            List<Booking> bookingList = guestIdContext.Bookings.ToList();

            foreach(Booking booking in bookingList.ToList()) 
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
            List<Booking> bookingList = bookingContext.Bookings.ToList();
            User user = new User();

            foreach(Booking booking  in bookingList.ToList())
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

        public static List<string> GetDisplayableDates(List<List<DateTime>> availableDates)
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

