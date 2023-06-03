using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service.AccommodationServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class ForumsViewModel : ViewModelBase
    {
        public ViewModelCommand GoMyForums { get; set; }
        public ViewModelCommand ShowForum { get; set; }
        private ForumService forumService { get; set; }
        

        public ForumsViewModel()
        {
            forumService = new ForumService();
            ForumText = "Forum is a great place where you can get to know a lot about certain place.\n" +
                "Thousands of people every day share their opinion and advices.\n" +
                "You can create your own forum or open an existing one.";
            GoMyForums = new ViewModelCommand(GoToMyForums);
            ShowForum = new ViewModelCommand(ShowSelectedForum);

            // ne treba svi nego od tog foruma
            var forumsToGrid = from forum in forumService.GetAll()
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

    }
}
