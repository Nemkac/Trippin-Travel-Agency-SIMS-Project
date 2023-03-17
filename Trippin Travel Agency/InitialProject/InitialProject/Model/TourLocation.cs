using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourLocation
    {
        public int id { get; set; } 

        public string city { get; set; }

        public string country { get; set; }

        public TourLocation(string city, string country)
        {
            this.city = city;
            this.country = country;
        }

        public TourLocation() { }   

    }
}
