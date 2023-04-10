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
        public bool seen { get; set; }

        public CanceledBooking(int bookingId) 
        {
            this.bookingId = bookingId;
            this.seen = false;
        }

        public CanceledBooking() { }
    }
}
