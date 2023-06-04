using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{


    public class GuestsForumsViewModel : ViewModelBase
    {
        private ForumService forumService { get; set; }
        public ViewModelCommand CreateForum { get; set; }
        private AccommodationLocationService accommodationLocationService { get; set; }
        private UserService userService { get; set; }
        public ViewModelCommand CloseForum { get; set; }
        public ViewModelCommand ShowForum { get; set; }
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }

        bool isHelpOn = false;
        public GuestsForumsViewModel()
        {
            CloseForum = new ViewModelCommand(CloseGuestsForum);

            forumService = new ForumService();
            userService = new UserService();
            accommodationLocationService = new AccommodationLocationService();
            CreateForum = new ViewModelCommand(CreateNewForum);
            ShowForum = new ViewModelCommand(ShowSelectedForum);
            Help = new ViewModelCommand(ShowHelp);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            ForumText = "Here are shown forums you have created.\n You can close one of your forums from additional commenting, but you can never delete it.";

            var forumsToGrid = from forum in forumService.GetByCreatorId()
                               select new
                               {
                                   Country = forumService.GetLocation(forum.id)[0],
                                   City = forumService.GetLocation(forum.id)[1],
                                   IfClosed = forum.isClosed ? new String("Closed") : new String("Opened"),
                                   IfUseful = forum.isVeryUseful ? new String("Useful") : new String("-")
                               };
            ForumsGrid = forumsToGrid;
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

        private string debug;
        public string Debug
        {
            get { return debug; }
            set
            {
                if (debug != value)
                {
                    debug = value;
                    OnPropertyChanged(nameof(Debug));
                }
            }
        }

        private dynamic forumsGrid;
        public dynamic ForumsGrid
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

        private int selectedForum;
        public int SelectedForum
        {
            get { return selectedForum; }
            set
            {
                if (selectedForum != value)
                {
                    selectedForum = value;
                    OnPropertyChanged(nameof(SelectedForum));
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

        private string warningMessage2;
        public string WarningMessage2
        {
            get { return warningMessage2; }
            set
            {
                if (warningMessage2 != value)
                {
                    warningMessage2 = value;
                    OnPropertyChanged(nameof(WarningMessage2));
                }
            }
        }

        public void CreateNewForum(object sender)
        {
            if(InputCity == null || InputCity == string.Empty || InputCountry == null || InputCountry == string.Empty || Comment == null || Comment == string.Empty)
            {
                WarningMessage = "You must enter all parameters first";
                return;
            }
            bool ifLocationAlreadyExists = false;
            bool ifVisited = false;
            DataBaseContext context = new DataBaseContext();
            AccommodationLocation accommodationLocation;
            List<AccommodationLocation> locations = accommodationLocationService.GetAll();

            foreach (AccommodationLocation location in locations)
            {
                if (location.country.ToUpper().Equals(InputCountry.ToUpper()) && location.city.ToUpper().Equals(InputCity.ToUpper()))
                {
                    ifLocationAlreadyExists = true;

                    Forum forum = new Forum(false, location, LoggedUser.id, false, null);
                    context.Attach(forum);
                    context.SaveChanges();

                    ForumComment comment = new ForumComment(LoggedUser.id, Comment, DateTime.Today, 0, userService.HasGuestVisitedPlace(LoggedUser.id, location), forum.id);
                    context.Attach(comment);
                    context.SaveChanges();
                    List<ForumComment> comments = new List<ForumComment>() { comment };

                    forum.comments = comments;
                    context.Update(forum);
                    context.SaveChanges();


                    ForumMessage message = new ForumMessage(new string("New forum is opened at location " + location.city + "," + location.country), location.id, false, forum.id);
                    context.Attach(message);
                    context.SaveChanges();
                    ifVisited = false;
                    WarningMessage = string.Empty;

                }
            }
            if (!ifLocationAlreadyExists)
            {
                accommodationLocation = new AccommodationLocation(InputCountry, InputCity);
                context.Attach(accommodationLocation);
                context.SaveChanges();

                Forum forum = new Forum(false, accommodationLocation, LoggedUser.id, false, null);
                context.Attach(forum);
                context.SaveChanges();

                ForumComment comment = new ForumComment(LoggedUser.id, Comment, DateTime.Today, 0, false, forum.id);
                context.Attach(comment);
                context.SaveChanges();
                List<ForumComment> comments = new List<ForumComment>() { comment };

                forum.comments = comments;
                context.Update(forum);
                context.SaveChanges();

                ForumMessage message = new ForumMessage(new string("New forum is opened at location " + accommodationLocation.city + "," + accommodationLocation.country), accommodationLocation.id, false, forum.id);
                context.Attach(message);
                context.SaveChanges();

            }
            var forumsToGrid = from forum1 in forumService.GetByCreatorId()
                               select new
                               {
                                   Country = forumService.GetLocation(forum1.id)[0],
                                   City = forumService.GetLocation(forum1.id)[1],
                                   IfClosed = forum1.isClosed ? new String("Closed") : new String("Opened"),
                                   IfUseful = forum1.isVeryUseful ? new String("Useful") : new String("-")
                               };
            ForumsGrid = forumsToGrid;
        }

        public void CloseGuestsForum(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> forums = forumService.GetByCreatorId();
            Forum forum = forums[SelectedForum];
            forum.isClosed = true;
            context.Update(forum);
            context.SaveChanges();
            var forumsToGrid = from forum1 in forumService.GetByCreatorId()
                               select new
                               {
                                   Country = forumService.GetLocation(forum1.id)[0],
                                   City = forumService.GetLocation(forum1.id)[1],
                                   IfClosed = forum1.isClosed ? new String("Closed") : new String("Opened"),
                                   IfUseful = forum1.isVeryUseful ? new String("Useful") : new String("-")
                               };
            ForumsGrid = forumsToGrid;
        }

        public void ShowSelectedForum(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> forums = context.Forums.ToList();
            GuestOneStaticHelper.selectedForum = forums[SelectedForum];
            SelectedForumInterface selectedForumInterface = new SelectedForumInterface();
            selectedForumInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            selectedForumInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            selectedForumInterface.Show();
            GuestOneStaticHelper.guestsForumsInterface.Hide();
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
                HelpLand = "Go through input parameters with UP and DOWN arrows.\nThen press ";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.guestsForumsInterface.Left + (GuestOneStaticHelper.guestsForumsInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.guestsForumsInterface.Top + (GuestOneStaticHelper.guestsForumsInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.guestsForumsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }
    }
}
