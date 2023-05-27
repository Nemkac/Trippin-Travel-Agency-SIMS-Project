using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourRequestDTO
    {
        public string city { get; set; }
        public string country { get; set; }
        public language language { get; set; } 

        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public TourRequestStatus status { get; set; }  

        public TourRequestDTO( string city, string country, language language, string dateFrom, string dateTo, TourRequestStatus status)
        {
            this.city = city;
            this.country = country;
            this.language = language;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.status = status;
        }
    }
}
