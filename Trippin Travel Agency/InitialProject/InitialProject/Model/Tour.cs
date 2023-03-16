using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public TourLocation location { get; set; }

        public ICollection<KeyPoint> keyPoints { get; set; } // ovo kada se dobavlja bice nova metoda u sqlitedataaccess

        public string description { get; set; } 

        public language language { get; set; }  

        public int touristLimit { get; set; }

        public DateTime startDates { get; set; }

        public int hoursDuration { get; set; }

        public Tour(int id, string name, TourLocation location, ICollection<KeyPoint> keyPoints, string description, language language, int touristLimit, DateTime startDates, int hoursDuration)
        {
            this.id = id;
            this.name = name;
            this.location = location;
            this.keyPoints = keyPoints;
            this.description = description;
            this.language = language;
            this.touristLimit = touristLimit;
            this.startDates = startDates;
            this.hoursDuration = hoursDuration;
        }

        public Tour() { }
    }
}

