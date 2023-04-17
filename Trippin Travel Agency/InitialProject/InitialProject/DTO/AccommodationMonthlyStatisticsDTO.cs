using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class AccommodationMonthlyStatisticsDTO
    {
        public int id { get; set; }
        public int selectedYear { get; set; }
        public int month { get; set; }
        public int numberOfBookings { get; set; }
        public int numberOfCancelations { get; set; }
        public int numberOfDelayments { get; set; }
        public int numberOfRenovationSuggestions { get; set; }

        public AccommodationMonthlyStatisticsDTO(int month, int numberOfBookings, int numberOfCancelations, int numberOfDelayments)
        {
            this.month = month;
            this.numberOfBookings = numberOfBookings;
            this.numberOfCancelations = numberOfCancelations;
            this.numberOfDelayments = numberOfDelayments;
            //this.numberOfRenovationSuggestions = numberOfRenovationSuggestions;
        }
    }
}
