using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    class BookingDTO
    {
        public string guestName { get; set; }
        public int bookingId { get; set; }
        public string accommodationName { get; set; }
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }  
        public int daysToStay { get; set; }

        public BookingDTO() { }

        public BookingDTO(string guestName, int bookingId, string accommodationName, string arrivalDate, string departureDate, int daysToStay)
        {
            this.guestName = guestName;
            this.bookingId = bookingId;
            this.accommodationName = accommodationName;
            this.arrivalDate = arrivalDate;
            this.departureDate = departureDate;
            this.daysToStay = daysToStay;
        }
    }
}
