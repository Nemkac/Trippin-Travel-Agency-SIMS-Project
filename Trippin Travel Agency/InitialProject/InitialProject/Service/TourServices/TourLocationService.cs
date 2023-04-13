using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
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
    }
}
