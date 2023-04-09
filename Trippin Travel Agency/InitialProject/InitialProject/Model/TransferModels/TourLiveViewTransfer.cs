using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class TourLiveViewTransfer
    {
        public int id { get; set; }
        public int tourId { get; set; } 

        public TourLiveViewTransfer(int tourId)
        {
            this.tourId = tourId; 
        }

    }
}
