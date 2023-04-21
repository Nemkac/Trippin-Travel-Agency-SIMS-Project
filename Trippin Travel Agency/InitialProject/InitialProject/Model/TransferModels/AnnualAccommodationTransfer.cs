using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class AnnualAccommodationTransfer
    {
        public int id { get; set; }
        public int accommodationId { get; set; }
        public string accommodationName { get; set; }
        public string location { get; set; }
        public int maxNumOfGuests { get; set; }

        public AnnualAccommodationTransfer(int accommodationId, string accommodationName, string location, int maxNumOfGuests)
        {
            this.accommodationId = accommodationId;
            this.accommodationName = accommodationName;
            this.location = location;
            this.maxNumOfGuests = maxNumOfGuests;
        }
    }
}
