using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GuestsReviewsViewModel : ViewModelBase
    {
        private BookingService bookingService;
        private AccommodationService accommodationService;
        private GuestRateService guestRateService;
        public ViewModelCommand OpenReview { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public GuestsReviewsViewModel()
        {
            this.bookingService = new BookingService(new BookingRepository());
            this.accommodationService = new AccommodationService(new AccommodationRepository());
            this.guestRateService = new GuestRateService(new GuestRateRepository());
            List<GuestRate> guestsRates = guestRateService.GetGuestRates();
            OpenReview = new ViewModelCommand(ShowReview);
            OpenNavigator = new ViewModelCommand(ShowNavigator);

            var guestsRatesToGrid = from guestRate in guestsRates
                                    select new
                                    {
                                        bookingId = guestRate.bookingId,
                                        accommodationName = accommodationService.GetById((bookingService.GetById(guestRate.bookingId)).accommodationId).name,
                                        test = GenerateFeedback(bookingService.HasGuestRated(guestRate.bookingId))
                                    };
            ReviewsGrid = guestsRatesToGrid;
            
        }

        public void ShowReview(object sender)
        {
            List<GuestRate> guestsRates = guestRateService.GetGuestRates();
            if (bookingService.HasGuestRated(guestsRates[selectedIndex].bookingId))
            {
                GuestOneStaticHelper.guestRate = guestsRates[selectedIndex];
                SelectedGuestReviewInterface selectedGuestReviewInterface = new SelectedGuestReviewInterface();
                selectedGuestReviewInterface.Left = GuestOneStaticHelper.guestsReviewsInterface.Left + (GuestOneStaticHelper.guestsReviewsInterface.Width - selectedGuestReviewInterface.Width) / 2;
                selectedGuestReviewInterface.Top = GuestOneStaticHelper.guestsReviewsInterface.Top + (GuestOneStaticHelper.guestsReviewsInterface.Height - selectedGuestReviewInterface.Height) / 2;
                GuestOneStaticHelper.guestsReviewsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                selectedGuestReviewInterface.Show();
            }
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.guestsReviewsInterface.Left + (GuestOneStaticHelper.guestsReviewsInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.guestsReviewsInterface.Top + (GuestOneStaticHelper.guestsReviewsInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.guestsReviewsInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private dynamic reviewsGrid;
        public dynamic ReviewsGrid
        {
            get { return reviewsGrid; }
            set
            {
                if (reviewsGrid != value)
                {
                    reviewsGrid = value;
                    OnPropertyChanged(nameof(ReviewsGrid));
                }
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }

        private dynamic selectedReview;
        public dynamic SelectedReview
        {
            get { return selectedReview; }
            set
            {
                if (selectedReview != value)
                {
                    selectedReview = value;
                    OnPropertyChanged(nameof(SelectedReview));
                }
            }
        }
         
        private string debuger;
        public string Debuger
        {
            get { return debuger; }
            set
            {
                if (debuger != value)
                {
                    debuger = value;
                    OnPropertyChanged(nameof(Debuger));
                }
            }
        }
 

        public string GenerateFeedback(bool ifRated)
        {
            if (ifRated)
            {
                return new string("Select then press ENTER to see review you got");
            }
            return new string("In order to see review, you must leave a review of your staying there");
        }
    }
}
