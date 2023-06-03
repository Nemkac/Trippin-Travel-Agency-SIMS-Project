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

            DataBaseContext context = new DataBaseContext();

            var commentsToGrid = from comment in context.ForumComments.ToList()
                                 select new
                                 {
                                     User = userService.GetById(comment.userId).firstName,
                                     Date = comment.postingDate.ToString().Substring(0, comment.postingDate.ToString().Length-11),
                                     Comment = comment.comment
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
            bool ifVisited = userService.HasGuestVisitedPlace(new AccommodationLocation(forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[0], forumService.GetLocation(GuestOneStaticHelper.selectedForum.id)[1]));
            //ForumComment comment = new ForumComment(LoggedUser.id, NewComment,DateTime.Today,0, ifVisited);

            // na listu komentara selektovanog foruma dodas taj novi kom i updajtujes bazu
            

        }
    }
}
