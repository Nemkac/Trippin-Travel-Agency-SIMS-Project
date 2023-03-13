using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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


    public class Tour
    {
        public int id { get; set; }

        public string name { get; set; }

        public int location { get; set; }

        public int keyPoints { get; set; } // ovo kada se dobavlja bice nova metoda u sqlitedataaccess

        public string description { get; set; } 

        public language language { get; set; }  

        public int touristLimit { get; set; }

        public List<DateAndTime> startDates = new List<DateAndTime>();  

        public int hoursDuration { get; set; }

        public Tour(int id, string name, int location, int keyPoints, string description, language language, int touristLimit, List<DateAndTime> startDates, int hoursDuration)
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

// Ostao je problem povezivanja tabela za keyPoite (mozda)
// Ostao je problem sa listom datuma. -> Kako sve to povezati sa bazom. 