using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourLocationRepository : ITourLocationRepository
    {
        public TourLocationRepository() { }

        public TourLocation GetById(int id)
        {
            using DataBaseContext db = new DataBaseContext();
            return db.TourLocation.SingleOrDefault(tl => tl.id == id);
        }

        public TourLocation GetTourLocationByCountryAndCity(string country, string city)
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<TourLocation> locations = locationContext.TourLocation.ToList();

            foreach (TourLocation location in locations.ToList())
            {
                if (location.country == country && location.city == city)
                {
                    return location;
                }
            }

            TourLocation newLocation = new TourLocation(country, city);
            return newLocation;
        }

        public List<TourLocation> GetAllTourLocations() {
            DataBaseContext locationContext = new DataBaseContext();
            List<TourLocation> locations = locationContext.TourLocation.ToList();
            return locations;
        }
        public List<string> GetAllCities()
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<TourLocation> locations = locationContext.TourLocation.ToList();
            List<string> cities = new List<string>();
            foreach(TourLocation location in locations) 
            {
                if (!cities.Contains(location.city)) 
                {
                    cities.Add(location.city);
                }
            }
            return cities;
        }
        public List<string> GetAllCountries()
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<TourLocation> locations = locationContext.TourLocation.ToList();
            List<string> countries = new List<string>();
            foreach (TourLocation location in locations)
            {
                if (!countries.Contains(location.country))
                {
                    countries.Add(location.country);
                }
            }
            return countries;
        }
    }
}
