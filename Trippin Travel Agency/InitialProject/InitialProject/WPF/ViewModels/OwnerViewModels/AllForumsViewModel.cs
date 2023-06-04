using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
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

        public ObservableCollection<string> Forums { get; set; } = new ObservableCollection<string>();

        private string _forumNo;
        public string ForumNo
        {
            get { return _forumNo; }
            set 
            { 
                _forumNo = value;
                OnPropertyChanged(nameof(ForumNo)); 
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public AllForumsViewModel() 
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            DisplayAllForums();
        }

        private void DisplayAllForums()
        {
            AccommodationService accommodationService = new(new AccommodationRepository());
            AccommodationLocationService accommodationLocationService = new AccommodationLocationService();
            List<Accommodation> ownersAccommodations = accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);

            DataBaseContext forumsContext = new DataBaseContext();
            List<Forum> forums = forumsContext.Forums.ToList();

            foreach (Forum forum in forums)
            {
                int forumLocationId = accommodationService.GetAccommodationLocationIdByForumId(forum.id);
                foreach (Accommodation accommodation in ownersAccommodations)
                {
                    int accommodationLocationId = accommodationService.GetAccommodationLocationId(accommodation.id);
                    if(forumLocationId == accommodationLocationId)
                    {
                        string forumListBoxItem = $"Forum {forum.id}\nLocation: {forum.location}";
                        Forums.Add(forumListBoxItem);
                    }
                }
            }
        }
    }
}
