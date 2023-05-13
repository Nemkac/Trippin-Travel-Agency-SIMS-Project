using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        public AccommodationRepository() { }
        public Accommodation GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> accommodations = context.Accommodations.ToList();
            return accommodations.Find(a => a.id == id);
        }
        public List<int> GetAllByName(string name)
        {
            if (name != null)
            {
                DataBaseContext context = new DataBaseContext();
                GuestOneInterface guestOneInterface = new GuestOneInterface();
                List<int> filtered = new List<int>();
                List<Accommodation> accommodations = context.Accommodations.ToList();
                foreach (Accommodation accommodation in accommodations)
                {
                    string nameToUpper = accommodation.name.ToUpper();
                    if (nameToUpper.Contains(name.ToUpper()))
                    {
                        filtered.Add(accommodation.id);
                    }
                }
                return filtered;
            }
            return null;
        }

        public List<int> GetAllByCity(string city)
        {
            if (city != null)
            {
                DataBaseContext context = new DataBaseContext();
                List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
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
            return null;
        }

        public List<int> GetAllByGuestsNumber(int guestsNumber)
        {
            if (guestsNumber != null)
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
            return null;
        }

        public List<int> GetAllByMininumDays(int days)
        {
            if (days != null)
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
            return null;
        }

        public List<int> GetAllByType(string type)
        {
            if (type != null)
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
            return null;
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

        public void Save(Accommodation accommodation)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(accommodation);
            saveContext.SaveChanges();
        }

        public List<Accommodation> GetOwnersAccommodations(int id)
        {
            DataBaseContext accommodationContext = new DataBaseContext();
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach (Accommodation accommodation in accommodationContext.Accommodations.ToList())
            {
                if(accommodation.ownerId == id)
                {
                    accommodations.Add(accommodation);
                }
            }

            return accommodations;
        }

        public AccommodationLocation GetNewLocation(string country, string city)
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

        public List<AccommodationRenovation> GetAllRenovations()
        {
            DataBaseContext renovationsContext = new DataBaseContext();
            List<AccommodationRenovation> renovations = renovationsContext.AccommodationRenovations.ToList();
            return renovations;
        }
    }
}
