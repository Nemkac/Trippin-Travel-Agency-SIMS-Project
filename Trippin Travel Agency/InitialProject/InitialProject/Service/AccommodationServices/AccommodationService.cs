using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Service.AccommodationServices

{
    public class AccommodationService
    {
        private readonly IAccommodationRepository iAccommodationRepository;
        private AccommodationLocationService accommodationLocationService;

        public AccommodationService(IAccommodationRepository iAccommodationRepository)
        {
            this.iAccommodationRepository = iAccommodationRepository;
            this.accommodationLocationService = new AccommodationLocationService();
        }

        public List<Accommodation> ConvertDtoToInitial(List<AccommodationDTO> accommodationDTOs)
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach (AccommodationDTO dto in accommodationDTOs)
            {
                accommodations.Add(GetById(dto.accommodationId));
            }
            return accommodations;
        }

        public Accommodation GetById(int id)
        {
            return this.iAccommodationRepository.GetById(id);
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
            foreach (Accommodation accommodationToCheck in accommodationsToCheck)
            {
                foreach (int accommodation in allAccommodations)
                {
                    if (accommodationToCheck.id == accommodation)
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
            List<List<DateTime>> takenDates = new List<List<DateTime>>();
            GetDateBasicProperties(accommodation, dateLimits, out startingDate, out startEndSpan, out availablePeriods, out sameAccommodationBookings);

            if (sameAccommodationBookings.Count == 0 && GetRenovations(GuestOneStaticHelper.id) == null)
            {
                for (int i = 0; i <= startEndSpan - daysToBook; i++)
                {
                    availablePeriods.Add(new List<DateTime>() { startingDate.AddDays(i), startingDate.AddDays(i + daysToBook) });
                }
                return availablePeriods;
            }
            if(sameAccommodationBookings.Count == 0)
            {
                foreach (AccommodationRenovation accommodationRenovation in GetRenovations(GuestOneStaticHelper.id))
                {
                    takenDates.Add(new List<DateTime>() { DateTime.Parse(accommodationRenovation.startDate), DateTime.Parse(accommodationRenovation.endDate) });
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
            }

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
            startEndSpan = endingDate.Subtract(startingDate).Days;
            availablePeriods = new List<List<DateTime>>();
            sameAccommodationBookings = new List<Booking>();
            sameAccommodationBookings = GetAccommodationsBookings(bookings, accommodation);
        }

        public List<Booking> GetAccommodationsBookings(List<Booking> bookings, Accommodation accommodation)
        {
            return iAccommodationRepository.GetAccommodationsBookings(bookings, accommodation);
        }

        public List<List<DateTime>> GetAvailableDateSlots(int startEndSpan, int daysToBook, DateTime startingDate, List<List<DateTime>> takenDates)
        {
            List<List<DateTime>> dateSlots = new List<List<DateTime>>();
            for (int i = 0; i <= startEndSpan - daysToBook; i++)
            {
                if (CheckIfPeriodAvailable(i, daysToBook, takenDates, startingDate))
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

                if (CheckIfPeriodAvailable(i, daysToBook, takenDates, startingDate))
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
                if (!CheckIfDayAvailable(takenDates, startingDate.AddDays(i)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfDayAvailable(List<List<DateTime>> takenDates, DateTime exactDay)
        {
            foreach (List<DateTime> bookingsDates in takenDates)
            {
                if ((exactDay >= bookingsDates[0] && exactDay < bookingsDates[1]) || IsDayDuringRenovation(exactDay))
                {
                    return false;
                }
            }

            return true;
        }

        public AccommodationLocation GetLocation(string country, string city)
        {
            return this.iAccommodationRepository.GetNewLocation(country, city);
        }

        public void Save(Accommodation accommodation)
        {
            iAccommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetAccommodationsByOwnerId(int id)
        {
            return this.iAccommodationRepository.GetOwnersAccommodations(id);
        }

        public AccommodationStatisticsDTO CreateAccommodationStatisticsDTO(Accommodation accommodation)
        {
            List<string> location = this.GetAccommodationLocation(accommodation.id);
            AccommodationStatisticsDTO dto = new AccommodationStatisticsDTO(accommodation, location);
            return dto;
        }

        public List<AccommodationRenovation> GetAllRenovations()
        {
            return this.iAccommodationRepository.GetAllRenovations();
        }

        public bool IsDayDuringRenovation(DateTime exactDay)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationRenovation> accommodationRenovations = GetRenovations(GuestOneStaticHelper.id);
            if (accommodationRenovations != null)
            {
                foreach (AccommodationRenovation accommodationRenovation in accommodationRenovations)
                {
                    if (exactDay >= DateTime.Parse(accommodationRenovation.startDate) && exactDay <= DateTime.Parse(accommodationRenovation.endDate))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<AccommodationRenovation> GetRenovations(int accommodationId)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationRenovation> accommodationRenovations = context.AccommodationRenovations.ToList();
            List<AccommodationRenovation> foundRenovations = new List<AccommodationRenovation>();
            foreach (AccommodationRenovation accommodationRenovation in accommodationRenovations)
            {
                if(accommodationRenovation.accommodationId == accommodationId)
                {
                    foundRenovations.Add(accommodationRenovation);
                }
            }
            return foundRenovations;
        }

        public bool IfAccommodationRecentlyRenovated(int accommodationId)
        {
            if (GetRenovations(accommodationId) != null)
            {
                foreach (AccommodationRenovation accommodationRenovation in GetRenovations(accommodationId))
                {
                    if (DateTime.Today.Subtract(DateTime.Parse(accommodationRenovation.endDate)).Days < 364)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}




