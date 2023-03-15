using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.View;

namespace InitialProject.Services
{
    class AccommodationService
    {
        public static void Save(Accommodation accommodation)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(accommodation);
            saveContext.SaveChanges();
            MessageBox.Show("Accommodation registered succesfuly!");
        }

        public static AccommodationLocation findLocation(string country, string city)
        {
            DataBaseContext locationContext = new DataBaseContext();
            List<AccommodationLocation> locationsList = locationContext.LocationsOfAccommodations.ToList();

            foreach(AccommodationLocation location in locationsList.ToList()) { 
                if(location.Country == country && location.City == city) {
                    return location;
                }
            }

            AccommodationLocation newLocation = new AccommodationLocation(country, city);
            return newLocation;
        }
    }
}
