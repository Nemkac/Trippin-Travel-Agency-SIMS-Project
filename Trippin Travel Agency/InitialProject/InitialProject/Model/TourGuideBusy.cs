using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourGuideBusy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime BusyDate { get; set; }
    }

}
