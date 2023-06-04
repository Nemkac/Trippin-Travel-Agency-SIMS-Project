using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class SelectedForumViewModel : ViewModelBase
    {
        private ForumService forumService { get; set; }
        private UserService userService { get; set; }
        public ViewModelCommand AddComment { get; set; }

        public SelectedForumViewModel()
        {
            AddComment = new ViewModelCommand(AddNewComment);

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

        public void AddNewComment(object sender)
        {
            bool ifVisited = userService.HasGuestVisitedPlace(LoggedUser.id, new AccommodationLocation(forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[0], forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[1]));
            ForumComment comment = new ForumComment(LoggedUser.id, NewComment,DateTime.Today,0, ifVisited,GuestOneStaticHelper.selectedForum.id);
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

        }
    }
}
