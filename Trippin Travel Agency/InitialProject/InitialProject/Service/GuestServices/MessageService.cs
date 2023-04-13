using InitialProject.Context;
using InitialProject.Model;
using InitialProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InitialProject.Service.GuestServices
{
    public class MessageService
    {
        public MessageService() { }

        public TourMessage GetByTourId(int tourid)
        {
            using DataBaseContext dbContext = new DataBaseContext();
            return dbContext.TourMessages.SingleOrDefault(m => m.tourId == tourid);
        }
    }
}
