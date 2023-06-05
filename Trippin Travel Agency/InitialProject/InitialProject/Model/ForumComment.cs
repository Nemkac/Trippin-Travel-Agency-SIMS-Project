using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class ForumComment
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string username { get; set; }
        public string comment { get; set; }
        public DateTime postingDate { get; set; }
        public int numberOfReports { get; set; }
        public bool hasGuestVisited { get; set; }
        public int forumId { get; set; }
        public string userIcon { get; set; }

        public ForumComment() { }
        public ForumComment(int userId, string username, string comment, DateTime postingDate, int numberOfReports, bool hasGuestVisited, int forumId, string userIcon)
        {
            this.userId = userId;
            this.username = username;
            this.comment = comment;
            this.postingDate = postingDate;
            this.numberOfReports = numberOfReports;
            this.hasGuestVisited = hasGuestVisited;
            this.forumId = forumId;
            this.userIcon = userIcon;   
        }
    }
}
