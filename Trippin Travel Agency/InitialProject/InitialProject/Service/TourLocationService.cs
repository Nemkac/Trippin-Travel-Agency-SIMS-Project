using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
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
            return this.iTourLocationRepository.GetById(id);
        }

        public TourLocation GetTourLocationByCountryAndCity(string country, string city)
        {
            return this.iTourLocationRepository.GetTourLocationByCountryAndCity(country, city);
        }
    }
}
