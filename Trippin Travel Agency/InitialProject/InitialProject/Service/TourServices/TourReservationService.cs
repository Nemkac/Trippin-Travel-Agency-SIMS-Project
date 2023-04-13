using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Interfaces;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.TourServices
{
    class TourReservationService
    {
        private readonly ITourReservationRepository iTourReservationRepository;

        public TourReservationService(ITourReservationRepository iTourReservationRepository)
        {
            this.iTourReservationRepository = iTourReservationRepository;
        }

        public TourReservation GetById(int id)
        {
            return iTourReservationRepository.GetById(id);
        }

        public TourReservationsTodayDTO transformTourReservationToDTO(TourReservation tourReservation)
        {
            TourReservationsTodayDTO tourReservationsTodayDTO = new TourReservationsTodayDTO();
            tourReservationsTodayDTO.id = tourReservation.id;
            tourReservationsTodayDTO.guestJoined = tourReservation.guestJoined;
            tourReservationsTodayDTO.guideConfirmed = tourReservation.guideConfirmed;

            return tourReservationsTodayDTO;
        }

        public List<TourReservation> GetAllById(int tourId)
        {
            return iTourReservationRepository.GetAllById(tourId);
        }
    }
}
