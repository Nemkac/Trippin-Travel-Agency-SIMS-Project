using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.DTO;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Service
{
    class TourService
    {
        public TourService() { }
        public static TourLocation findLocation(string country, string city)
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<TourLocation> locationsList = locationContext.TourLocation.ToList();

            foreach (TourLocation location in locationsList.ToList())
            {
                if (location.country == country && location.city == city)
                {
                    return location;
                }
            }

            TourLocation newLocation = new TourLocation(country, city);
            return newLocation;
        }
        public static void Save(Tour tour)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(tour);
            saveContext.SaveChanges();
            MessageBox.Show("Tour registered succesfuly!");
        }
        public static bool CheckTourExists(string name, DateTime date)
        {
            using (var context = new DataBaseContext())
            {
                var tour = context.Tours.FirstOrDefault(t => t.name == name && t.startDates == date);
                return tour != null;
            }

        }

        public List<Tour> createTour()
        {
            DataBaseContext context = new DataBaseContext();

            Tour tour = new Tour();

            tour.name = "Washington DC";
            TourLocation loc = new TourLocation("Washington", "USA");

            tour.location = loc.id;
           
            ICollection<KeyPoint> keyPoints = new List<KeyPoint>
            {
                new KeyPoint("White house", false),
                new KeyPoint("Stadion",false)
            };
            tour.keyPoints = keyPoints.ToList();

            tour.description = "Visit presidental house";

            tour.language = language.English;

            tour.touristLimit = 15;

            tour.startDates = new DateTime(2022, 2, 2, 05, 00, 0);

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

            List<TourLocation> locations = dbContext.TourLocation.ToList();

            foreach (TourLocation location in locations)
            {
                if (location.id == id)
                {
                    requiredTour = location;
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
                {                                                               // Ovaj deo mozda refaktorisati? 
                    keyPoints += keyPoint.name + '\n';
                }
            }
            TourLocation tmp = GetTourLocation(tour.location);
            TourDTO tourDTO = new(tour.id, tour.name, tour.description, tmp.city, tmp.country, keyPoints, tour.language, tour.touristLimit, tour.startDates, tour.hoursDuration);
            return tourDTO;
        }
        public List<TourDTO> GetByInputCityName(string cityName)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                TourLocation tourLocation = GetTourLocation(tour.location);
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
                TourLocation tourLocation = GetTourLocation(tour.location);
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
                if (tour.hoursDuration == Int32.Parse(duration))
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
                if (tour.touristLimit == Int32.Parse(limit))
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }
        public List<Tour> GetToursToday()
        {
            DataBaseContext context = new DataBaseContext();
            using (context)
            {
                // Get tours with startDates equal to today's date
                DateTime today = DateTime.Today;
                List<Tour> toursToday = context.Tours
                    .Where(t => t.startDates.Date == today)
                    .ToList();

                return toursToday;
            }
        }

        public Tour GetTourByButton(object sender)
        {
            Button tourButton = sender as Button;
            if (tourButton != null)
            {
                Tour selectedTour = tourButton.DataContext as Tour;
                if (selectedTour != null)
                {
                    return selectedTour;
                }
            }
            return null;
        }
        public bool IsTourFinished(List<KeyPoint> keyPoints)
        {
            return keyPoints != null && keyPoints.All(kp => kp.visited);
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
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                    break;
                }
            }
            return tourDTOs;
        }
        public List<TourDTO> GetNonFullTours(string cityName, string nameOfFullTour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.name != nameOfFullTour)
                {
                    TourLocation tourLocation = GetTourLocation(tour.location);
                    if (tourLocation.city.ToUpper().Contains(cityName.ToUpper()))
                    {
                        tourDTO = CreateDTO(tour);
                        tourDTOs.Add(tourDTO);
                    }
                }
            }
            return tourDTOs;
        }

        public int ReserveTour(int tourIndex, int numberOfTourists)
        {
            DataBaseContext dataBase = new DataBaseContext();
            List<Tour> allTours = dataBase.Tours.ToList();

            foreach (Tour tour in allTours)
            {
                if (tour.id == tourIndex)
                {
                    if (tour.touristLimit - numberOfTourists >= 0)
                    {
                        tour.touristLimit -= numberOfTourists;
                        TourReservation tourReservation = new TourReservation(LoggedUser.id, tourIndex, numberOfTourists);
                        dataBase.Attach(tourReservation);
                        dataBase.SaveChanges();
                        return 0; // Tour registered

                    }else if (tour.touristLimit > 0)
                    {
                        return 1; // Too many guests for selected tour
                    }
                    if(tour.touristLimit == 0)
                    {
                        return -1; // Tour filled
                    }
                }
            }
            return -2; // Error return value
        }
    }
}
