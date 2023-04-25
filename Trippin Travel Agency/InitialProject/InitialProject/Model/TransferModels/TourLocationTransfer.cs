using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class TourLocationTransfer
    {
        public int id { get; set; }
        public string country { get; set; }
        public string city { get; set; }

        public TourLocationTransfer(string country, string city)
        {
            this.country = country;
            this.city = city;
        }

    }
}
