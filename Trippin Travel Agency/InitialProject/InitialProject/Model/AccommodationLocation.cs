using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationLocation
    {
        public int id {  get; set; }
        public string country { get; set; }

        public string city { get; set; }
        


        public AccommodationLocation() {}
         
        public AccommodationLocation(string country, string city)
        {
            this.country = country;
            this.city = city;

        }

    }
}
