using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class UnfulfilledTourRequests
    {

        public int id {  get; set; }

        public int guestId { get; set; }
        public string city { get; set; }

        public string country { get; set; } 

        public language language { get; set; }

        public bool fulfilled { get; set; }

        public UnfulfilledTourRequests(int guestId, string city, string country, language language)
        {
            this.guestId = guestId;
            this.city = city;
            this.country = country;
            this.language = language;
            this.fulfilled = false;
        }
    }
}
