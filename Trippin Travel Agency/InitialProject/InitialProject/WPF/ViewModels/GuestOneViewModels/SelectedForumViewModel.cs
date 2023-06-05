using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InitialProject.WPF.View.GuestOne_Views;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class SelectedForumViewModel : ViewModelBase
    {
        private ForumService forumService { get; set; }
        private UserService userService { get; set; }
        public ViewModelCommand AddComment { get; set; }
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        bool isHelpOn = false;
        public SelectedForumViewModel()
        {
            AddComment = new ViewModelCommand(AddNewComment);
            Help = new ViewModelCommand(ShowHelp);
            OpenNavigator = new ViewModelCommand(ShowNavigator);

            forumService = new ForumService();
            userService = new UserService();
            Label = (forumService.GetLocation(GuestOneStaticHelper.selectedForum.id))[1];

            var commentsToGrid = from comment in forumService.GetForumsComments(GuestOneStaticHelper.selectedForum)
                                 select new
                                 {
                                     User = userService.GetById(comment.userId).firstName,
                                     Date = comment.postingDate.ToString().Substring(0, comment.postingDate.ToString().Length-11),
                                     Comment = comment.comment,
                                     Visited = userService.HasGuestVisitedPlace(userService.GetById(comment.userId).id, new AccommodationLocation(forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[0], forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[1])) ? new string("Been there") : new string("Hasn't been there")
                                 };
            Comments = commentsToGrid;

            if(GuestOneStaticHelper.selectedForum.isClosed == true)
            {
                NewComment = "The forum is closed for further commenting";
            }
        }

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                if (label != value)
                {
                    label = value;
                    OnPropertyChanged(nameof(Label));
                }
            }
        }

        private dynamic comments;
        public dynamic Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    OnPropertyChanged(nameof(Comments));
                }
            }
        }

        private string newComment;
        public string NewComment
        {
            get { return newComment; }
            set
            {
                if (newComment != value)
                {
                    newComment = value;
                    OnPropertyChanged(nameof(NewComment));
                }
            }
        }

        private string helpLand;
        public string HelpLand
        {
            get { return helpLand; }
            set
            {
                if (helpLand != value)
                {
                    helpLand = value;
                    OnPropertyChanged(nameof(HelpLand));
                }
            }
        }

        private string helpExit;
        public string HelpExit
        {
            get { return helpExit; }
            set
            {
                if (helpExit != value)
                {
                    helpExit = value;
                    OnPropertyChanged(nameof(helpExit));
                }
            }
        }

        private string warningMessage;
        public string WarningMessage
        {
            get { return warningMessage; }
            set
            {
                if (warningMessage != value)
                {
                    warningMessage = value;
                    OnPropertyChanged(nameof(WarningMessage));
                }
            }
        }

        public void ShowHelp(object sender)
        {
            if (isHelpOn)
            {
                HelpLand = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpLand = "You can see other comments, or add one of your own.Access comment input box with F1.\n Enter a comment then press ENTER";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.selectedForumInterface.Left + (GuestOneStaticHelper.selectedForumInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.selectedForumInterface.Top + (GuestOneStaticHelper.selectedForumInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.selectedForumInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        public void AddNewComment(object sender)
        {
            if (GuestOneStaticHelper.selectedForum.isClosed == false)
            {
                if (NewComment != null && NewComment != string.Empty)
                {
                    bool ifVisited = userService.HasGuestVisitedPlace(LoggedUser.id, new AccommodationLocation(forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[0], forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[1]));
                    ForumComment comment = new ForumComment(LoggedUser.id, NewComment, DateTime.Today, 0, ifVisited, GuestOneStaticHelper.selectedForum.id);
                    forumService.AddComment(comment);
                    var commentsToGrid = from comment1 in forumService.GetForumsComments(GuestOneStaticHelper.selectedForum)
                                         select new
                                         {
                                             User = userService.GetById(comment1.userId).firstName,
                                             Date = comment1.postingDate.ToString().Substring(0, comment1.postingDate.ToString().Length - 11),
                                             Comment = comment1.comment,
                                             Visited = ifVisited ? new string("Been there") : new string("Hasn't been there")
                                         };
                    Comments = commentsToGrid;
                    NewComment = string.Empty;
                }
                else
                {
                    WarningMessage = "Please add comment";
                }
            }
        }
    }
}
