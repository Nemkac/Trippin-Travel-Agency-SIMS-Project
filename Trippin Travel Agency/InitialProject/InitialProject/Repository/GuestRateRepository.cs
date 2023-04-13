using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class GuestRateRepository : IGuestRateRepository
    {
        public GuestRateRepository() { }

        public void Save(GuestRate guestRate)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(guestRate);
            saveContext.SaveChanges();
        }

        public bool IsGuestRated(int bookingId)
        {
            DataBaseContext ratedBookingContext = new DataBaseContext();
            List<GuestRate> ratedGuests = ratedBookingContext.GuestRate.ToList();
            foreach (GuestRate guestRate in ratedGuests.ToList())
            {
                if (guestRate.bookingId == bookingId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
