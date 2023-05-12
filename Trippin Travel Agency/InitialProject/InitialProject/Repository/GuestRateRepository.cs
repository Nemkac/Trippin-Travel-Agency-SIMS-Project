using InitialProject.Context;
using InitialProject.Interfaces;
using InitialProject.Model;
using InitialProject.Service.BookingServices;
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

        public List<GuestRate> GetGuestsRates()
        {   
            DataBaseContext context = new DataBaseContext();
            List<GuestRate> rates = context.GuestRate.ToList(); 
            BookingRepository bookingRepository = new BookingRepository();
            BookingService bookingService = new BookingService(bookingRepository);
            List<GuestRate> foundRates = new List<GuestRate> ();
            foreach(GuestRate guestRate in rates)
            {
                if (bookingService.GetById(guestRate.bookingId).guestId == LoggedUser.id)
                {
                    foundRates.Add(guestRate);
                }
            }
            return foundRates;
        }
    }
}
