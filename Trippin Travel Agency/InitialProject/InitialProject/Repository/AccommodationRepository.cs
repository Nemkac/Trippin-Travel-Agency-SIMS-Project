using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        public AccommodationRepository() { }

        public List<int> GetAllByName(string name)
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

        public List<int> GetAllByCity(string city)
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

        public List<int> GetAllByType(string type)
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
    }
}
