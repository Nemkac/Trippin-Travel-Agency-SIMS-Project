using InitialProject.Context;
using InitialProject.Model;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.DTO
{
    public enum Type
    {
        Apartment,
        House,
        Hut
    }
    public class AccommodationDTO
    {
        public int accommodationId { get; set; }
        public string name{ get; set; }
        public string country{ get; set; }
        public string city{ get; set; }
        public int guestLimit{ get; set; }
        public int minDaysBooked{ get; set; }
        public int bookingCancelPeriod{ get; set; }
        public Type type { get; set; }
 
        public AccommodationDTO() { }

        public AccommodationDTO(Accommodation accommodation, List<string> location)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
            this.accommodationId = accommodation.id;
            this.name = accommodation.name;
            this.country = location[0];
            this.city = location[1];
            this.guestLimit= accommodation.guestLimit;
            this.minDaysBooked = accommodation.minDaysBooked;
            this.bookingCancelPeriod = accommodation.bookingCancelPeriodDays;
            this.type = (Type)accommodation.type;
        }
    } 
}
