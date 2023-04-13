using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Service

{
    public class AccommodationService
    {
        private readonly IAccommodationRepository iAccommodationRepository;

        public AccommodationService(IAccommodationRepository iAccommodationRepository)
        {
            this.iAccommodationRepository = iAccommodationRepository;
        }

        public List<Accommodation> ConvertDtoToInitial(List<AccommodationDTO> accommodationDTOs)
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach(AccommodationDTO dto in accommodationDTOs)
            {
                accommodations.Add(GetById(dto.id));
            }
            return accommodations;
        }

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
            List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
            Accommodation accommodation = accommodations.Find(a => a.id == id);
            return new List<string>() { accommodation.location.country, accommodation.location.city };
        }

        public List<int> GetAllByCountry(string country)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
            List<int> filtered = new List<int>();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            foreach (Accommodation accommodation in accommodations)
            {
                if (GetAccommodationLocation(accommodation.id)[0].ToUpper().Contains(country.ToUpper()))
                {
                    filtered.Add(accommodation.id);
                }
            }
            GuestOneInterface guestOneInterface = new GuestOneInterface();
            return filtered;
        }

        public List<Accommodation> GetMatching(List<int> allAccommodations, List<Accommodation> accommodationsToCheck)
        {
            List<Accommodation> matchingAccommodations = new List<Accommodation>();
            DataBaseContext context = new DataBaseContext();
            foreach(Accommodation accommodationToCheck in accommodationsToCheck)
            {
                foreach(int accommodation in allAccommodations)
                {
                    if(accommodationToCheck.id == accommodation)
                    {
                        matchingAccommodations.Add(GetById(accommodationToCheck.id));
                    }
                }
            }
            return matchingAccommodations;
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

            if (GetAvailableDateSlots(startEndSpan, daysToBook, startingDate, takenDates).Count > 0)
            {
                return GetAvailableDateSlots(startEndSpan, daysToBook, startingDate, takenDates);
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

        public List<List<DateTime>> GetAvailableDateSlots(int startEndSpan, int daysToBook, DateTime startingDate, List<List<DateTime>> takenDates)
        {
            List<List<DateTime>> dateSlots = new List<List<DateTime>>();
            for (int i = 0; i <= startEndSpan - daysToBook; i++)
            {
                if (CheckIfPeriodAvailable(i,daysToBook,takenDates,startingDate))
                {
                    dateSlots.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                }
            }
            return dateSlots;
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
            List<AccommodationLocation> locations = locationContext.AccommodationLocation.ToList();

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




