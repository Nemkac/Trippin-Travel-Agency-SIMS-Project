using InitialProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class TourBookingTransfer
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string cityLocation { get; set; }
        public string countryLocation { get; set; }
        public string keypoints { get; set; }
        public language language { get; set; }
        public int touristLimit { get; set; }
        public DateTime startDates { get; set; }
        public int hoursDuration { get; set; }

        public int numberOfGuests { get; set; }

        public TourBookingTransfer(int id, string name, string description, string cityLocation, string countryLocation, string keypoints, language language, int touristLimit, DateTime startDates, int hoursDuration, int numberOfGuests)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.cityLocation = cityLocation;
            this.countryLocation = countryLocation;
            this.keypoints = keypoints;
            this.language = language;
            this.touristLimit = touristLimit;
            this.startDates = startDates;
            this.hoursDuration = hoursDuration;
            this.numberOfGuests = numberOfGuests;
        }

        public TourBookingTransfer()
        {
        }
    }
}
