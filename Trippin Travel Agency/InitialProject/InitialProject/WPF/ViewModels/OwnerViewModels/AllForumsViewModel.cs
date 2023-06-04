using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AllForumsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;
        private AccommodationService accommodationService = new(new AccommodationRepository());

        public ObservableCollection<ForumItem> Forums { get; set; } = new ObservableCollection<ForumItem>();

        private ForumItem _selectedItem;
        public ForumItem SelectedItem
        {
            get { return _selectedItem; }

            set
            {
                _selectedItem = value;
                HandleSelections();
                OnPropertyChanged(nameof(SelectedItem));
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

        public AllForumsViewModel() 
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            DisplayAllForums();
            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }

        private void DisplayAllForums()
        {
            AccommodationService accommodationService = new(new AccommodationRepository());
            AccommodationLocationService accommodationLocationService = new AccommodationLocationService();
            List<Accommodation> ownersAccommodations = accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);

            DataBaseContext forumsContext = new DataBaseContext();
            List<Forum> forums = forumsContext.Forums.ToList();

            List<ForumComment> comments = forumsContext.ForumComments.ToList();

            foreach (Forum forum in forums)
            {
                int forumLocationId = accommodationService.GetAccommodationLocationIdByForumId(forum.id);
                List<string> forumLocation = accommodationService.GetAccommodationLocationCityAndCountryByForumId(forum.id);
                int numberOfComments = 0;
                int numberOfUserComments = 0;
                int numberOfOwnerComments = 0;

                foreach (ForumComment comment in comments)
                {
                    if(comment.forumId == forum.id)
                    {
                        numberOfComments++;

                        if(comment.userIcon == "User")
                        {
                            numberOfUserComments++;
                        }
                        if(comment.userIcon == "Location")
                        {
                            numberOfOwnerComments++;
                        }
                    }
                }

                foreach (Accommodation accommodation in ownersAccommodations)
                {
                    int accommodationLocationId = accommodationService.GetAccommodationLocationId(accommodation.id);
                    if (forumLocationId == accommodationLocationId)
                    {

                        bool forumExists = Forums.Any(f => f.ForumId == $"Forum {forum.id}");

                        if (!forumExists)
                        {
                            if(numberOfOwnerComments >= 2 && numberOfUserComments >= 1)
                            {
                                ForumItem item = new ForumItem
                                {
                                    ForumId = $"Forum {forum.id}",
                                    Location = $"Location: {forumLocation[1]}, {forumLocation[0]}",
                                    NoOfComments = $"Number of comments: {numberOfComments}",
                                    ForumType = "VERY USEFUL FORUM"
                                };
                                Forums.Add(item);
                            }
                            else
                            {
                                ForumItem item = new ForumItem
                                {
                                    ForumId = $"Forum {forum.id}",
                                    Location = $"Location: {forumLocation[1]}, {forumLocation[0]}",
                                    NoOfComments = $"Number of comments: {numberOfComments}"
                                };
                                Forums.Add(item);
                            }
                        }
                    }
                }
            }
        }

        private void HandleSelections()
        {
            string forum = SelectedItem.ForumId;
            int forumId;

            string forumIdString = forum.Replace("Forum ", "");
            if (int.TryParse(forumIdString, out forumId))
            {
                LoggedUser.VisitedForumId = forumId;
                ShowForumView(null);
            }
        }

        public void ShowForumView(object obj)
        {
            _mainViewModel.ExecuteShowForumCommand(obj);
        }
    }
}

public class ForumItem
{
    public string ForumId { get; set; }
    public string Location { get; set; }
    public string NoOfComments { get; set; }
    public string ForumType { get; set; }
}
