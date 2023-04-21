using InitialProject.Context;
using InitialProject.Model;
using InitialProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InitialProject.Interfaces;
using InitialProject.Repository;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InitialProject.Service.TourServices
{
    class TourService
    {
        private readonly ITourRepository iTourRepository;
        private TourLocationService tourLocationService;
        private TourReservationService tourReservationService;
        public TourService(ITourRepository iTourRepository)
        {
            this.iTourRepository = iTourRepository;
            tourLocationService = new(new TourLocationRepository());
            tourReservationService = new(new TourReservationRepository());
        }
        public TourLocation GetTourLocation(string country, string city)
        {
            return tourLocationService.GetTourLocationByCountryAndCity(country, city);
        }
        public void Save(Tour tour)
        {
            iTourRepository.Save(tour);
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
            return tourLocationService.GetById(id);
        }
        public TourDTO CreateDTO(Tour tour)
        {
            return iTourRepository.CreateTourDTO(tour);
        }

        public string GetKeyPointNames(Tour tour, DataBaseContext dataBaseContext)
        {
            return iTourRepository.GetKeyPointNames(tour, dataBaseContext);
        }

        public List<KeyPoint> GetKeyPoints(int tourId, DataBaseContext dataBaseContext)
        {
            return iTourRepository.GetTourKeyPoints(tourId, dataBaseContext);
        }
        public List<TourDTO> GetAllByCity(string cityName)
        {
            return iTourRepository.GetAllByCity(cityName);
        }
        public List<TourDTO> GetAllByCountry(string countryName)
        {
            return iTourRepository.GetAllByCountry(countryName);
        }
        public List<TourDTO> GetAllByLanguage(language tourLanguage)
        {
            return iTourRepository.GetAllByLanguage(tourLanguage);
        }
        public List<TourDTO> GetAllByDuration(string duration)
        {
            return iTourRepository.GetAllByDuration(duration);
        }
        public List<TourDTO> GetAllByTouristLimit(string limit)
        {
            return iTourRepository.GetAllByTouristLimit(limit);
        }
        public List<Tour> GetAllToursToday()
        {
            return iTourRepository.GetAllToursToday();
        }
        public List<Tour> GetAllFutureTours()
        {
            return iTourRepository.GetAllFutureTours();
        }
        public List<Tour> GetAllFinishedTours()
        {
            return iTourRepository.GetAllFinishedTours();
        }
        public bool IsTourFinished(List<KeyPoint> keyPoints)
        {
            return keyPoints != null && keyPoints.All(kp => kp.visited);
        }
        public List<TourDTO> GetPreviouslySelected(int id)
        {
            return this.iTourRepository.GetPreviouslySelected(id);
        }
        public List<TourDTO> GetBookableTours(string cityName, string nameOfFullTour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();
            List<TourDTO> tourDTOs = new List<TourDTO>();

            foreach (Tour tour in dataBaseContext.Tours.ToList())
            {
                if (tour.name != nameOfFullTour && tour.touristLimit > 0)
                {
                    TourLocation tourLocation = GetLocationById(tour.location);
                    if (tourLocation.city.ToUpper().Contains(cityName.ToUpper()))
                    {
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
            return iTourRepository.GetById(id);
        }
        public ToursTodayDTO createToursTodayDTO(Tour tour)
        {
            ToursTodayDTO toursTodayDTO = new ToursTodayDTO(tour.id, tour.name, tour.language);
            return toursTodayDTO;
        }
        public FutureToursDTO createFutureToursDTO(Tour tour)
        {
            FutureToursDTO futureToursDTO = new FutureToursDTO(tour.id, tour.name, tour.language);
            return futureToursDTO;
        }
        public FinishedTourDTO createFinishedToursDTO(Tour tour)
        {
            FinishedTourDTO finishedTourDTO = new FinishedTourDTO(tour.id, tour.name, tour.language);
            return finishedTourDTO;
        }
        public TourReservationsTodayDTO createTourReservationsTodayDTO(TourReservation tr)
        {
            TourReservationsTodayDTO tourReservationsTodayDTO = new TourReservationsTodayDTO(tr.id);
            return tourReservationsTodayDTO;
        }
        public List<TourReservation> GetTourReservationsById(int tourId)
        {
            return tourReservationService.GetAllById(tourId);
        }

        public List<TourAndGuideRate> GetTourRatingsById(int tourId)
        {
            List<TourAndGuideRate> reviews = new List<TourAndGuideRate>();
            DataBaseContext dataBaseContext = new DataBaseContext();
            foreach (TourAndGuideRate tagr in dataBaseContext.TourAndGuideRates.ToList())
            {
                if (tagr.tourId == tourId)
                {
                    reviews.Add(tagr);
                }
            }
            return reviews;
        }


        public Tour GetByID(int tourId)
        {

            return iTourRepository.GetById(tourId);
        }

        public Tour GetActiveTour(DataBaseContext context)
        {
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

        public KeyPoint GetNextUnvisitedKeyPoint(List<KeyPoint> keyPointsList)
        {
            return keyPointsList.FirstOrDefault(kp => kp.visited == false);
        }

        public List<TourAttendance> GetTourAttendances (int tourId)
        {
            List<TourAttendance> attendances = new List<TourAttendance>();
            DataBaseContext dataBaseContext = new DataBaseContext();
            foreach (TourAttendance ta in dataBaseContext.TourAttendances.ToList())
            {
                if (ta.tourId == tourId)
                {
                    attendances.Add(ta);
                }
            }
            return attendances;
        }


    }
}
