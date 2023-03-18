using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Services
{
    class GuestRateService
    {
        public static void Save(GuestRate guestRate)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(guestRate);
            saveContext.SaveChanges();
            MessageBox.Show("Guest rating successful!");
        }

        public bool IsRated(int bookingId)
        {
            DataBaseContext ratedBookingContext = new DataBaseContext();
            List<GuestRate> ratedGuests = ratedBookingContext.GuestRate.ToList();
            foreach(GuestRate guestRate in ratedGuests.ToList())
            {
                if(guestRate.bookingId == bookingId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
