using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class UnfulfilledTourLanguages
    {
        public int id { get; set; } 

        public int guestId { get; set; }

        public language language { get; set; }

        public UnfulfilledTourLanguages(int guestId, language language)
        {
            this.guestId = guestId;
            this.language = language;
        }
    }
}
