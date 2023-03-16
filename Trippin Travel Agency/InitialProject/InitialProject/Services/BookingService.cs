﻿using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bookingDto = new BookingDTO(tmpUser.Username, booking.Id, tmpAccommodation.name, booking.arrival, booking.departure, booking.stayingPeriod);
            return bookingDto;
        }
    }
}