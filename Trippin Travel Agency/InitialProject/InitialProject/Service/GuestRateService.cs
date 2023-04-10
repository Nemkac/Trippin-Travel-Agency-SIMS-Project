using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Service
{
    class GuestRateService
    {
        public static void Save(GuestRate guestRate)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(guestRate);
            saveContext.SaveChanges();
        }

        public bool IsRated(int bookingId)
        {
            DataBaseContext ratedBookingContext = new DataBaseContext();
            List<GuestRate> ratedGuests = ratedBookingContext.GuestRate.ToList();
            foreach (GuestRate guestRate in ratedGuests.ToList())
            {
                if (guestRate.bookingId == bookingId)
                {
                    return true;
                }
            }

            return false;
        }

        //Ovo ide u AccommodationRateService
        public void FormDisplayableRates(List<AccommodationRate> accommodationRates, List<GuestRate> guestRates, List<AccommodationRate> ratesForDisplay)
        {
            foreach (GuestRate guestRate in guestRates.ToList())
            {
                foreach (AccommodationRate accommodationRate in accommodationRates.ToList())
                {
                    if (accommodationRate.bookingId == guestRate.bookingId)
                    {
                        ratesForDisplay.Add(accommodationRate);
                    }
                }
            }
        }

        public decimal CalculateTotalRating(List<AccommodationRate> availableRates)
        {
            int ratesSum = 0;
            int numOfRates = availableRates.Count;
            decimal totalRating;
            foreach (AccommodationRate item in availableRates)
            {
                ratesSum = ratesSum + item.cleanness + item.ownerRate;
            }

            if(numOfRates == 0)
            {
                totalRating = 0;
                return totalRating;
            }

            totalRating = Math.Round((decimal)ratesSum / numOfRates, 2);
            return totalRating;
        }
    }
}
