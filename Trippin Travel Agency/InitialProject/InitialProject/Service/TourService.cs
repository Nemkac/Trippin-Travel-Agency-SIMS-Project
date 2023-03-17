using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourService
    {
        public List<Tour> createTour()
        {
            DataBaseContext context = new DataBaseContext();

            Tour tour = new Tour();

            tour.name = "Novi Sad GHETTO";
            
            TourLocation location = new TourLocation("Novi Sad", "Serbia");

            tour.location = location;
           
            ICollection<KeyPoint> keyPoints = new List<KeyPoint>
            {
                new KeyPoint("Detelinara", false),
                new KeyPoint("Novo Naselje",false)
            };
            tour.keyPoints = keyPoints.ToList();
            
            tour.description = "Visit Magic Forest";
            
            tour.language = language.Serbian;
            
            tour.touristLimit = 15;

            tour.startDates = new DateTime(2022,2,2,05,00,0);
            
            tour.hoursDuration = 24;

            context.Attach(tour);
            List<Tour> dataList = context.Tours.ToList();
            dataList.Add(tour);
            context.SaveChanges();

            return dataList;
        }

        public TourLocation GetTourLocation(int id)
        {
            DataBaseContext dbContext = new DataBaseContext();
            TourLocation requiredTour = new TourLocation();

            List<TourLocation> tours = dbContext.TourLocations.ToList();

            foreach (TourLocation tour in tours)
            {
                if (tour.id == id)
                {
                    requiredTour = tour;
                }
            }
            return requiredTour;
        }

        public TourDTO CreateDTO(Tour tour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();

            string keyPoints = "";

            foreach (KeyPoint keyPoint in dataBaseContext.KeyPoints.ToList()) 
            {
                if (keyPoint.tourId == tour.id)
                {
                    keyPoints += keyPoint.name + '\n'; 
                }
            }

            TourLocation tmp = GetTourLocation(tour.id);
            TourDTO tourDTO = new(tour.id, tour.name, tour.description,tmp.city, tmp.country, keyPoints, tour.language, tour.touristLimit, tour.startDates, tour.hoursDuration);
            return tourDTO;
        }

        public List<TourDTO> GetByInputCityName(string cityName)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                TourLocation tourLocation = GetTourLocation(tour.id);
                if (tourLocation.city.ToUpper().Contains(cityName.ToUpper()))
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }
        public List<TourDTO> GetByInputCountryName(string countryName)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                TourLocation tourLocation = GetTourLocation(tour.id);
                if (tourLocation.country.ToUpper().Contains(countryName.ToUpper()))
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }

        public List<TourDTO> GetByInputLanguage(language tourLanguage)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.language == tourLanguage)
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }
        public List<TourDTO> GetByInputTourDuration(string duration)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.hoursDuration == int.Parse(duration))
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }

        public List<TourDTO> GetByInputTouristLimit(string limit)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.hoursDuration == int.Parse(limit))
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }

    }
}
