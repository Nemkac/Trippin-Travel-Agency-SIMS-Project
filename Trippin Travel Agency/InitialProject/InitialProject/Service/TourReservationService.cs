using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class TourReservationService
    {
        public TourReservation GetById(int id)
        {
            using DataBaseContext dbContext = new DataBaseContext();
            return dbContext.TourReservations.SingleOrDefault(t => t.id == id);
        }

        public TourReservationsTodayDTO transformTourReservationToDTO(TourReservation tourReservation) 
        {
            TourReservationsTodayDTO tourReservationsTodayDTO = new TourReservationsTodayDTO();
            tourReservationsTodayDTO.id= tourReservation.id;
            tourReservationsTodayDTO.guestJoined= tourReservation.guestJoined;
            tourReservationsTodayDTO.guideConfirmed= tourReservation.guideConfirmed;
            
            return tourReservationsTodayDTO;
        }
    }
}
