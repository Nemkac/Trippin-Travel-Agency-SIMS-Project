using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourReservationsTodayDTO
    {
        public int id { get; set; }
        public bool guestJoined { get; set; }
        public bool guideConfirmed { get; set; }

        public TourReservationsTodayDTO(int id)
        {
            this.id = id;
            this.guestJoined = false;
            this.guideConfirmed = false;
        }

        public TourReservationsTodayDTO()
        {
        }
    }
}
