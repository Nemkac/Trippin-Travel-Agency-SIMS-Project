using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class RequestDTO
    {
        public string guestName { get; set; }
        public int bookingId { get; set; }
        public int accommodationId { get; set; }
        public DateTime oldArrival { get; set; }
        public DateTime oldDeparture { get; set; }
        public DateTime newArrival { get; set; }
        public DateTime newDeparture { get; set; }
        public string possible { get; set; }

        public RequestDTO() {}

        public RequestDTO(string guestName, int bookingId, int accommodationId, DateTime oldArrival, DateTime oldDeparture, DateTime newArrival, DateTime newDeparture, string possible)
        {
            this.guestName = guestName;
            this.bookingId = bookingId;
            this.accommodationId = accommodationId;
            this.oldArrival = oldArrival;
            this.oldDeparture = oldDeparture;
            this.newArrival = newArrival;
            this.newDeparture = newDeparture;
            this.possible = possible;
        }
    }
}
