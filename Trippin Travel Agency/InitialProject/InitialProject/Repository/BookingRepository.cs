using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Service.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public BookingRepository() { }

        public List<Booking> GetAll()
        {
            DataBaseContext bookingContext = new DataBaseContext();
            List<Booking> bookings = bookingContext.Bookings.ToList();
            return bookings;
        }
        public Booking GetById(int bookingId)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Bookings.SingleOrDefault(b => b.Id == bookingId);
        }

        public void Delete(Booking booking)
        {
            DataBaseContext context = new DataBaseContext();
            context.Remove(booking);
            context.SaveChanges();
        }

        public int GetGuestId(int bookingId)
        {
            int guestId;
            DataBaseContext guestIdContext = new DataBaseContext();
            List<Booking> bookings = guestIdContext.Bookings.ToList();

            foreach (Booking booking in bookings.ToList())
            {
                if (booking.Id == bookingId)
                {
                    guestId = booking.guestId;
                    return guestId;
                }
            }
            return -1;
        }

        public string GetGuestName(int bookingId)
        {
            UserService userService = new UserService();
            DataBaseContext bookingContext = new DataBaseContext();
            List<Booking> bookings = bookingContext.Bookings.ToList();
            User user = new User();

            foreach (Booking booking in bookings.ToList())
            {
                if (booking.Id == bookingId)
                {
                    user = userService.GetById(booking.guestId);
                }
            }

            return user.username;
        }
        public void Save(Booking booking)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(booking);
            saveContext.SaveChanges();
        }
        public List<CanceledBooking> GetAllCanceledBookings()
        {
            DataBaseContext canceledContext = new DataBaseContext();
            List<CanceledBooking> canceledBookings = canceledContext.CanceledBookings.ToList();
            return canceledBookings;
        }
        public List<DelayedBookings> GetAllDelayedBookings()
        {
            DataBaseContext delayedContext = new DataBaseContext();
            List<DelayedBookings> delayedBookings = delayedContext.DelayedBookings.ToList();
            return delayedBookings;
        }

        public bool HasGuestRated(int bookingId)
        {
            DataBaseContext context = new DataBaseContext();
            List<AccommodationRate> accommodationRates = context.AccommodationRates.ToList();
            foreach(AccommodationRate rate in accommodationRates)
            {
                if(bookingId == rate.bookingId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
