using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class TourFlagTransfer
    {
        public int id { get; set; }
        public int flag { get; set; }

        public TourFlagTransfer(int flag)
        {
            this.flag = flag; 
        }

    }
}
