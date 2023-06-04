using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Migrations;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using Microsoft.VisualBasic.ApplicationServices;
using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ForumViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;
        private UserService userService = new UserService();

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
        public ForumViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            HeaderButtonIconColor = Mediator.GetCurrentIsChecked() ? "#487eb0" : "#192a56";
            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";

            SendCommentCommand = new ViewModelCommand(SendComment);

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

            return new ObservableCollection<ForumComment>(comments);
        }
    }
}
