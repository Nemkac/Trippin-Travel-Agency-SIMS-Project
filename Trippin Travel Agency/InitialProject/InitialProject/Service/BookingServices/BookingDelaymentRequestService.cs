﻿using InitialProject.Context;
using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.BookingServices
{
    internal class BookingDelaymentRequestService
    {
        private readonly IBookingDelaymentRequestRepository iBookingDelaymentRequestRepository;
        private AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private BookingService bookingService;

        public BookingDelaymentRequestService(IBookingDelaymentRequestRepository iBookingDelaymentRequestRepository)
        {
            this.iBookingDelaymentRequestRepository = iBookingDelaymentRequestRepository;
            accommodationRepository = new AccommodationRepository();
            accommodationService = new AccommodationService(accommodationRepository);
            BookingRepository bookingRepository = new BookingRepository();
            bookingService = new BookingService(bookingRepository);

        }

        public BookingDelaymentRequest GetById(int bookingDelaymentRequstId)
        {
            return iBookingDelaymentRequestRepository.GetById(bookingDelaymentRequstId);
        }

        public void Save(BookingDelaymentRequest bookingDelaymentRequest)
        {
            iBookingDelaymentRequestRepository.Save(bookingDelaymentRequest);
        }

        public List<string> GetTextOutput(BookingDelaymentRequest bookingDelaymentRequest)
        {
            string outcome = string.Empty;
            List<string> output = new List<string>();
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
            iBookingDelaymentRequestRepository.Delete(bookingDelaymentRequest);
        }
    }
}
