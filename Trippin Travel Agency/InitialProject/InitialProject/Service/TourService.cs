using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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


    }
}
