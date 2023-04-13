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
    }
}
