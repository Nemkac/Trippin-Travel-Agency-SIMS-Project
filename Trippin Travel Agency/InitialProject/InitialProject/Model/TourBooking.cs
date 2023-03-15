using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourBooking
    {
        public int id { get; set; }

        public int tourId { get; set; }

        public int touristNumber { get; set; }

        public TourBooking(int id, int tourId, int touristNumber)
        {
            this.id = id;
            this.tourId = tourId;
            this.touristNumber = touristNumber;
        }
        public TourBooking() { }    


    }
}
