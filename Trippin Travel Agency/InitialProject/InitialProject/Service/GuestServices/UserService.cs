using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.GuestServices
{
    class UserService
    {
        private AccommodationService accommodationService;
        private BookingService bookingService;
        public UserService()
        {
            BookingRepository bookingRepository = new BookingRepository();
            bookingService = new BookingService(bookingRepository);
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            accommodationService = new AccommodationService(accommodationRepository);
        }

        public User GetById(int id)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Users.SingleOrDefault(u => u.id == id);
        }

        public List<Booking> GetGuestsBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> allBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id)
                {
                    allBookings.Add(booking);
                }
            }
            return allBookings;
        }

        public List<BookingDelaymentRequest> GetBookingDelaymentRequests(int id)
        {
            DataBaseContext context = new DataBaseContext();
            //BookingService bookingService = new BookingService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = context.BookingDelaymentRequests.ToList();
            List<BookingDelaymentRequest> foundBookingDelaymentRequests = new List<BookingDelaymentRequest>();
            foreach (BookingDelaymentRequest bookingDelaymentRequest in bookingDelaymentRequests)
            {
                if (bookingService.GetById(bookingDelaymentRequest.bookingId).guestId == id)
                {
                    foundBookingDelaymentRequests.Add(bookingDelaymentRequest);
                }
            }
            return foundBookingDelaymentRequests;
        }

        public List<BookingDelaymentRequest> GetResolvedBookingDelaymentRequests()
        {
            DataBaseContext context = new DataBaseContext();
            //BookingService bookingService = new BookingService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = context.BookingDelaymentRequests.ToList();
            List<BookingDelaymentRequest> foundRequests = new List<BookingDelaymentRequest>();
            foreach (BookingDelaymentRequest bookingDelaymentRequest in bookingDelaymentRequests)
            {
                if (bookingService.GetById(bookingDelaymentRequest.bookingId).guestId == LoggedUser.id && (bookingDelaymentRequest.status == Status.Accepted || bookingDelaymentRequest.status == Status.Denied))
                {
                    foundRequests.Add(bookingDelaymentRequest);
                }
            }
            if (foundRequests.Count > 0)
            {
                return foundRequests;
            }
            return null;
        }

        public List<BookingDelaymentRequest> GetPendingBookingDelaymentRequests()
        {
            DataBaseContext context = new DataBaseContext();
            //BookingService bookingService = new BookingService();
            List<BookingDelaymentRequest> bookingDelaymentRequests = context.BookingDelaymentRequests.ToList();
            List<BookingDelaymentRequest> foundRequests = new List<BookingDelaymentRequest>();
            foreach (BookingDelaymentRequest bookingDelaymentRequest in bookingDelaymentRequests)
            {
                if (bookingService.GetById(bookingDelaymentRequest.bookingId).guestId == LoggedUser.id && bookingDelaymentRequest.status == Status.Pending)
                {
                    foundRequests.Add(bookingDelaymentRequest);
                }
            }
            if (foundRequests.Count > 0)
            {
                return foundRequests;
            }
            return null;
        }


        public List<Booking> GetGuestsPastBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> pastBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id && DateTime.Parse(booking.departure) < DateTime.Today)
                {
                    pastBookings.Add(booking);
                }
            }
            return pastBookings;
        }

        public List<Booking> GetGuestsFutureBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> futureBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id && DateTime.Parse(booking.arrival) > DateTime.Today)
                {
                    futureBookings.Add(booking);
                }
            }
            return futureBookings;
        }

        public List<Booking> GetGuestsDelayableBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> delayableBookings = new List<Booking>();
            //AccommodationService accommodationService = new AccommodationService();
            bool ifDelayable = false;
            foreach (Booking booking in bookings.ToList())
            {
                ifDelayable = accommodationService.GetById(booking.accommodationId).bookingCancelPeriodDays <= DateTime.Parse(booking.arrival).Subtract(DateTime.Today).Days;
                if (booking.guestId == id && ifDelayable)
                {
                    delayableBookings.Add(booking);
                }
            }
            return delayableBookings;
        }

    }
}
