using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationLocation
    {
        public int Id {  get; set; }
        public string Country { get; set; }

        public string City { get; set; }

        public AccommodationLocation() {}
         
        public AccommodationLocation(string country, string city)
        {
            this.Country = country;
            this.City = city;

        }

        public AccommodationLocation GetLocation()
        {
            return (AccommodationLocation)this.MemberwiseClone();
        }

        public void Set(string country, string city)
        {
            this.Country = country;
            this.City = city;
        }

    }
}
