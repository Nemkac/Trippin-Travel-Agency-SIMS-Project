using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class AcceptedTourRequestViewTransfer
    {
        public int id { get; set; }
        public int requestId { get; set; } 

        public AcceptedTourRequestViewTransfer(int requestId)
        {
            this.requestId = requestId; 
        }

    }
}
