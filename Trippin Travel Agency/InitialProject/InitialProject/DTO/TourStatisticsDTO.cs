using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourStatisticsDTO
    {
        public int id {  get; set; }    
        public int tourId { get; set; }
        public int numberOfGuests { get; set; }
        public string tourName { get; set; }
        public DateTime startDate { get; set; }

        public TourStatisticsDTO(int id, int tourId, int numberOfGuests, string tourName, DateTime startDate)
        {
            this.id = id;
            this.tourId = tourId;
            this.numberOfGuests = numberOfGuests;
            this.tourName = tourName;
            this.startDate = startDate;
        }

        public TourStatisticsDTO() { }
    }
}
