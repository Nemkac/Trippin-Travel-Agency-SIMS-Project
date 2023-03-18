using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace InitialProject.Service

{
    public class AccommodationService
    {
        public Accommodation GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.id == id)
                {
                    return accommodation;
                }
            }
            return null;
        }

        public List<int> GetByName(string name)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                string nameToUpper = accommodation.name.ToUpper();
                if (nameToUpper.Contains(name.ToUpper()))
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByCountry(string country)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locationsData = context.LocationsOfAccommodations.ToList();
            List<Accommodation> accommodationsData = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in accommodationsData.ToList())
            {

                if ((accommodation.location.country.ToUpper()).Contains(country.ToUpper()))
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByCity(string city)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locationsData = context.LocationsOfAccommodations.ToList();
            List<Accommodation> accommodationsData = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in accommodationsData.ToList())
            {
                if ((accommodation.location.city.ToUpper()).Contains(city.ToUpper()))
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByGuestsNumber(int guestsNumber)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.guestLimit >= guestsNumber)
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByMininumDays(int days)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.minDaysBooked <= days)
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByType (string type)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if ((accommodation.type.ToString().ToUpper()).Contains(type.ToUpper()))
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<List<DateTime>> GetAvailableDates(Accommodation accommodation, int days, List<DateTime> dateLimits)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            DateTime startingDate = dateLimits[0];
            DateTime endingDate = dateLimits[1];
            int startEndSpan = (endingDate.Subtract(startingDate)).Days;
            int daysToBook = days;
            // nadji sve bookinge u tom smestaju
            List<Booking> sameAccommodationBookings = new List<Booking>();
            List<List<DateTime>> availablePeriods = new List<List<DateTime>>();
            List<DateTime> checkInCheckOut = new List<DateTime>();
            foreach (Booking booking in bookings)
            {
                if (booking.accommodationId == accommodation.id)
                {
                    sameAccommodationBookings.Add(booking);
                }
            }
            // ako nije nadjen nijedan booking, ponudi sve moguce datume koji upadaju izmedju dva zadata
            if (sameAccommodationBookings.Count == 0)
            {
                for (int i = 0; i <= startEndSpan - daysToBook; i++)
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                }
                return availablePeriods;
            }

            // izvuci sve zauzete datume tog smestaja u neku listu
            List<DateTime> arrivalAndDepartueOfFoundBooking = new List<DateTime>();
            List<List<DateTime>> allDates = new List<List<DateTime>>();
            foreach (Booking booking in sameAccommodationBookings)
            {
                allDates.Add(new List<DateTime>() { DateTime.Parse(booking.arrival), DateTime.Parse(booking.departure) });
            }

            // nadji slobodan datum 
            DateTime exactDay;
            int availablePeriod;
            bool availablePeriodFound = false;
            for (int i = 0; i <= startEndSpan - daysToBook; i++)
            {
                availablePeriod = 0;
                for (int j = i; j < i + daysToBook; j++)
                {
                    exactDay = startingDate.AddDays(j);

                    foreach (List<DateTime> bookingsDates in allDates)
                    {
                        if (exactDay >= bookingsDates[0] && exactDay < bookingsDates[1])
                        {
                            availablePeriod++;
                        }
                    }
                }
                if (availablePeriod == 0)
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                    availablePeriodFound = true;
                }
            }
            if (availablePeriodFound)
            {
                return availablePeriods;
            }

            // ako je availablePeriodFound ostao na false onda se predlazu neki datumi van onih zadatih
            int periodsFound = 0;

            for (int i = 0; i < startEndSpan - daysToBook + 1095; i++)
            {
                availablePeriod = 0;
                for (int j = i; j < i + daysToBook; j++)
                {
                    exactDay = startingDate.AddDays(j);
                    foreach (List<DateTime> bookingsDates in allDates)
                    {
                        if (exactDay >= bookingsDates[0] && exactDay < bookingsDates[1])
                        {
                            availablePeriod++;
                        }
                    }
                }
                if (availablePeriod == 0)
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                    availablePeriodFound = true;
                    periodsFound++;
                    if (periodsFound == 3)
                    {
                        break;
                    }
                }
            }

            if (!availablePeriodFound)
            {
                // napisi da nema slobodnih datuma u naredne 3 godine
                return availablePeriods;
            }
            return availablePeriods;
        }
    }
}




