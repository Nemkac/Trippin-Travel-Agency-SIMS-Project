using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class RenovationSuggestion
    {
        public int id { get; set; }
        public string message { get; set; }
        public int urgency { get; set; }
        public DateTime sendingDate { get; set; }

        public RenovationSuggestion(string message, int urgency, DateTime sendingDate)
        {
            this.message = message;
            this.urgency = urgency;
            this.sendingDate = sendingDate;
        }

        public RenovationSuggestion() { }
    }
}
