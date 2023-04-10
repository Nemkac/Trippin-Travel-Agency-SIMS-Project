using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourReservation
    {
        public int id { get; set; }

        public int guestId { get; set; }

        public int tourId { get; set; }

        public int guestNumber { get; set; }
       
        public bool guestJoined { get; set; }
        
        public bool guideConfirmed { get; set; }

        public bool guestJoined { get; set; }

        public bool guideConfirmed { get; set; }

        public TourReservation(int guestId, int tourId, int guestNumber)
        {
            this.guestId = guestId;
            this.tourId = tourId;
            this.guestNumber = guestNumber;
            this.guestJoined = false;  
            this.guideConfirmed = false;
        }
        public TourReservation() { }    
    }
}
