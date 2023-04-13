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
    public class TourReservationRepository : ITourReservationRepository
    {
        public TourReservationRepository() { }
        public TourReservation GetById(int id)
        {
            using DataBaseContext dbContext = new DataBaseContext();
            return dbContext.TourReservations.SingleOrDefault(t => t.id == id);
        }
        public List<TourReservation> GetAllById(int tourId)
        {
            List<TourReservation> reservations = new List<TourReservation>();
            DataBaseContext dataBaseContext = new DataBaseContext();
            foreach (TourReservation tr in dataBaseContext.TourReservations.ToList())
            {
                if (tr.tourId == tourId)
                {
                    reservations.Add(tr);
                }
            }
            return reservations;
        }
    }
}
