using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for GuestsReviewsInterface.xaml
    /// </summary>
    public partial class GuestsReviewsInterface : Window
    {
        private BookingService bookingService;
        private AccommodationService accommodationService;
        private AccommodationRateService accommodationRateService;
        private GuestRateService guestRateService;
        public GuestsReviewsInterface()
        {
            InitializeComponent();
            this.Loaded += ShowGuestsReviews;
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            this.accommodationService = new AccommodationService(accommodationRepository);
        }

        public void ShowGuestsReviews(object sender, RoutedEventArgs e)
        {
            GuestRateRepository guestRateRepository = new GuestRateRepository();
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            this.accommodationService = new AccommodationService(accommodationRepository);
            this.guestRateService = new GuestRateService(guestRateRepository);
            List<GuestRate> guestsRates = guestRateService.GetGuestRates();

            var guestsRatesToGrid = from guestRate in guestsRates
                                    select new
                                    {
                                        bookingId = guestRate.bookingId,
                                        accommodationName = accommodationService.GetById((bookingService.GetById(guestRate.bookingId)).accommodationId).name,
                                        test = new String("testtest")
                                      };
            this.GuestsReviewsGrid.ItemsSource = guestsRatesToGrid;
        }
    }
}
