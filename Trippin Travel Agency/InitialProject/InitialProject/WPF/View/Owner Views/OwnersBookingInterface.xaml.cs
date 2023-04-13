using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.Owner_Views
{

    public partial class OwnersBookingInterface : UserControl
    {
        private readonly BookingService bookingService;
        private GuestRateService guestRateService;
        public OwnersBookingInterface()
        {
            InitializeComponent();
            List<BookingDTO> dataGridData = ShowBookings();
            bookingDataGrid.ItemsSource = dataGridData;
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            this.guestRateService = new(new GuestRateRepository());
        }

        private List<BookingDTO> ShowBookings()
        {
            //GuestRateService guestRateService = new GuestRateService();
            DataBaseContext bookingContext = new DataBaseContext();
            List<BookingDTO> dataList = new List<BookingDTO>();
            BookingService bookingService = new BookingService(new BookingRepository());
            BookingDTO dto = new BookingDTO();

            foreach (Booking booking in bookingContext.Bookings.ToList())
            {
                dto = bookingService.CreateBookingDTO(booking);
                dataList.Add(dto);
            }

            return dataList;
        }
    }
}
