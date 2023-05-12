using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class RenovationSuggestionMessage
    {
        public int id {  get; set; }
        public string message { get; set; }
        public int accommodationId { get; set; }

        public RenovationSuggestionMessage (string message, int accommodationId)
        {
            this.message = message;
            this.accommodationId = accommodationId;
        }

        public RenovationSuggestionMessage() { }
    }
}
