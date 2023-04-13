using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationRate
    {
        public int id { get; set; }
        public int bookingId { get; set; }
        public int cleanness { get; set; }
        public int ownerRate { get; set; }
        public string comment { get; set; }
        public List<Image> images { get; set; }

        public AccommodationRate() { }

        public AccommodationRate(int bookingId, int cleanness, int ownerRate, string comment, List<Image> images)
        {
            this.bookingId = bookingId;
            this.cleanness = cleanness;
            this.ownerRate = ownerRate;
            this.comment = comment;
            this.images = images;
        }
    }
}
