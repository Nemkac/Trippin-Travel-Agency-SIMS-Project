using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class BookingCancelationMessageService
    {
        public static void Save(BookingCancelationMessage bookingCancelationMessage)
        {
            DataBaseContext saveContext = new DataBaseContext();
            saveContext.Attach(bookingCancelationMessage);
            saveContext.SaveChanges();
        }
    }
}
