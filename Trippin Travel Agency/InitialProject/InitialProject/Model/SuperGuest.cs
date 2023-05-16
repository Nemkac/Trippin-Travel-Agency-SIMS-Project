using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class SuperGuest
    {
        public int id {  get; set; }
        public int guestId { get; set; }
        public int points { get; set; }
        public DateTime titleAcquisition { get;set; }
        public int ifActive { get; set; }

        public SuperGuest() {}

        public SuperGuest(int guestId, int points, DateTime titleAcquisition, int ifActive)
        {
            this.guestId = guestId;
            this.points = points;
            this.titleAcquisition = titleAcquisition;
            this.ifActive = ifActive;
        }
    }
}
