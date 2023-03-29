using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{

    public enum Status
    {
        Pending,
        Denied,
        Accepted
    }
    public class BookingDelaymentRequest
    {
        public int id { get; set; }
        public int bookingId { get; set; }
        public DateTime newArrival { get; set; }
        public DateTime newDeparture { get; set; }
        public Status status { get; set; }
        public string comment { get; set; }

        public BookingDelaymentRequest() { }

        public BookingDelaymentRequest(int bookingId, DateTime newArrival, DateTime newDeparture, Status status, string comment)
        {
            this.bookingId = bookingId;
            this.newArrival = newArrival;
            this.newDeparture = newDeparture;
            this.status = status;
            this.comment = comment;
        }
    }
}