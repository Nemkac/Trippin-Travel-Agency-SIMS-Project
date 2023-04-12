using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{

    public enum language
    {
        English,
        Spanish,
        German,
        Russian,
        French,
        Serbian
    }

    [Table("Tour")]
    public class Tour
    {
        public int id { get; set; }

        public string name { get; set; }

        public int location { get; set; }

        public ICollection<KeyPoint> keyPoints { get; set; }

        public string description { get; set; }

        public language language { get; set; }

        public int touristLimit { get; set; }

        public DateTime startDates { get; set; }

        public int hoursDuration { get; set; }

        public List<Image> imageLinks { get; set; }

        public bool active { get; set; }

        public int guideId { get; set; }

        public bool finished { get; set; }
 
        public Tour(string name, int location, ICollection<KeyPoint> keyPoints, string description, language language, int touristLimit, DateTime startDates, int hoursDuration, List<Image> imageLinks, bool active, int guideID)
        {
            this.name = name;
            this.location = location;
            this.keyPoints = keyPoints.ToList();
            this.description = description;
            this.language = language;
            this.touristLimit = touristLimit;
            this.startDates = startDates;
            this.hoursDuration = hoursDuration;
            this.imageLinks = imageLinks;
            this.active = active;
            this.guideId = guideID;
            this.finished = false;
        }

        public Tour(string name, int location, ICollection<KeyPoint> keyPoints, string description, language language, int touristLimit, DateTime startDates, int hoursDuration)
        {
            this.name = name;
            this.location = location;
            this.keyPoints = keyPoints.ToList();
            this.description = description;
            this.language = language;
            this.touristLimit = touristLimit;
            this.startDates = startDates;
            this.hoursDuration = hoursDuration;
            this.finished = false;
        }

        public Tour() { }


    }
}

