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

            // Tour name
            tour.name = "Novi Sad spring tour";
            // Tour location
            TourLocation location = new TourLocation("Beograd", "Serbia");

            tour.location = location;
            // Tour key poits
            ICollection<KeyPoint> keyPoints = new List<KeyPoint>
            {
                new KeyPoint("Petrovaradin", false)
            };
            tour.keyPoints = keyPoints.ToList();
            // Tour description
            tour.description = "Description";
            // Tour language
            tour.language = language.English;
            // Tour tourist limit
            tour.touristLimit = 5;
            // Tour start dates
            tour.startDates = "02-02-2022";
            // Tour hours duration
            tour.hoursDuration = 3;

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

        public TourDTO createDTO(Tour tour)
        {
            DataBaseContext dataBaseContext = new DataBaseContext();

            List<string> selectedKeyPoints = new List<string>(); // needed for dtos


            foreach (KeyPoint keyPoint in dataBaseContext.KeyPoints.ToList()) // za svaki kp iz svih kpova 
            {
                if (keyPoint.Tourid == tour.id)// ako je id ture od kpa isti kao neki od ture iz lista svih tura
                {
                    selectedKeyPoints.Add(keyPoint.name); // u listu selektovanih kpa dodajemo ime kpa
                }
            }

            TourLocation tmp = GetTourLocation(tour.id);
            TourDTO tourDTO = new(tour.id, tour.name, tour.description,tmp.city, tmp.country, selectedKeyPoints, tour.language, tour.touristLimit, tour.startDates, tour.hoursDuration);
            return tourDTO;
        }
    }
}
