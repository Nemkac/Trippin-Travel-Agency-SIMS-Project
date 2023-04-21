using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class MonthlyAccommodationTransfer
    {
        public int id { get; set; }
        public int selectedYear { get; set; }
        public int accommodationId { get; set; }

        public MonthlyAccommodationTransfer(int selectedYear, int accommodationId)
        {
            this.selectedYear = selectedYear;
            this.accommodationId = accommodationId;
        }
    }
}
