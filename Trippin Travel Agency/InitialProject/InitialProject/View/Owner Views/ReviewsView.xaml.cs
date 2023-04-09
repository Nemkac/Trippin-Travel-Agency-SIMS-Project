using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for ReviewsView.xaml
    /// </summary>
    public partial class ReviewsView : UserControl
    {

        public ReviewsView()
        {
            InitializeComponent();
            GuestRateService guestRateService = new GuestRateService();
            List<AccommodationRate> dataGridData = ShowReviews();

            var reviewsData = from review in dataGridData
                              select new
                              {
                                  review.bookingId,
                                  review.cleanness,
                                  review.ownerRate,
                                  review.comment
                              };

            ReviewsDataGrid.ItemsSource = reviewsData;

            List<AccommodationRate> availableRates = ShowReviews();
            decimal totalRating = guestRateService.CalculateTotalRating(availableRates);
            TotalRateTextBlock.Text = totalRating.ToString();
        }

        public List<AccommodationRate> ShowReviews()
        {
            DataBaseContext accommodationRateContext = new DataBaseContext();
            DataBaseContext guestRateContext = new DataBaseContext();
            GuestRateService guestRateService = new GuestRateService();

            List<AccommodationRate> accommodationRates = accommodationRateContext.AccommodationRates.ToList();
            List<GuestRate> guestRates = guestRateContext.GuestRate.ToList();

            List<AccommodationRate> ratesForDisplay = new List<AccommodationRate>();

            guestRateService.FormDisplayableRates(accommodationRates, guestRates, ratesForDisplay);

            return ratesForDisplay;
        }
    }
}
