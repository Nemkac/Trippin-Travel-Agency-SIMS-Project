using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.AccommodationServices
{
    class AccommodationLocationService
    {
        public AccommodationLocation GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
            foreach (AccommodationLocation location in locations.ToList())
            {
                if (location.id == id)
                {
                    return location;
                }
            }
            return null;
        }
    }
}