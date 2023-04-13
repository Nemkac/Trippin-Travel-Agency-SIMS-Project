using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels;
using SharpVectors.Dom;
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

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for RateGuestView.xaml
    /// </summary>
    public partial class RateGuestView : UserControl
    {
        GuestRateRepository rateRepository = new GuestRateRepository();
        private BookingService bookingService;

        public int bookingId { get; set; }
        public RateGuestView()
        {
            InitializeComponent();
            DataBaseContext ratingContext = new DataBaseContext();
            BookingTransfer transferedBooking = ratingContext.SelectedRatingNotificationTransfer.First();
            this.bookingId = transferedBooking.bookingId;
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
        }

        private void SaveRate(object sender, RoutedEventArgs e)
        {
            //BookingService bookingService = new BookingService();
            int cleannessRate = GetCleanness();
            int rulesRate = GetRulesRespecting();
            string comment = commentTB.Text;
            int guestId = bookingService.GetGuestId(this.bookingId);
            GuestRate newGuestRate = new GuestRate(cleannessRate, rulesRate, comment, guestId, this.bookingId);
            GuestRateService.Save(newGuestRate);
            DataBaseContext transferedBooking = new DataBaseContext();
            transferedBooking.SelectedRatingNotificationTransfer.Remove(transferedBooking.SelectedRatingNotificationTransfer.First());
            transferedBooking.SaveChanges();
            saveFeedback.Text = "Rating successfully saved!";
        }

        private int GetCleanness()
        {
            int cleannessRate;
            if (cleannesRateRadioButton1.IsChecked == true)
            {
                cleannessRate = 1;
            }
            else if (cleannesRateRadioButton2.IsChecked == true)
            {
                cleannessRate = 2;
            }
            else if (cleannesRateRadioButton3.IsChecked == true)
            {
                cleannessRate = 3;
            }
            else if (cleannesRateRadioButton4.IsChecked == true)
            {
                cleannessRate = 4;
            }
            else
            {
                cleannessRate = 5;
            }

            return cleannessRate;
        }

        private int GetRulesRespecting()
        {
            int rulesRate;
            if (rulesRateRadioButton1.IsChecked == true)
            {
                rulesRate = 1;
            }
            else if (rulesRateRadioButton2.IsChecked == true)
            {
                rulesRate = 2;
            }
            else if (rulesRateRadioButton3.IsChecked == true)
            {
                rulesRate = 3;
            }
            else if (rulesRateRadioButton4.IsChecked == true)
            {
                rulesRate = 4;
            }
            else
            {
                rulesRate = 5;
            }

            return rulesRate;
        }
    }
}
