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

        public Tour tour { get; set; }

        public int touristNumber { get; set; }

        public TourBooking(int id, Tour tour, int touristNumber)
        {
            this.id = id;
            this.tour = tour;
            this.touristNumber = touristNumber;
        }
        public TourBooking() { }    


    }
}
