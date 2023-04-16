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

        public DelayedBookings(int bookingId)
        {
            this.bookingId = bookingId;
        }

        public DelayedBookings() { }
    }
}
