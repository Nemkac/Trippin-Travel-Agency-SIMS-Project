using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class AccommodationRateService
    {
        public static void Save(AccommodationRate accommodationRate)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(accommodationRate);
            saveContext.SaveChanges();
        }

        public double GetAccommodationAverageRate(int accommodationId)
        {
            DataBaseContext context = new DataBaseContext();
            BookingService bookingService = new BookingService();
            List<AccommodationRate> rates = context.AccommodationRates.ToList();
            double averageRate = 0;
            int ratesCounter = 0;
            foreach(AccommodationRate rate in rates)
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
            foreach(AccommodationRate accommodationRate in accommodationRates)
            {
                if(accommodationRate.bookingId == bookingId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
