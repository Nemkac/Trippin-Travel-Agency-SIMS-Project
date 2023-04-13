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
    public class BookingDelaymentRequestRepository : IBookingDelaymentRequestRepository
    {
        public BookingDelaymentRequestRepository() { }
        public BookingDelaymentRequest GetById(int bookingDelaymentRequstId)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.BookingDelaymentRequests.SingleOrDefault(b => b.id == bookingDelaymentRequstId);
        }
        public void Save(BookingDelaymentRequest bookingDelaymentRequest)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(bookingDelaymentRequest);
            saveContext.SaveChanges();
        }
        public void Delete(BookingDelaymentRequest bookingDelaymentRequest)
        {
            DataBaseContext context = new DataBaseContext();
            context.Remove(bookingDelaymentRequest);
            context.SaveChanges();
        }
    }
}
