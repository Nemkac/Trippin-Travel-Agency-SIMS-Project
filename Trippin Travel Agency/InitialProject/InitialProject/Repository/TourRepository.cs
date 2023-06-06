using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Service.TourServices;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    public class TourRepository : ITourRepository
    {
        private TourLocationService tourLocationService;
        public TourRepository() {
            this.tourLocationService = new(new TourLocationRepository());
        }
        public Tour GetById(int id)
        {
            using (var db = new DataBaseContext())
            {
                foreach (Tour tour in db.Tours) 
                {
                    if (tour.id == id) { 
                    
                    return tour;
                    
                    }
                }
                return null;    
            }
        }

        public TourRequest GetRequestById(int id)
        {
            using (var db = new DataBaseContext())
            {
                foreach (TourRequest tr in db.TourRequests)
                {
                    if (tr.id == id)
                    {

                        return tr;

                    }
                }
                return null;
            }
        }

        public void Save(Tour tour)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(tour);
            saveContext.SaveChanges();
            //MessageBox.Show("Tour registered succesfuly!");
        }

        public List<KeyPoint> GetTourKeyPoints(int tourId, DataBaseContext dataBaseContext)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in dataBaseContext.KeyPoints.ToList())
            {
                if (keyPoint.tourId == tourId)
                {
                    keyPoints.Add(keyPoint);
                }
            }
            return keyPoints;
        }
        public string GetKeyPointNames(Tour tour, DataBaseContext dataBaseContext)
        {
            string keyPoints = "";
            foreach (KeyPoint keyPoint in dataBaseContext.KeyPoints.ToList())
            {
                if (keyPoint.tourId == tour.id)
                {
                    keyPoints += keyPoint.name + ", \n";
                }

            }
            keyPoints = keyPoints.Remove(keyPoints.Length - 3);
            return keyPoints;
        }

        public TourDTO CreateTourDTO(Tour tour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            string keyPoints = GetKeyPointNames(tour, dataBaseContext);
            TourLocation tmp = this.tourLocationService.GetById(tour.location);
            TourDTO tourDTO = new(tour.id, tour.name, tour.description, tmp.city, tmp.country, keyPoints, tour.language, tour.touristLimit, tour.startDates, tour.hoursDuration);
            return tourDTO;
        }

        public List<TourDTO> GetAllByCity(string cityName)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                TourLocation tourLocation = this.tourLocationService.GetById(tour.location);
                if (tourLocation.city.ToUpper().Contains(cityName.ToUpper()))
                {
                    tourDTO = CreateTourDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }

        public List<TourDTO> GetAllByCountry(string countryName)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                TourLocation tourLocation = this.tourLocationService.GetById(tour.location);
                if (tourLocation.country.ToUpper().Contains(countryName.ToUpper()))
                {
                    tourDTO = CreateTourDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }

        public List<TourDTO> GetAllByLanguage(language tourLanguage)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.language == tourLanguage)
                {
                    tourDTO = CreateTourDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }

        public List<TourDTO> GetAllByDuration(string duration)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            int number;
            if (int.TryParse(duration, out number))
            {
                foreach (Tour tour in dataBaseContext.Tours.ToList())
                {
                    if (tour.hoursDuration == Int32.Parse(duration))
                    {
                        tourDTO = CreateTourDTO(tour);
                        tourDTOs.Add(tourDTO);
                    }
                }
                return tourDTOs;
            }
            return tourDTOs;
        }

        public List<TourDTO> GetAllByTouristLimit(string limit)
        {             
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            int number;
            if (int.TryParse(limit, out number)) 
            {
                foreach (Tour tour in dataBaseContext.Tours.ToList())
                {
                    if (tour.touristLimit == Int32.Parse(limit))
                    {
                        tourDTO = CreateTourDTO(tour);
                        tourDTOs.Add(tourDTO);
                    }
                }
                return tourDTOs;
            }
            return tourDTOs;
        }

        public List<Tour> GetAllToursToday()
        {
            DataBaseContext context = new DataBaseContext();
            using (context)
            {
                DateTime today = DateTime.Today;
                List<Tour> toursToday = context.Tours
                    .Where(t => t.startDates.Date == today)
                    .ToList();

                return toursToday;
            }
        }
        public List<Tour> GetAllFutureTours()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                List<Tour> futureTours = context.Tours
                    .Where(t => t.startDates.Date >= tomorrow)
                    .ToList();

                return futureTours;
            }
        }
        public List<Tour> GetAllFinishedTours()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                List<Tour> finishedTours = context.Tours
                    .Where(t => t.finished == true)
                    .ToList();

                return finishedTours;
            }
        }

        public List<TourDTO> GetPreviouslySelected(int id)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.id == id)
                {
                    tourDTO = CreateTourDTO(tour);
                    tourDTOs.Add(tourDTO);
                    break;
                }
            }
            return tourDTOs;
        }
    }
}
