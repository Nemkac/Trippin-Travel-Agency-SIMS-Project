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
using System.Windows;

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


        private Style _dataGridStyle;
        public Style DataGridStyle
        {
            get { return _dataGridStyle; }
            set
            {
                _dataGridStyle = value;
                OnPropertyChanged(nameof(DataGridStyle));
            }
        }

        private Style _dataGridCellStyle;
        public Style DataGridCellStyle
        {
            get { return _dataGridCellStyle; }
            set
            {
                _dataGridCellStyle = value;
                OnPropertyChanged(nameof(DataGridCellStyle));
            }
        }
        private Style _dataGridColumnHeaderStyle;
        public Style DataGridColumnHeaderStyle
        {
            get { return _dataGridColumnHeaderStyle; }
            set
            {
                _dataGridColumnHeaderStyle = value;
                OnPropertyChanged(nameof(DataGridColumnHeaderStyle));
            }
        }

        private Style _dataGridRowStyle;
        public Style DataGridRowStyle
        {
            get { return _dataGridRowStyle; }
            set
            {
                _dataGridRowStyle = value;
                OnPropertyChanged(nameof(DataGridRowStyle));
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

            DataGridStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";

            DataGridStyle = isChecked ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = isChecked ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = isChecked ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = isChecked ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
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
