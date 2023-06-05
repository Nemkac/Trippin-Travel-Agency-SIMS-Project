using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class ReportedComment
    {
        public int id { get; set; }
        public int forumId { get; set; }
        public int commentId { get; set; }
        public string senderUsername { get; set; }
        public string reporterUsername { get; set; }
        public string explanation { get; set; }

        public ReportedComment(int forumId, int commentId, string senderUsername, string reporterUsername, string explanation)
        {
            this.forumId = forumId;
            this.commentId = commentId;
            this.senderUsername = senderUsername;
            this.reporterUsername = reporterUsername;
            this.explanation = explanation;
        }
    }
}
