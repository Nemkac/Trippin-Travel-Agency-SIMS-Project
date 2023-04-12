using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private DataBaseContext context;

        public AccommodationRepository(DataBaseContext context)
        {
            this.context = context;
        }

        public List<AccommodationLocation> GetAllLocations()
        {
            List<AccommodationLocation> countryList = context.AccommodationLocation.ToList();
            return countryList;
        }

    }
}
