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

        public List<Booking> GetBookings(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<Booking> bookings = context.Bookings.ToList();
            List<Booking> foundBookings = new List<Booking>();
            foreach (Booking booking in bookings.ToList())
            {
                if (booking.guestId == id)
                {
                    foundBookings.Add(booking);
                }
            }
            return foundBookings;
        }
    }
}
