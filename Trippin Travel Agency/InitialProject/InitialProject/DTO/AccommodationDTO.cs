using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Context;
using System.ComponentModel.DataAnnotations;

namespace InitialProject.DTO
{
    public enum Type
    {
        Apartment,
        House,
        Hut
    }
    class AccommodationDTO
    {
        public int id { get; set; }
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
            List<AccommodationLocation> locationsData = context.LocationsOfAccommodations.ToList();
            this.id = accommodation.id;
            this.name = accommodation.name;
            this.country = location[0];
            this.city = location[1];
            this.guestLimit= accommodation.guestLimit;
            this.minDaysBooked = accommodation.minDaysBooked;
            this.bookingCancelPeriod = accommodation.bookingCancelPeriodDays;
            if (accommodation.type == Model.Type.Hut)
            {
                this.type = Type.Hut;
            }
            if(accommodation.type == Model.Type.House)
            {
                this.type = Type.House;
            }
            if(accommodation.type == Model.Type.Apartment)
            {
                this.type = Type.Apartment;
            }
        }
    } 
}
