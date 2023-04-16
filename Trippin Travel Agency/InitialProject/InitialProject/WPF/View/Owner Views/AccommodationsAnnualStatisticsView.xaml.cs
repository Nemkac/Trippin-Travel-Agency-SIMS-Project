using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AccommodationsAnnualStatisticsView.xaml
    /// </summary>
    public partial class AccommodationsAnnualStatisticsView : UserControl
    {
        private BookingService bookingService;
        private AnnualAccommodationTransfer transferedAccommodation;
        private int numberOfBookings, numberOfCancelations, numberOfDelayments, numberOfRenovationSuggestions;
        public AccommodationsAnnualStatisticsView()
        {
            InitializeComponent();
            FillYearComboBox();
            DataBaseContext transferContext = new DataBaseContext();
            this.transferedAccommodation = transferContext.AccommodationAnnualStatisticsTransfer.First();
            this.accommodationNameTextBlock.Text = transferedAccommodation.accommodationName;
            this.bookingService = new(new BookingRepository());
        }

        private void FillYearComboBox()
        {
            for (int year = 2015; year <= 2023; year++)
            {
                yearComboBox.Items.Add(year.ToString());
            }
        }

        private void ShowTransferedAccommodationsStatistics()
        {
            List<int> yearList = new List<int>();
            for(int year = 2015; year <= 2023; year++)
            {
                yearList.Add(year);
            }

            List<Booking> bookings = this.bookingService.GetAllBookings();
            List<CanceledBooking> canceledBookings = this.bookingService.GetAllCanceledBookings();

            foreach(int year in yearList)
            {
                foreach(Booking booking in bookings)
                {
                    DateTime arrivalDate = DateTime.ParseExact(booking.arrival, "M/d/yyyy", CultureInfo.InvariantCulture);
                    if(year == arrivalDate.Year)
                    {
                        this.numberOfBookings++;
                    }
                }

                foreach(CanceledBooking canceledBooking in canceledBookings)
                {
                    Booking booking = this.bookingService.GetById(canceledBooking.bookingId);
                    //Treba proveriti arrival izvucenog bookinga itd itd.
                    //Booking kada se otkaze ne treba brisati iz tabele bookings vec treba staviti polje canceled u tabelu booking tipa bool.
                    //Ako je false prikazuje se i obradjuje ga guestOne, ako je true ja ovde racunam to a guestOne ga ne prikazuje.
                }
            }
        }
    }
}
