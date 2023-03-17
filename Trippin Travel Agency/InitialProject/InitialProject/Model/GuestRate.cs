using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    class GuestRate
    {
        public int cleanness { get; set; }
        public int respectingRules { get; set; }
        public string comment { get; set; }
        public int overallRating { get; set; }  
        public int userId { get; set; }

        public GuestRate(int cleanness, int respectingRules, string comment, int userId)
        {
            this.cleanness = cleanness;
            this.respectingRules = respectingRules;
            this.comment = comment;
            this.overallRating = (cleanness + respectingRules) / 2; 
            this.userId = userId;
        }
    }
}
