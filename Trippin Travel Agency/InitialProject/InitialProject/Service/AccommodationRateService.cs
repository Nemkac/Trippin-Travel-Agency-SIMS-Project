using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class AccommodationRateService
    {
        public static void Save(AccommodationRate accommodationRate)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(accommodationRate);
            saveContext.SaveChanges();
        }
    }
}
