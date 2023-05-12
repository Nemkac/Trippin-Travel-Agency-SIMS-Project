using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Service.GuestServices
{
    class GuestRateService
    {
        private readonly IGuestRateRepository iGuestRateRepository;

        public GuestRateService(IGuestRateRepository iGuestRateRepository)
        {
            this.iGuestRateRepository = iGuestRateRepository;
        }

        public void Save(GuestRate guestRate)
        {
            iGuestRateRepository.Save(guestRate);
        }

        public bool IsRated(int bookingId)
        {
            return iGuestRateRepository.IsGuestRated(bookingId);
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

        public List<GuestRate> GetGuestRates()
        {
            return iGuestRateRepository.GetGuestsRates();
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

            if (numOfRates == 0)
            {
                totalRating = 0;
                return totalRating;
            }

            totalRating = Math.Round((decimal)ratesSum / numOfRates, 2);
            return totalRating;
        }

        public string GetDisplayableRate(GuestRate guestRate)
        {
            return guestRate.cleanness.ToString() + "\n\n" + guestRate.respectingRules + "\n\n" + guestRate.comment + "\n\n";
        }
    }
}
