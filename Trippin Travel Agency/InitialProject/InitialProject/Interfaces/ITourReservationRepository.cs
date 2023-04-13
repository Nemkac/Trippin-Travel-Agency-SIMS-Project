using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    public interface ITourReservationRepository
    {
        public TourReservation GetById(int id);
        public List<TourReservation> GetAllById(int tourId);
    }
}
