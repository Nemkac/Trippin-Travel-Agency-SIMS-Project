using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class CanceledBooking
    {
        public int id { get; set; }
        public int bookingId { get; set; }
        public int accommodationId { get; set; }
        public bool seen { get; set; }
        public DateTime plannedArrival { get; set; }

        public CanceledBooking(int bookingId, int accommodationId, DateTime plannedArrival) 
        {
            this.bookingId = bookingId;
            this.accommodationId = accommodationId;
            this.seen = false;
            this.plannedArrival = plannedArrival;

        }

        public CanceledBooking() { }
    }
}
