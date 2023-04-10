using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class BookingCancelationMessage
    {
        public int id { get; set; }
        public string message { get; set; }
        public int bookingId { get; set; }

        public BookingCancelationMessage() { }

        public BookingCancelationMessage(string message, int bookingId)
        {
            this.message = message;
            this.bookingId = bookingId;
        }
    }
}
