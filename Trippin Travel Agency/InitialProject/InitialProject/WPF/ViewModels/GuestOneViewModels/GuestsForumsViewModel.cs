using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{


    public class GuestsForumsViewModel : ViewModelBase
    {
        private ForumService forumService { get; set; }
        public ViewModelCommand CreateForum { get; set; }
        private AccommodationLocationService accommodationLocationService { get; set; }
        private UserService userService { get; set; }
        public GuestsForumsViewModel()
        {
            forumService = new ForumService();
            userService = new UserService();
            accommodationLocationService = new AccommodationLocationService();
            CreateForum = new ViewModelCommand(CreateNewForum);
            ForumText = "You can close your forum for additional commenting, but you can never delete it.";
            ForumsGrid = new ObservableCollection<Forum>(forumService.GetByCreatorId());
        }

        private string forumText;
        public string ForumText
        {
            get { return forumText; }
            set
            {
                if (forumText != value)
                {
                    forumText = value;
                    OnPropertyChanged(nameof(ForumText));
                }
            }
        }

        private string inputCountry;
        public string InputCountry
        {
            get { return inputCountry; }
            set
            {
                if (inputCountry != value)
                {
                    inputCountry = value;
                    OnPropertyChanged(nameof(InputCountry));
                }
            }
        }

        private string inputCity;
        public string InputCity
        {
            get { return inputCity; }
            set
            {
                if (inputCity != value)
                {
                    inputCity = value;
                    OnPropertyChanged(nameof(InputCity));
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private ObservableCollection<Forum> forumsGrid;
        public ObservableCollection<Forum> ForumsGrid
        {
            get { return forumsGrid; }
            set
            {
                if (forumsGrid != value)
                {
                    forumsGrid = value;
                    OnPropertyChanged(nameof(ForumsGrid));
                }
            }
        }

        public void CreateNewForum(object sender)
        {
            bool ifLocationAlreadyExists = false;
            bool ifVisited = false;
            DataBaseContext context = new DataBaseContext();
            AccommodationLocation accommodationLocation;
            List<AccommodationLocation> locations = accommodationLocationService.GetAll();

            foreach (AccommodationLocation location in locations)
            {
                if (location.country.ToUpper().Equals(InputCountry.ToUpper()) && location.city.ToUpper().Equals(InputCity.ToUpper()))
                {

                    ForumComment comment = new ForumComment(LoggedUser.id, Comment, DateTime.Today, 0, userService.HasGuestVisitedPlace(location));
                    context.Attach(comment);
                    context.SaveChanges();
                    List<ForumComment> comments = new List<ForumComment>() { comment };

                    ifLocationAlreadyExists = true;
                    Forum forum = new Forum(false, location, LoggedUser.id, false, comments);
                    context.Attach(forum);
                    context.SaveChanges();

                    ForumMessage message = new ForumMessage(new string("New forum is opened at location " + location.city + "," + location.country), location.id, false, forum.id);
                    context.Attach(message);
                    context.SaveChanges();
                    ifVisited = false;

                }
            }
            if (!ifLocationAlreadyExists)
            {
                ForumComment comment = new ForumComment(LoggedUser.id, Comment, DateTime.Today, 0, false);
                context.Attach(comment);
                context.SaveChanges();
                List<ForumComment> comments = new List<ForumComment>() { comment };

                accommodationLocation = new AccommodationLocation(InputCountry, InputCity);
                context.Attach(accommodationLocation);
                context.SaveChanges();

                Forum forum = new Forum(false, accommodationLocation, LoggedUser.id, false, comments);
                context.Attach(forum);
                context.SaveChanges();

                ForumMessage message = new ForumMessage(new string("New forum is opened at location " + accommodationLocation.city + "," + accommodationLocation.country), accommodationLocation.id, false, forum.id);
                context.Attach(message);
                context.SaveChanges();

            }
        }
    }
}
