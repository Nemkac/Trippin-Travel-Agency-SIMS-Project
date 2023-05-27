using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourAttendance
    {
        public int id {  get; set; }

        public int tourId { get; set; }

        public int keyPointId { get; set; }

        public int guestID { get; set; }
        public int numberOfGuests { get; set; }
        public bool checkedForCoupon { get; set; }  

        public TourAttendance(int tourId, int keyPointId, int guestID, int numberOfGuests)
        {
            this.tourId = tourId;
            this.keyPointId = keyPointId;
            this.guestID = guestID;
            this.numberOfGuests = numberOfGuests;
            this.checkedForCoupon = false;
        }

        public TourAttendance() { } 
    }
}
