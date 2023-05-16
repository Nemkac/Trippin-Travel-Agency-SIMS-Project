using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    public interface ITourLocationRepository
    {
        public TourLocation GetById(int id);
        public TourLocation GetTourLocationByCountryAndCity(string country, string city);
        public List<TourLocation> GetAllTourLocations();
        public List<string> GetAllCities();
        public List<string> GetAllCountries();
    }
}
