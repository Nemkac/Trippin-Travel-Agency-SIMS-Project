using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class DelayedBookings
    {
        public int id { get; set; }
        public int bookingId { get; set; }
        public int accommodationId { get; set; }
        public DateTime previousArrival { get; set; }

        public DelayedBookings(int bookingId, int accommodationId, DateTime previousArrival)
        {
            this.bookingId = bookingId;
            this.accommodationId = accommodationId;
            this.previousArrival = previousArrival;
        }

        public DelayedBookings() { }
    }
}
