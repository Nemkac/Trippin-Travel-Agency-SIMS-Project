using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.DTO
{
    public class AccommodationStatisticsDTO
    {
        public int accommodationId { get; set; }
        public string accommodationName { get; set; }
        public string location { get; set; }
        public int guestLimit { get; set; }
        public int minDaysBooked { get; set; }
        public int bookingCancelPeriodDays { get; set; }
        public Type type { get; set; }

        public AccommodationStatisticsDTO(Accommodation accommodation, List<string> location) {
            this.accommodationId = accommodation.id;
            this.accommodationName = accommodation.name;
            this.location = location[0] + ", " + location[1];
            this.guestLimit = accommodation.guestLimit;
            this.minDaysBooked = accommodation.minDaysBooked;
            this.bookingCancelPeriodDays = accommodation.bookingCancelPeriodDays;
            this.type = (Type)accommodation.type;
        }
    }
}
