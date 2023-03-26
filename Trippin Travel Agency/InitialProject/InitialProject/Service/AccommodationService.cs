using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace InitialProject.Service

{
    public class AccommodationService
    {
        public Accommodation GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            return accommodations.Find(a => a.id == id);
        }

        public List<string> GetAccommodationLocation(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<string> countryAndCity = new List<string>();
            List<AccommodationLocation> locations = context.LocationsOfAccommodations.ToList();

            Accommodation accommodation = accommodations.Find(a => a.id == id);
            countryAndCity.Add(accommodation.location.country);
            countryAndCity.Add(accommodation.location.city);
            return countryAndCity;
        }

        public List<int> GetAllByName(string name)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> filtered = new List<int>();
            foreach (Accommodation accommodation in accommodations.ToList())
            {
                string nameToUpper = accommodation.name.ToUpper();
                if (nameToUpper.Contains(name.ToUpper()))
                {
                    filtered.Add(accommodation.id);
                }
            }
            return filtered;
        }

        public List<int> GetAllByCountry(string country)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locations = context.LocationsOfAccommodations.ToList();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> filtered = new List<int>();
            foreach (Accommodation accommodation in accommodations.ToList())
            {

                if ((accommodation.location.country.ToUpper()).Contains(country.ToUpper()))
                {
                    filtered.Add(accommodation.id);
                }
            }
            return filtered;
        }

        public List<int> GetAllByCity(string city)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locations = context.LocationsOfAccommodations.ToList();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> filtered = new List<int>();
            foreach (Accommodation accommodation in accommodations.ToList())
            {
                if ((accommodation.location.city.ToUpper()).Contains(city.ToUpper()))
                {
                    filtered.Add(accommodation.id);
                }
            }
            return filtered;
        }

        public List<int> GetAllByGuestsNumber(int guestsNumber)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> filtered = new List<int>();
            foreach (Accommodation accommodation in accommodations.ToList())
            {
                if (accommodation.guestLimit >= guestsNumber)
                {
                    filtered.Add(accommodation.id);
                }
            }
            return filtered;
        }

        public List<int> GetAllByMininumDays(int days)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> filtered = new List<int>();
            foreach (Accommodation accommodation in accommodations.ToList())
            {
                if (accommodation.minDaysBooked <= days)
                {
                    filtered.Add(accommodation.id);
                }
            }
            return filtered;
        }

        public List<int> GetAllByType (string type)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            List<int> filtered = new List<int>();
            foreach (Accommodation accommodation in accommodations.ToList())
            {
                if ((accommodation.type.ToString().ToUpper()).Contains(type.ToUpper()))
                {
                    filtered.Add(accommodation.id);
                }
            }
            return filtered;
        }

        public List<List<DateTime>> GetAvailableDatePeriods(Accommodation accommodation, int daysToBook, List<DateTime> dateLimits)
        {
            DateTime startingDate;
            int startEndSpan;
            List<List<DateTime>> availablePeriods;
            List<Booking> sameAccommodationBookings;
            GetDateBasicProperties(accommodation, dateLimits, out startingDate, out startEndSpan, out availablePeriods, out sameAccommodationBookings);

            if (sameAccommodationBookings.Count == 0)
            {
                for (int i = 0; i <= startEndSpan - daysToBook; i++)
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                }
                return availablePeriods;
            }

            List<List<DateTime>> takenDates = new List<List<DateTime>>();
            foreach (Booking booking in sameAccommodationBookings)
            {
                takenDates.Add(new List<DateTime>() { DateTime.Parse(booking.arrival), DateTime.Parse(booking.departure) });
            }

            if (GetAvailableDates(startEndSpan, daysToBook, startingDate, takenDates).Count > 0)
            {
                return GetAvailableDates(startEndSpan, daysToBook, startingDate, takenDates);
            }

            if (SuggestAdditionalDates(startEndSpan, daysToBook, startingDate, takenDates).Count > 0)
            {
                return SuggestAdditionalDates(startEndSpan, daysToBook, startingDate, takenDates);
            }
            return null;
        }

        private void GetDateBasicProperties(Accommodation accommodation, List<DateTime> dateLimits, out DateTime startingDate, out int startEndSpan, out List<List<DateTime>> availablePeriods, out List<Booking> sameAccommodationBookings)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            startingDate = dateLimits[0];
            DateTime endingDate = dateLimits[1];
            startEndSpan = (endingDate.Subtract(startingDate)).Days;
            availablePeriods = new List<List<DateTime>>();
            sameAccommodationBookings = new List<Booking>();
            sameAccommodationBookings = GetAccommodationsBookings(bookings, accommodation);
        }

        public List<Booking> GetAccommodationsBookings(List<Booking> bookings, Accommodation accommodation) 
        {
            List<Booking> sameAccommodationBookings = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                if (booking.accommodationId == accommodation.id)
                {
                    sameAccommodationBookings.Add(booking);
                }
            }
            return sameAccommodationBookings;
        }

        public List<List<DateTime>> GetAvailableDates(int startEndSpan, int daysToBook, DateTime startingDate, List<List<DateTime>> takenDates)
        {
            List<List<DateTime>> availablePeriods = new List<List<DateTime>>();
            for (int i = 0; i <= startEndSpan - daysToBook; i++)
            {
                if (CheckIfPeriodAvailable(i,daysToBook,takenDates,startingDate))
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                }
            }
            return availablePeriods;
        } 

        public List<List<DateTime>> SuggestAdditionalDates(int startEndSpan, int daysToBook, DateTime startingDate, List<List<DateTime>> takenDates) 
        {
            int periodsFound = 0;
            List<List<DateTime>> availablePeriods = new List<List<DateTime>>();
            for (int i = 0; i < startEndSpan - daysToBook + 1095; i++)
            {

                if (CheckIfPeriodAvailable(i,daysToBook,takenDates,startingDate))
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                    periodsFound++;
                    if (periodsFound == 3) { break; }
                }
            }
            return availablePeriods;
        }

        public bool CheckIfPeriodAvailable(int dayIterator, int daysToBook, List<List<DateTime>> takenDates, DateTime startingDate)
        {
            for (int i = dayIterator; i < dayIterator + daysToBook; i++)
            {
                if(!CheckIfDayAvailable(takenDates, startingDate.AddDays(i)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfDayAvailable(List<List<DateTime>> takenDates,DateTime exactDay)
        {
            foreach (List<DateTime> bookingsDates in takenDates)
            {
                if (exactDay >= bookingsDates[0] && exactDay < bookingsDates[1])
                {
                    return false;
                }
            }
            return true;
        }

        public static AccommodationLocation GetLocation(string country, string city)
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<AccommodationLocation> locations = locationContext.LocationsOfAccommodations.ToList();

            foreach (AccommodationLocation location in locations.ToList())
            {
                if (location.country == country && location.city == city)
                {
                    return location;
                }
            }

            AccommodationLocation newLocation = new AccommodationLocation(country, city);
            return newLocation;
        }

        public static void Save(Accommodation accommodation)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(accommodation);
            saveContext.SaveChanges();
            MessageBox.Show("Accommodation registered succesfuly!");
        }

    }
}




