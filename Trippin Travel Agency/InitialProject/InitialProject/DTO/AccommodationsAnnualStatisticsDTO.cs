using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class AccommodationsAnnualStatisticsDTO
    {
        public int id { get; set; }
        public int year { get; set; }
        public int numberOfBookings { get; set; }
        public int numberOfCancelations { get; set; }
        public int numberOfDelayments { get; set; }
        public int numberOfRenovationSuggestions { get; set; }

        public AccommodationsAnnualStatisticsDTO(int year, int numberOfBookings, int numberOfCancelations, int numberOfDelayments, int numberOfRenovationSuggestions)
        {
            this.year = year;
            this.numberOfBookings = numberOfBookings;
            this.numberOfCancelations = numberOfCancelations;
            this.numberOfDelayments = numberOfDelayments;
            this.numberOfRenovationSuggestions = numberOfRenovationSuggestions;
        }
    }
}
