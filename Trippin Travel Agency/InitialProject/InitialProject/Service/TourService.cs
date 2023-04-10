using InitialProject.Context;
using InitialProject.Model;
using InitialProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InitialProject.Service
{
    class TourService
    {
        public TourService() { }
        public static TourLocation GetTourLocation(string country, string city)
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
        public static void Save(Tour tour)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(tour);
            saveContext.SaveChanges();
            MessageBox.Show("Tour registered succesfuly!");
        }
        public static bool CheckExistence(string name, DateTime date)
        {
            using (var context = new DataBaseContext())
            {
                var tour = context.Tours.FirstOrDefault(t => t.name == name && t.startDates == date);
                return tour != null;
            }

        }
        public TourLocation GetLocationById(int id)
        {
            using DataBaseContext dbContext = new DataBaseContext();
            return dbContext.TourLocation.SingleOrDefault(tl => tl.id == id);
        }
        public TourDTO CreateDTO(Tour tour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            string keyPoints = GetKeyPointNames(tour, dataBaseContext);
            TourLocation tmp = GetLocationById(tour.location);
            TourDTO tourDTO = new(tour.id, tour.name, tour.description, tmp.city, tmp.country, keyPoints, tour.language, tour.touristLimit, tour.startDates, tour.hoursDuration);
            return tourDTO;
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

        public List<KeyPoint> GetKeyPoints(int tourId, DataBaseContext dataBaseContext)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in dataBaseContext.KeyPoints.ToList()) {
                if (keyPoint.tourId == tourId) { 
                    keyPoints.Add(keyPoint);
                }
            }
            return keyPoints;
        }
        public List<TourDTO> GetByCity(string cityName)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                TourLocation tourLocation = GetLocationById(tour.location);
                if (tourLocation.city.ToUpper().Contains(cityName.ToUpper()))
                {
                    tourDTO = CreateDTO(tour);
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
                TourLocation tourLocation = GetLocationById(tour.location);
                if (tourLocation.country.ToUpper().Contains(countryName.ToUpper()))
                {
                    tourDTO = CreateDTO(tour);
                    tourDTOs.Add(tourDTO);
                }
            }
            return tourDTOs;
        }
        public List<TourDTO> GetByLanguage(language tourLanguage)
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
        public List<TourDTO> GetByDuration(string duration)
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
        public List<TourDTO> GetByTouristLimit(string limit)
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
                DateTime today = DateTime.Today;
                List<Tour> toursToday = context.Tours
                    .Where(t => t.startDates.Date == today)
                    .ToList();

                return toursToday;
            }
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
        public List<TourDTO> GetBookableTours(string cityName, string nameOfFullTour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();
            //TourDTO tourDTO = new TourDTO();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.name != nameOfFullTour && tour.touristLimit > 0)
                {
                    TourLocation tourLocation = GetLocationById(tour.location);
                    if (tourLocation.city.ToUpper().Contains(cityName.ToUpper()))
                    {
                        //tourDTO = CreateDTO(tour);
                        tourDTOs.Add(CreateDTO(tour));
                    }
                }
            }
            return tourDTOs;
        }
        public int Book(int tourId, int numberOfTourists)
        {
            using DataBaseContext dataBase = new DataBaseContext();
            Tour tour = dataBase.Tours.SingleOrDefault(t => t.id == tourId);

            if (tour == null) return -2; // Error return value
            if (tour.touristLimit == 0) return -1; // Tour filled
            if (tour.touristLimit < numberOfTourists) return 1; // Too many guests for selected tour
            return 0; // Tour registered
        }
        public Tour GetById(int id)
        {
            using DataBaseContext dbContext = new DataBaseContext();
            return dbContext.Tours.SingleOrDefault(t => t.id == id);
        }
        public ToursTodayDTO createToursTodayDTO (Tour tour)
        {
            ToursTodayDTO toursTodayDTO = new ToursTodayDTO(tour.id, tour.name, tour.language); 
            return toursTodayDTO;
        }
        public TourReservationsTodayDTO createTourReservationsTodayDTO(TourReservation tr)
        {
            TourReservationsTodayDTO tourReservationsTodayDTO = new TourReservationsTodayDTO(tr.id);
            return tourReservationsTodayDTO;
        }
        public List<TourReservation> GetTourReservationsById(int tourId)
        {
            List<TourReservation> reservations = new List<TourReservation>();
            DataBaseContext dataBaseContext = new DataBaseContext();
            foreach(TourReservation tr in dataBaseContext.TourReservations.ToList())
            {
                if(tr.tourId == tourId)
                {
                    reservations.Add(tr);
                }
            }
            return reservations;
        }


        public Tour GetByID(int tourId) { 
            
            using DataBaseContext dataBase = new DataBaseContext();

            foreach (Tour tour in dataBase.Tours.ToList()) { 
                if(tour.id == tourId) {
                    return tour;
                }            
            }

            return null;
        }

        public Tour GetActiveTour(DataBaseContext context) {
            Tour tour = new Tour();
            List<Tour> bookedTours = new List<Tour>();
            foreach (TourReservation reservation in context.TourReservations.ToList())
            {
                if (reservation.guestId == LoggedUser.id)
                {
                    bookedTours.Add(GetByID(reservation.tourId));
                }
            }
            foreach (Tour t in bookedTours)
            {
                if (t.active)
                {
                    tour = t;
                    break;
                }
            }
            return tour;
        }
    }
}
