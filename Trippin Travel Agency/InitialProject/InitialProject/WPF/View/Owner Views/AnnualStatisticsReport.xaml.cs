using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
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

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AnnualStatisticsReport.xaml
    /// </summary>
    public partial class AnnualStatisticsReport : UserControl
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private BookingService bookingService = new(new BookingRepository());
        public AnnualStatisticsReport(int selectedYear)
        {
            InitializeComponent();
            DataBaseContext dataBaseContext = new DataBaseContext();
            AnnualAccommodationTransfer transferedAccommodation = dataBaseContext.AccommodationAnnualStatisticsTransfer.First();
            Accommodation accommodation = this.accommodationService.GetById(transferedAccommodation.accommodationId);
            AccommodationNameTB1.Text = transferedAccommodation.accommodationName;
            OwnerTB.Text = LoggedUser.firstName + " " + LoggedUser.lastName;
            EmailTB.Text = LoggedUser.email;
            AccommodationNameTB2.Text = transferedAccommodation.accommodationName;
            TypeTB.Text = accommodation.type.ToString();
            LocationTB.Text = transferedAccommodation.location;
            GuestLimitTB.Text = transferedAccommodation.maxNumOfGuests.ToString();
            YearTB.Text = selectedYear.ToString();

            List<Booking> bookings = bookingService.GetAllBookings();
            List<CanceledBooking> canceledBookings = bookingService.GetAllCanceledBookings();
            List<DelayedBookings> delayedBookings = bookingService.GetAllDelayedBookings();

            int numberOfBookings = 0;
            int numberOfCancelations = 0;
            int numberOfDelayments = 0;

            numberOfBookings = bookingService.CountAccommodationsBookingsForYear(selectedYear, numberOfBookings, transferedAccommodation, bookings);

            numberOfCancelations = bookingService.CountAccommodationsCanceledBookingsForYear(selectedYear, numberOfCancelations, transferedAccommodation, canceledBookings);

            numberOfDelayments = bookingService.CountAccommodationsDelayedBookingsForYear(selectedYear, numberOfDelayments, transferedAccommodation, delayedBookings);

            Bookings.Text = numberOfBookings.ToString();
            Cancelations.Text = numberOfCancelations.ToString();
            Delayments.Text = numberOfDelayments.ToString();

            DataBaseContext rateContext = new DataBaseContext();
            List<AccommodationRate> rates = rateContext.AccommodationRates.ToList();
            decimal avgRate = 0;
            int ratesSum = 0;
            int countRates = 0;

            foreach(AccommodationRate rate in rates)
            {
                Booking booking = this.bookingService.GetById(rate.bookingId);
                Accommodation acc = this.accommodationService.GetById(booking.accommodationId);
                if (acc.name == transferedAccommodation.accommodationName)
                {
                    ratesSum += rate.cleanness;
                    countRates++;
                }
            }

            if(ratesSum != 0 && countRates != 0) 
            {
                avgRate = ratesSum / countRates;
            }
            else
            {
                avgRate = 0;
            }

            AvgRate.Text = avgRate.ToString();
        }
    }
}
