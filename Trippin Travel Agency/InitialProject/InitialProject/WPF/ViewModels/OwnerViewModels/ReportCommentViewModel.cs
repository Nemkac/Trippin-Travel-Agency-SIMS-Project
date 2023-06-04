using InitialProject.Context;
using InitialProject.Migrations;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ReportCommentViewModel : ViewModelBase
    {
        private UserService userService = new UserService();
        private AccommodationService accommodationService = new(new AccommodationRepository());

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }
        private string _explanation;
        public string Explanation
        {
            get { return _explanation; }
            set
            {
                _explanation = value;
                OnPropertyChanged(nameof(Explanation));
            }
        }
        private string _guestName;
        public string Guestname
        {
            get { return _guestName; }
            set
            {
                _guestName = value;
                OnPropertyChanged(nameof(Guestname));
            }
        }
        private string _forumlocation;
        public string ForumLocation
        {
            get { return _forumlocation; }
            set
            {
                _forumlocation = value;
                OnPropertyChanged(nameof(ForumLocation));
            }
        }
        private string _reporterName;
        public string ReporterName
        {
            get { return _reporterName; }
            set
            {
                _reporterName = value;
                OnPropertyChanged(nameof(ReporterName));
            }
        }

        private string _feedback;
        public string FeedBack
        {
            get { return _feedback; }
            set
            {
                _feedback = value;
                OnPropertyChanged(nameof(FeedBack));
            }
        }

        public ViewModelCommand SubmitReportCommand { get; }

        public ReportCommentViewModel()
        {
            ForumComment commentToReport = GetCommentToReport();
            DisplayData(commentToReport);
            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            SubmitReportCommand = new ViewModelCommand(SubmitReport);
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }

        private ForumComment GetCommentToReport()
        {
            int commentId = LoggedUser.commentIdToReport;
            DataBaseContext forumCommentsContext = new DataBaseContext();
            List<ForumComment> comments = forumCommentsContext.ForumComments.ToList();

            foreach (ForumComment comment in comments)
            {
                if (comment.id == commentId)
                {
                    ForumComment commentToReport = comment;
                    return commentToReport;
                }
            }

            return null;
        }
        private void DisplayData(ForumComment commentToReport)
        {
            User userToReport = userService.GetById(commentToReport.userId);
            Guestname = userToReport.username;

            List<string> forumLocation = accommodationService.GetAccommodationLocationCityAndCountryByForumId(commentToReport.forumId);
            ForumLocation = forumLocation[1] + ", " + forumLocation[0];

            ReporterName = LoggedUser.username;
        }

        private void SubmitReport(object obj)
        {
            ForumComment commentToReport = GetCommentToReport();
            ReportedComment reportedComment = new ReportedComment(commentToReport.forumId, commentToReport.id, Guestname, ReporterName, Explanation);

            DataBaseContext forumCommentsContext = new DataBaseContext();
            List<ForumComment> comments = forumCommentsContext.ForumComments.ToList();

            foreach(ForumComment comment in comments)
            {
                if(comment.id == commentToReport.id)
                {
                    comment.numberOfReports++;
                    forumCommentsContext.ForumComments.Update(comment);
                    forumCommentsContext.SaveChanges();
                }
            }

            DataBaseContext reportedCommentContext = new DataBaseContext();
            reportedCommentContext.ReportedComments.Add(reportedComment);
            reportedCommentContext.SaveChanges();

            FeedBack = "Report submitted!";

            Guestname = null;
            ForumLocation = null;
            ReporterName = null;
            Explanation = null;
        }
    }
}
