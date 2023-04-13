﻿using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.AccommodationServices
{
    internal class AccommodationRateService
    {
        private readonly BookingService bookingService = new(new BookingRepository());
        private readonly IAccommodationRateRepository iAccommodationRateRepository;
        private AccommodationService accommodationService;

        public AccommodationRateService(IAccommodationRateRepository iAccommodationRateRepository)
        {
            this.iAccommodationRateRepository = iAccommodationRateRepository;
            BookingRepository bookingRepository = new BookingRepository();
            bookingService = new BookingService(bookingRepository);
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            accommodationService = new AccommodationService(accommodationRepository);
        }

        public void Save(AccommodationRate accommodationRate)
        {
            iAccommodationRateRepository.Save(accommodationRate);
        }

        public double GetAccommodationAverageRate(int accommodationId)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationRate> rates = context.AccommodationRates.ToList();
            double averageRate = 0;
            int ratesCounter = 0;
            foreach (AccommodationRate rate in rates)
            {
                if (bookingService.GetById(rate.bookingId) != null && bookingService.GetById(rate.bookingId).accommodationId == accommodationId)
                {
                    averageRate += rate.cleanness + rate.ownerRate;
                    ratesCounter++;
                }
            }
            return averageRate / (ratesCounter * 2);
        }

        public bool isPreviouslyRated(int bookingId)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationRate> accommodationRates = context.AccommodationRates.ToList();
            foreach (AccommodationRate accommodationRate in accommodationRates)
            {
                if (accommodationRate.bookingId == bookingId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
