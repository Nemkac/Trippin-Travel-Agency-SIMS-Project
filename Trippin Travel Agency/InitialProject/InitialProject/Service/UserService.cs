using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class UserService
    {
        public User GetById(int id)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Users.SingleOrDefault(u => u.id == id);
        }

        public List<Booking> GetAllGuestsBookings(int id)
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
            foreach(Booking booking in bookings.ToList())
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
            AccommodationService accommodationService = new AccommodationService();
            bool ifDelayable = false;
            foreach (Booking booking in bookings.ToList())
            {
                ifDelayable = (accommodationService.GetById(booking.accommodationId)).bookingCancelPeriodDays <= (DateTime.Parse(booking.arrival)).Subtract(DateTime.Today).Days;
                if (booking.guestId == id && ifDelayable)
                {
                    delayableBookings.Add(booking);
                }
            }
            return delayableBookings;
        }

    }
}
