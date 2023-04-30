using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class UnfulfilledTourCities
    {
        public int id { get; set; }

        public int guestId { get; set; }
        public string city { get; set; }

        public UnfulfilledTourCities(int guestId, string city)
        {
            this.guestId = guestId;
            this.city = city;
        }
    }
}
