using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.TourServices
{
    public class TourLocationService
    {
        private readonly ITourLocationRepository iTourLocationRepository;
        public TourLocationService(ITourLocationRepository iTourLocationRepository)
        {
            this.iTourLocationRepository = iTourLocationRepository;
        }

        public TourLocation GetById(int id)
        {
            return iTourLocationRepository.GetById(id);
        }

        public TourLocation GetTourLocationByCountryAndCity(string country, string city)
        {
            return iTourLocationRepository.GetTourLocationByCountryAndCity(country, city);
        }

        public List<TourLocation> GetAllTourLocations()
        {
            return iTourLocationRepository.GetAllTourLocations();
        }
        public List<string> GetAllCities()
        {
            return iTourLocationRepository.GetAllCities();
        }
        public List<string> GetAllCountries()
        {
            return iTourLocationRepository.GetAllCountries();
        }
    }
}
