using InitialProject.Context;
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


        private string _guestNameText;
        public string GuestNameText
        {
            get { return _guestNameText; }
            set
            {
                if (_guestNameText != value)
                {
                    _guestNameText = value;
                    OnPropertyChanged(nameof(GuestNameText));
                }
            }
        }

        private string _forumLocationText;

        public string ForumLocationText
        {
            get { return _forumLocationText; }
            set
            {
                _forumLocationText = value;
                OnPropertyChanged(nameof(ForumLocationText));
            }
        }

        private string _reportFromText;

        public string ReportFromText
        {
            get { return _reportFromText; }
            set
            {
                _reportFromText = value;
                OnPropertyChanged(nameof(ReportFromText));
            }
        }

        private string _explanationText;

        public string ExplanationText
        {
            get { return _explanationText; }
            set
            {
                _explanationText = value;
                OnPropertyChanged(nameof(ExplanationText));
            }
        }

        private string _submitReportText;

        public string SubmitReportText
        {
            get { return _submitReportText; }
            set
            {
                _submitReportText = value;
                OnPropertyChanged(nameof(SubmitReportText));
            }
        }

        public ViewModelCommand SubmitReportCommand { get; }

        public ReportCommentViewModel()
        {
            ForumComment commentToReport = GetCommentToReport();
            DisplayData(commentToReport);
            Mediator.IsCheckedChanged += OnIsCheckedChanged;
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";

            GuestNameText = Mediator.GetCurrentIsLanguageChecked() ? "Ime gosta" : "Guest name";
            ForumLocationText = Mediator.GetCurrentIsLanguageChecked() ? "Lokacija foruma" : "Forum location";
            ReportFromText = Mediator.GetCurrentIsLanguageChecked() ? "Prijava od" : "Report from";
            ExplanationText = Mediator.GetCurrentIsLanguageChecked() ? "Obrazlozenje" : "Explanation";
            SubmitReportText = Mediator.GetCurrentIsLanguageChecked() ? "Posalji prijavu" : "Submit report";

            SubmitReportCommand = new ViewModelCommand(SubmitReport);
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            GuestNameText = isChecked ? "Ime gosta" : "Guest name";
            ForumLocationText = isChecked ? "Lokacija foruma" : "Forum location";
            ReportFromText = isChecked ? "Prijava od" : "Report from";
            ExplanationText = isChecked ? "Obrazlozenje" : "Explanation";
            SubmitReportText = isChecked ? "Posalji prijavu" : "Submit report";
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
