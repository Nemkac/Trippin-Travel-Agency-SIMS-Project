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

                if ((accommodation.location.Country.ToUpper()).Contains(country.ToUpper()))
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
                if ((accommodation.location.City.ToUpper()).Contains(city.ToUpper()))
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

        public List<Accommodation> GetAvailableDays(int daysNumber)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<Accommodation> filteredList = new List<Accommodation>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                // ovo ce biti jebeno
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
    }
}




