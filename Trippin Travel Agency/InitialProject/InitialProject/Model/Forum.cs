using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Forum
    {
        public int id {  get; set; }
        public bool isClosed { get; set; }
        public AccommodationLocation location { get; set; }
        public int creatorId { get; set; }
        public bool isVeryUseful { get; set; }
        public List<ForumComment> comments { get; set; }

        public Forum() { }  
        public Forum(bool isClosed, AccommodationLocation location, int creatorId, bool isVeryUseful, List<ForumComment> comments)
        {
            this.isClosed = isClosed;
            this.location = location;
            this.creatorId = creatorId;
            this.isVeryUseful = isVeryUseful;
            this.comments = comments;
            this.comments = comments;
        }
    }
}
