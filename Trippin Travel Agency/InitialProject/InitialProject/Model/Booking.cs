using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using Microsoft.VisualBasic;

namespace InitialProject.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public Accommodation accommodation { get; set; }

        public DateAndTime arrival { get; set; }
        public DateAndTime departure { get; set; }

        public int stayingPeriod { get; set; }

        public Booking(Accommodation accommodation, DateAndTime arrival, DateAndTime departure, int stayingPeriod)
        {
            this.accommodation = accommodation;
            this.arrival = arrival;
            this.departure = departure;
            this.stayingPeriod = stayingPeriod;
        }

        public Booking() { }    
    }
}
