using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class ForumMessage
    {
        public int id { get;set; }
        public string message { get;set; }
        public int locationId { get;set; }
        public bool seen { get; set; }
        public int forumId { get; set; }

        public ForumMessage() { }
        public ForumMessage(string message, int locationId, bool seen, int forumId)
        {
            this.message = message;
            this.locationId = locationId;
            this.seen = seen;
            this.forumId = forumId;
        }
    }
}
