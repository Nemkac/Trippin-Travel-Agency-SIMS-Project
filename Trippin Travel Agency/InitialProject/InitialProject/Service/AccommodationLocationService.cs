using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class AccommodationLocationService
    {

        public AccommodationLocation GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> dataList = context.LocationsOfAccommodations.ToList();
            foreach (AccommodationLocation location in dataList.ToList())
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