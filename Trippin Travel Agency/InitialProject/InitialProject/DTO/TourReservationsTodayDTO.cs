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

        public TourReservationsTodayDTO(int id, bool guestJoined)
        {
            this.id = id;
            this.guestJoined = guestJoined;
        }

        public TourReservationsTodayDTO()
        {
        }
    }
}
