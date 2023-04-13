using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for TourConfirmationView.xaml
    /// </summary>
    public partial class TourConfirmationView : UserControl
    {
        public TourConfirmationView()
        {
            InitializeComponent();
            this.Loaded += WindowLoaded;
        }

        public void WindowLoaded(object sender, RoutedEventArgs e) 
        {

            ShowCoupons();
            DataBaseContext context = new DataBaseContext();
            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.First();
            this.TourNameLabel.Content = tourBookingTransfer.name;
            this.CityLabel.Content = tourBookingTransfer.cityLocation;
            this.CountryLabel.Content = tourBookingTransfer.countryLocation;
            this.KeyPointsLabel.Content = tourBookingTransfer.keypoints;
            this.GuestNumberLabel.Content = tourBookingTransfer.numberOfGuests;
            this.DurationLabel.Content = tourBookingTransfer.hoursDuration;
            
        }

        private void FinishBooking(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            CouponDTO? selectedCoupon = this.CouponsDataGrid.SelectedItem as CouponDTO;

            foreach (Coupon coupon in context.Coupons.ToList()) {
                if (selectedCoupon != null && coupon.id == selectedCoupon.id) { 
                    context.Coupons.Remove(coupon);
                }
            }

            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.First();
            Tour tour = context.Tours.SingleOrDefault(t => t.id == tourBookingTransfer.id);
            tour.touristLimit -= tourBookingTransfer.numberOfGuests;
            TourReservation tourReservation = new TourReservation(LoggedUser.id, tour.id, tourBookingTransfer.numberOfGuests);


            context.TourReservations.Add(tourReservation);
            context.SaveChanges();
            context.tourBookingTransfers.Remove(tourBookingTransfer);
            context.SaveChanges();

            ShowCoupons();
            this.CancelBookingButton.IsEnabled = false;
            this.FinishBookingButton.IsEnabled = false;
        }
        public void ShowCoupons() {

            DataBaseContext context = new DataBaseContext();
            List<Coupon> coupons = context.Coupons.ToList();

            List<CouponDTO> dataList = new List<CouponDTO>();
            int counter = 0;
            foreach (Coupon coup in coupons)
            {
                if (coup.userId == LoggedUser.id)
                {
                    counter += 1;
                    dataList.Add(new CouponDTO(coup.id, "Coupon" + counter, coup.exiresOn));
                }
            }
            this.CouponsDataGrid.ItemsSource = dataList;
        }

        private void CancelBooking(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            TourBookingTransfer tourBookingTransfer = context.tourBookingTransfers.First();
            context.tourBookingTransfers.Remove(tourBookingTransfer);
            context.SaveChanges();
            this.FinishBookingButton.IsEnabled = false;
            this.CancelBookingButton.IsEnabled = false;

        }
    }
}
