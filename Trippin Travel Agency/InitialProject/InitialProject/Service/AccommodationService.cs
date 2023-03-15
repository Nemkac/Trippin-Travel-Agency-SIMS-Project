using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace InitialProject.Service

{
    public class AccommodationService
    {
        public List<Accommodation> createAccommodations()
        {
            DataBaseContext context = new DataBaseContext();
            Accommodation accommodation1 = new Accommodation();
            Accommodation accommodation2 = new Accommodation();
            accommodation1.name = "Menza";
            AccommodationLocation location1 = new AccommodationLocation("Serbia", "Novi Sad");
            accommodation1.location = location1.GetLocation();
            accommodation1.minDaysBooked = 7;
            accommodation1.bookingCancelPeriodDays = 5;
            accommodation1.type = Model.Type.Hut;
            accommodation1.guestLimit = 5;

            accommodation2.name = "Morava";
            AccommodationLocation location2 = new AccommodationLocation("Serbia", "Cacak");
            accommodation2.location = location2.GetLocation();
            accommodation2.type = Model.Type.Apartment;
            accommodation2.guestLimit = 5; ;

            context.Attach(accommodation1);
            context.Attach(accommodation2);
            List<Accommodation> dataList = context.Accommodations.ToList();
            dataList.Add(accommodation1);
            dataList.Add(accommodation2);
            context.SaveChanges();
            return dataList;

        }

        public Accommodation GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.id == id)
                {
                    return accommodation;
                }
            }
            return null;
        }

        /*public int GetByName(string name)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                string nameToUpper = accommodation.name.ToUpper();
                if(nameToUpper.Contains(name.ToUpper()))
                {
                    return accommodation.id;
                }
            }
            return 0;
        }*/

        public List<int> GetByName(string name)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                string nameToUpper = accommodation.name.ToUpper();
                if (nameToUpper.Contains(name.ToUpper()))
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByType(int type)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if ((int)accommodation.type == type)
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByCountry(string country)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.location.Country == country)
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByCity(string city)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.location.City == city)
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<int> GetByGuestsNumber(int guestsNumber)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<int> filteredList = new List<int>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                if (accommodation.guestLimit >= guestsNumber)
                {
                    filteredList.Add(accommodation.id);
                }
            }
            return filteredList;
        }

        public List<Accommodation> GetAvailableDays(int daysNumber)
        {
            DataBaseContext context = new DataBaseContext();
            List<Accommodation> dataList = context.Accommodations.ToList();
            List<Accommodation> filteredList = new List<Accommodation>();
            foreach (Accommodation accommodation in dataList.ToList())
            {
                // ovo ce biti jebeno
            }
            return filteredList;
        }
    }
}

   
    

