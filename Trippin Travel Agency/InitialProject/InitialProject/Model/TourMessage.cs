using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourMessage
    {
        public int id {  get; set; }    

        public string message { get; set; }

        public int tourId { get; set; }

        public int guestId { get; set; }    

        public int keyPointId { get; set; }

        public int numberOfGuests { get; set; }

        public TourMessage(int id, int tourId, int guestId, int keyPointId, int numberOfGuests)
        {
            this.id = id;
            this.message = "Vodič je potvrdio vaše prisustvo na turi :";
            this.tourId = tourId;
            this.guestId = guestId;
            this.keyPointId = keyPointId;
            this.numberOfGuests = numberOfGuests;
        }

        public TourMessage() { }
    }
}
