using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class UnfulfilledTourCountries
    {

        public int id { get; set; }

        public int guestId { get; set; }

        public string country { get; set; }

        public UnfulfilledTourCountries(int guestId, string country)
        {
            this.guestId = guestId;
            this.country = country;
        }
    }
}
