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
            
            tour.startDates = "02-05-2022";
            
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
    }
}
