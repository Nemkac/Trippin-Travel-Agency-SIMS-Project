using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourDTO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string cityLocation { get; set; }
        public string countryLocation { get; set; }
        public string keypoints { get; set; }
        public language language { get; set; }
        public int touristLimit { get; set; }
        public string startDates { get; set; }
        public int hoursDuration { get; set; }

        public TourDTO(int id, string name, string description, string cityLocation, string countryLocation, string keypoints, language language, int touristLimit, string startDates, int hoursDuration)
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
        }
        public TourDTO() { }    
    }
}
