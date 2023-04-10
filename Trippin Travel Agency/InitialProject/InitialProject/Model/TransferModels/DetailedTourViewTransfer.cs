using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class DetailedTourViewTransfer
    {
        public DetailedTourViewTransfer()
        {
        }

        public int id { get; set; }

        public int tourId { get; set; }

        public DetailedTourViewTransfer(int tourId)
        {            
            this.tourId = tourId;
        }
    }
}
