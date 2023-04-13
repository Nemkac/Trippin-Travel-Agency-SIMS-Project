using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationRateRepository : IAccommodationRateRepository
    {
        public AccommodationRateRepository() { }

        public void Save(AccommodationRate accommodationRate)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(accommodationRate);
            saveContext.SaveChanges();
        }
    }
}
