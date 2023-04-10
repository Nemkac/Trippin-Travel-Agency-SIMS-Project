﻿using InitialProject.Context;
using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class BookingDelaymentRequestService
    {
        public BookingDelaymentRequest GetById(int bookingDelaymentRequstId)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.BookingDelaymentRequests.SingleOrDefault(b => b.id == bookingDelaymentRequstId);
        }

        public static void Save(BookingDelaymentRequest bookingDelaymentRequest)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(bookingDelaymentRequest);
            saveContext.SaveChanges();
        }

        public List<string> GetTextOutput(BookingDelaymentRequest bookingDelaymentRequest)
        {
            string outcome = string.Empty;
            List<string> output = new List<string>();
            BookingService bookingService = new BookingService();
            AccommodationService accommodationService = new AccommodationService();
            Booking booking = bookingService.GetById(bookingDelaymentRequest.bookingId);
            Accommodation accommodation = accommodationService.GetById(booking.accommodationId);
            if (bookingDelaymentRequest.status == Status.Accepted)
            {
                outcome = "accepted !";
            }
            else if (bookingDelaymentRequest.status == Status.Denied)
            {
                outcome = "denied";
            }
            output.Add(outcome);
            output.Add(booking.Id.ToString());
            output.Add(accommodation.name);
            output.Add(bookingDelaymentRequest.newArrival.ToString());
            output.Add(bookingDelaymentRequest.newDeparture.ToString());
            return output;
        }

        public void Delete(BookingDelaymentRequest bookingDelaymentRequest)
        {
            DataBaseContext context = new DataBaseContext();
            context.Remove(bookingDelaymentRequest);
            context.SaveChanges();
        }
    }
}
