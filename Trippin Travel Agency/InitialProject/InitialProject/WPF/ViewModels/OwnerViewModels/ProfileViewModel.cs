using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private GuestRateService guestRateService = new(new GuestRateRepository());
        public ViewModelCommand ShowReviewsViewCommand { get; private set; }
        private readonly OwnerInterfaceViewModel _mainViewModel;

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set
            {
                if (firstname != value)
                {
                    firstname = value;
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private decimal totalRating;
        public decimal TotalRating
        {
            get { return totalRating; }
            set
            {
                if (totalRating != value)
                {
                    totalRating = value;
                    OnPropertyChanged(nameof(TotalRating));
                }
            }
        }

        private string ownerType;
        public string OwnerType
        {
            get { return ownerType; }
            set
            {
                if (ownerType != value)
                {
                    ownerType = value;
                    OnPropertyChanged(nameof(OwnerType));
                }
            }
        }

        public ProfileViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowReviewsViewCommand = new ViewModelCommand(ShowReviewsView);
            DisplayUserInformations();
            List<AccommodationRate> availableRates = GetAvailableRates();
            decimal totalRating = guestRateService.CalculateTotalRating(availableRates);
            TotalRating = totalRating;

            DisplayOwnerType(totalRating);
        }

        public void ShowReviewsView(object obj)
        {
            _mainViewModel.ExecuteShowReviewsViewCommand(null);
        }

        public void DisplayUserInformations()
        {
            Username = LoggedUser.username;
            Firstname = LoggedUser.firstName;
            Lastname = LoggedUser.lastName;
            Email = LoggedUser.email;
        }

        private void DisplayOwnerType(decimal totalRating)
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            int numOfRates = accommodationRates.Count;

            if (numOfRates >= 1)
            {
                if (totalRating < (decimal)9.5)
                {
                    OwnerType = "Owner";
                }
                else
                {
                    OwnerType = "Super - Owner";
                }
            }
            else
            {
                OwnerType = "Owner";
            }
        }

        private static List<AccommodationRate> GetAvailableRates()
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            DataBaseContext guestRateContext = new DataBaseContext();
            GuestRateService guestRateService = new(new GuestRateRepository());

            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            List<GuestRate> guestRates = guestRateContext.GuestRate.ToList();

            List<AccommodationRate> ratesForDisplay = new List<AccommodationRate>();
            guestRateService.FormDisplayableRates(accommodationRates, guestRates, ratesForDisplay);

            return ratesForDisplay;
        }
    }
}
