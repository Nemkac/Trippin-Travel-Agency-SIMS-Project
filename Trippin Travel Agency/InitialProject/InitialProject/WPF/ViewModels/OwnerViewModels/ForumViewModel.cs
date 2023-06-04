using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Migrations;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.ViewModels.OwnerViewModels;
using Microsoft.VisualBasic.ApplicationServices;
using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ForumViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;
        private UserService userService = new UserService();

        public int reportButtonNumber;

        private string _reportButtonName;
        public string ReportButtonName
        {
            get { return _reportButtonName; }
            set
            {
                _reportButtonName = value;
                OnPropertyChanged(nameof(ReportButtonName));
            }
        }

        private ObservableCollection<ForumComment> _comments;
        public ObservableCollection<ForumComment> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        private string _ownerInput;
        public string OwnerInput
        {
            get { return _ownerInput; }
            set
            {
                _ownerInput = value;
                OnPropertyChanged(nameof(OwnerInput));
            }
        }

        private string _ownerIcon;
        public string OwnerIcon
        {
            get { return _ownerIcon; }
            set
            {
                _ownerIcon = value;
                OnPropertyChanged(nameof(OwnerIcon));
            }
        }

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

        private string _headerButtonIconColor;
        public string HeaderButtonIconColor
        {
            get { return _headerButtonIconColor; }
            set
            {
                _headerButtonIconColor = value;
                OnPropertyChanged(nameof(HeaderButtonIconColor));
            }
        }

        public ICommand SendCommentCommand { get; }
        public ICommand ReportCommentCommand { get; }
        public ForumViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            HeaderButtonIconColor = Mediator.GetCurrentIsChecked() ? "#487eb0" : "#192a56";
            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";

            SendCommentCommand = new ViewModelCommand(SendComment);
            ReportCommentCommand = new ViewModelCommand(ReportComment);

            LoadComments();
        }

        private void SendComment(object obj)
        {
            if(OwnerInput !=  null)
            {
                ForumComment comment = new ForumComment(LoggedUser.id, LoggedUser.username, OwnerInput, DateTime.Now, 0, false, LoggedUser.VisitedForumId, "Location");
                DataBaseContext newCommentContext = new DataBaseContext();
                newCommentContext.ForumComments.Add(comment);
                newCommentContext.SaveChanges();
                LoadComments();
                OwnerInput = null;
            }
        }

        private void ReportComment(object obj)
        {
            if (obj is int id)
            {
                LoggedUser.commentIdToReport = id;
                _mainViewModel.ExecuteShowReportCommentCommand(null);
            }
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            HeaderButtonIconColor = isChecked ? "#487eb0" : "#192a56";
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }

        private void LoadComments()
        {
            int forumId = LoggedUser.VisitedForumId;
            Comments = GetForumComments(forumId);
        }

        private ObservableCollection<ForumComment> GetForumComments(int forumId)
        {
            DataBaseContext commentsContext = new DataBaseContext();
            List<ForumComment> comments = commentsContext.ForumComments
                .Where(comment => comment.forumId == forumId)
                .ToList();

            List<ReportedComment> reportedComments = commentsContext.ReportedComments.ToList();

            foreach (ForumComment comment in comments)
            {
                comment.numberOfReports = 0;
                commentsContext.ForumComments.Update(comment);
                commentsContext.SaveChanges();

                foreach(ReportedComment reportedComment in reportedComments)
                {
                    if (comment.id == reportedComment.commentId)
                    {
                        comment.numberOfReports++;
                        commentsContext.ForumComments.Update(comment);
                        commentsContext.SaveChanges();
                    }
                }
            }

            return new ObservableCollection<ForumComment>(comments);
        }
    }
}
