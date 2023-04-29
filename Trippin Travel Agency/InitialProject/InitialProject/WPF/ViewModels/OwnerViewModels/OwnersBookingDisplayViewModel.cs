using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    class OwnersBookingDisplayViewModel : ViewModelBase
    {
        public ObservableCollection<BookingDTO> bookings { get; set; } = new ObservableCollection<BookingDTO>(); 
        public OwnersBookingDisplayViewModel() 
        {
            ShowBookings();
        }

        private void ShowBookings()
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
            
            foreach(BookingDTO booking in dataList)
            {
                bookings.Add(booking);
            }
        }
    }
}
