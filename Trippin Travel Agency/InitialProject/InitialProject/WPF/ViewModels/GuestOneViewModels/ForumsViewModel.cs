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
        private ForumService forumService { get; set; }

        public ForumsViewModel()
        {
            forumService = new ForumService();
            ForumText = "Forum is a great place where you can get to know a lot about certain place.\n" +
                "Thousands of people every day share their opinion and advices.\n" +
                "You can create your own forum or open an existing one.";
            GoMyForums = new ViewModelCommand(GoToMyForums);
            ForumsGrid = new ObservableCollection<Forum>(forumService.GetAll());

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

        public void GoToMyForums(object sender)
        {
            GuestsForumsInterface guestsForumsInterface = new GuestsForumsInterface();
            guestsForumsInterface.Left = GuestOneStaticHelper.guestOneInterface.Left;
            guestsForumsInterface.Top = GuestOneStaticHelper.guestOneInterface.Top;
            guestsForumsInterface.Show();
            GuestOneStaticHelper.forumsInterface.Hide();
        }

    }
}
