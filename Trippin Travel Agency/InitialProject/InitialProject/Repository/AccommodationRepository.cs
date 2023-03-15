using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Context;
using InitialProject.Model;

namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        public List<Accommodation> GetAll()
        {
            List<Accommodation> foundAccommodations = new List<Accommodation>();
            using (var db = new DataBaseContext())
            {
                foreach (Accommodation accommodation in db.Accommodations)
                {
                    foundAccommodations.Add(accommodation);
                }
            }
            return foundAccommodations;
        }

        public Accommodation GetById(int id)
        {
            using (var db = new DataBaseContext())
            {
                foreach(Accommodation accommodation in db.Accommodations)
                {
                    if (accommodation.id == id)
                    {
                        return accommodation;
                    }
                }
                return null;
            }
        }
    }

}
