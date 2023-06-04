using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ToastNotifications.Messages.Warning;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class ForumsViewModel : ViewModelBase
    {
        public ViewModelCommand GoMyForums { get; set; }
        public ViewModelCommand ShowForum { get; set; }
        public ViewModelCommand Search { get; set; }
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        private UserService userService { get; set; }
        private ForumService forumService { get; set; }

        
        bool isHelpOn = false;

        public ForumsViewModel()
        {
            forumService = new ForumService();
            userService = new UserService();
            ForumText = "Forum is a great place where you can get to know a lot about certain place.\n" +
                "You can create your own forum or open an existing one.";
            GoMyForums = new ViewModelCommand(GoToMyForums);
            ShowForum = new ViewModelCommand(ShowSelectedForum);
            Search = new ViewModelCommand(SearchBy);
            Help = new ViewModelCommand(ShowHelp);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            var forumsToGrid = from forum in forumService.GetAll()
                               select new
                               {
                                   Country = forumService.GetLocation(forum.id)[0],
                                   City = forumService.GetLocation(forum.id)[1],
                                   IfClosed = forum.isClosed ? new String("Closed") : new String("Opened"),
                                   IfUseful = userService.IsForumSuperUseful(forum) ? new String("Super Useful!") : new String("-"),

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

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.guestOneInterface.Left + (GuestOneStaticHelper.guestOneInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.guestOneInterface.Top + (GuestOneStaticHelper.guestOneInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.guestOneInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
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

                HelpLand = "Press Left Ctrl to input a country and \nRight Ctrl to input a city you want to search forums by.\nThen press TAB to access them and \nUP and DOWN arrows to go through them.\nWhen forum is selected, press F1 to open it.\nYou can press CTRL+M to go to your forums.";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        public void GoToMyForums(object sender)
        {
            GuestsForumsInterface guestsForumsInterface = new GuestsForumsInterface();
            guestsForumsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestsForumsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            guestsForumsInterface.Show();
            GuestOneStaticHelper.forumsInterface.Hide();
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
            GuestOneStaticHelper.forumsInterface.Hide();
        }

        public void SearchBy(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> allForums = context.Forums.ToList();
            List<Forum> byCountry = forumService.GetAllByCountry(InputCountry);
            List<Forum> byCity = forumService.GetAllByCity(InputCity);
            List<Forum> restult1 = new List<Forum>();
            List<Forum> result = new List<Forum>();
            if((InputCountry==null||InputCountry == string.Empty) && (inputCity == null || inputCity == string.Empty))
            {
                result = byCity;
                var forumsToGrid1 = from forum in allForums
                                    select new
                                    {
                                        Country = forumService.GetLocation(forum.id)[0],
                                        City = forumService.GetLocation(forum.id)[1],
                                        IfClosed = forum.isClosed ? new String("Closed") : new String("Opened"),
                                        IfUseful = userService.IsForumSuperUseful(forum) ? new String("Super Useful!") : new String("-"),

                                    };
                ForumsGrid = forumsToGrid1;
                return;
            }

            if (byCountry == null)
            {
                result = byCity;
                var forumsToGrid1 = from forum in result
                                   select new
                                   {
                                       Country = forumService.GetLocation(forum.id)[0],
                                       City = forumService.GetLocation(forum.id)[1],
                                       IfClosed = forum.isClosed ? new String("Closed") : new String("Opened"),
                                       IfUseful = userService.IsForumSuperUseful(forum) ? new String("Super Useful!") : new String("-"),

                                   };
                ForumsGrid = forumsToGrid1;
                return;
                    
            }
            if(byCity == null)
            {
                result = byCountry;
                var forumsToGrid2 = from forum in result
                                   select new
                                   {
                                       Country = forumService.GetLocation(forum.id)[0],
                                       City = forumService.GetLocation(forum.id)[1],
                                       IfClosed = forum.isClosed ? new String("Closed") : new String("Opened"),
                                       IfUseful = userService.IsForumSuperUseful(forum) ? new String("Super Useful!") : new String("-"),

                                   };
                ForumsGrid = forumsToGrid2;
                return;
            }
            restult1 = forumService.GetMathching(allForums, byCountry);
            result = forumService.GetMathching(restult1, byCity);
            var forumsToGrid = from forum in result
                               select new
                               {
                                   Country = forumService.GetLocation(forum.id)[0],
                                   City = forumService.GetLocation(forum.id)[1],
                                   IfClosed = forum.isClosed ? new String("Closed") : new String("Opened"),
                                   IfUseful = userService.IsForumSuperUseful(forum) ? new String("Super Useful!") : new String("-"),

                               };
            ForumsGrid = forumsToGrid;
        }

    }
}
