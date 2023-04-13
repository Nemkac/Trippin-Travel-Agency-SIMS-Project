using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    public interface ITourRepository
    {
        public Tour GetById(int id);
        public void Save(Tour tour);
        public List<KeyPoint> GetTourKeyPoints(int tourId, DataBaseContext dataBaseContext);
        public string GetKeyPointNames(Tour tour, DataBaseContext dataBaseContext);
        public TourDTO CreateTourDTO(Tour tour);
        public List<TourDTO> GetAllByCity(string cityName);
        public List<TourDTO> GetAllByCountry(string countryName);
        public List<TourDTO> GetAllByLanguage(language tourLanguage);
        public List<TourDTO> GetAllByDuration(string duration);
        public List<TourDTO> GetAllByTouristLimit(string limit);
        public List<Tour> GetAllToursToday();
        public List<Tour> GetAllFutureTours();
        public List<Tour> GetAllFinishedTours();
        public List<TourDTO> GetPreviouslySelected(int id);
    }
}
