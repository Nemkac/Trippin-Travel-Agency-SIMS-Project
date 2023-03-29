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
        public int accommodationId { get; set; }
        public string arrival { get; set; }
        public string departure { get; set; }
        public int daysToStay { get; set; }
        public int guestId { get; set; }    

        public Booking(int accommodationId, string arrival, string departure, int daysToStay, int guestId)
        {
            this.accommodationId = accommodationId;
            this.arrival = arrival;
            this.departure = departure;
            this.daysToStay = daysToStay;
            this.guestId = guestId; 
        }

        public Booking() { }    
    }
}
