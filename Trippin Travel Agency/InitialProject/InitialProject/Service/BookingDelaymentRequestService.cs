using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class BookingDelaymentRequestService
    {
        public static void Save(BookingDelaymentRequest bookingDelaymentRequest)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(bookingDelaymentRequest);
            saveContext.SaveChanges();
        }
    }
}
