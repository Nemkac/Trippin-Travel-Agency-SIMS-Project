using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public enum TourRequestStatus { 
        OnHold,
        Invalid,
        Accepted
    }
    public class TourRequest
    {

        public int id { get; set; }    
        public string city { get; set; }
        public string country { get; set; } 
        public int numberOfTourists { get; set; }
        public language language { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime? acceptedDate { get; set; }
        public string description { get; set; }
        public TourRequestStatus status { get; set; }
        public int guestId { get; set; }
        public bool sent { get; set; } 

        public TourRequest(string city, string country, int numberOfTourists, language language, DateTime startDate, DateTime endDate, string description, int guestId)
        {
            this.city = city;
            this.country = country;
            this.numberOfTourists = numberOfTourists;
            this.language = language;
            this.startDate = startDate;
            this.endDate = endDate;
            this.description = description;
            this.status = TourRequestStatus.OnHold;
            this.guestId = guestId;
            this.acceptedDate = null;
            this.sent = false;

        }
    }
}
