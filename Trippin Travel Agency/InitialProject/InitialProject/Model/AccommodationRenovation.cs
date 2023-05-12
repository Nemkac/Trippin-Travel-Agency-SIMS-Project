using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationRenovation
    {
        public int id { get; set; }
        public int accommodationId { get; set; }
        public string accommodationName { get; set; }
        public string accommodationType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int duration { get; set; }
        public string description { get; set; }

        public AccommodationRenovation(int accommodationId, string accommodationName, string accommodationType, string startDate, string endDate, int duration, string description)
        {
            this.accommodationId = accommodationId;
            this.accommodationName = accommodationName;
            this.accommodationType = accommodationType;
            this.startDate = startDate;
            this.endDate = endDate;
            this.duration = duration;
            this.description = description;
        }
    }
}
