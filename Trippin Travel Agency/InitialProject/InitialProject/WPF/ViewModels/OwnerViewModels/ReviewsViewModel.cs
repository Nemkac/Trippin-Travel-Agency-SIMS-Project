using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ReviewsViewModel : ViewModelBase
    {
        private GuestRateService guestRateService = new(new GuestRateRepository());
        public ObservableCollection<AccommodationRate> rates { get; set; } = new ObservableCollection<AccommodationRate>();

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

        public ReviewsViewModel() 
        {
            ShowReviews();
            List<AccommodationRate> availableRates = GetAllReviews();
            decimal totalRating = guestRateService.CalculateTotalRating(availableRates);
            TotalRating = totalRating;

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }

        public void ShowReviews()
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            DataBaseContext guestRateContext = new DataBaseContext();

            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            List<GuestRate> guestRates = guestRateContext.GuestRate.ToList();

            List<AccommodationRate> ratesForDisplay = new List<AccommodationRate>();

            guestRateService.FormDisplayableRates(accommodationRates, guestRates, ratesForDisplay);

            foreach(AccommodationRate rate in ratesForDisplay)
            {
                rates.Add(rate);
            }
        }

        public List<AccommodationRate> GetAllReviews()
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            DataBaseContext guestRateContext = new DataBaseContext();

            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            List<GuestRate> guestRates = guestRateContext.GuestRate.ToList();

            List<AccommodationRate> ratesForDisplay = new List<AccommodationRate>();

            guestRateService.FormDisplayableRates(accommodationRates, guestRates, ratesForDisplay);

            return ratesForDisplay;
        }
    }
}
