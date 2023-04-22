using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class RequestMessage
    {
        public int id {  get; set; }    

        public string message { get; set; }

        public int requestId { get; set; }

        public int guestId { get; set; }    

        public RequestMessage(int requestId, int guestId)
        {
            this.message = "Tour request accepted by guide.";
            this.requestId = requestId;
            this.guestId = guestId;
        }

        public RequestMessage() { }
    }
}
