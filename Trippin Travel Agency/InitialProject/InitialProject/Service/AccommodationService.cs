using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        // Potrebno refaktorisati
        public List<string> GetLocationList(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<string> countryAndCity = new List<string>();
            List<AccommodationLocation> locationsData = context.LocationsOfAccommodations.ToList();

            foreach (Accommodation accommodation in dataList)
            {
                if (accommodation.id == id)
                {
                    countryAndCity.Add(accommodation.location.country);
                    countryAndCity.Add(accommodation.location.city);
                }
            }
            return countryAndCity;
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

        public List<List<DateTime>> GetAvailableDates(Accommodation accommodation, int daysToBook, List<DateTime> dateLimits)
        {
            // Get date basic properties
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
            DateTime exactDay;
            int availablePeriod;
            List<List<DateTime>> availablePeriods = new List<List<DateTime>>();
            for (int i = 0; i <= startEndSpan - daysToBook; i++)
            {
                availablePeriod = 0;
                for (int j = i; j < i + daysToBook; j++)
                {
                    exactDay = startingDate.AddDays(j);

                    foreach (List<DateTime> bookingsDates in takenDates)
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
                }
            }
            return availablePeriods;
        }

        // Treba refaktorisati
        public List<List<DateTime>> SuggestAdditionalDates(int startEndSpan, int daysToBook, DateTime startingDate, List<List<DateTime>> takenDates) 
        {
            int periodsFound = 0;
            DateTime exactDay;
            int availablePeriod;
            List<List<DateTime>> availablePeriods = new List<List<DateTime>>();
            for (int i = 0; i < startEndSpan - daysToBook + 1095; i++)
            {
                availablePeriod = 0;
                for (int j = i; j < i + daysToBook; j++)
                {
                    exactDay = startingDate.AddDays(j);
                    foreach (List<DateTime> bookingsDates in takenDates)
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
                    periodsFound++;
                    if (periodsFound == 3)
                    {
                        break;
                    }
                }
            }
            return availablePeriods;
        }

        public static AccommodationLocation GetLocation(string country, string city)
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<AccommodationLocation> locationsList = locationContext.LocationsOfAccommodations.ToList();

            foreach (AccommodationLocation location in locationsList.ToList())
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




