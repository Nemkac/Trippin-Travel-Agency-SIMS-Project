﻿using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    public interface IBookingRepository
    {
        public Booking GetById(int bookingId);
        public void Delete(Booking booking);
        public int GetGuestId(int bookingId);
        public string GetGuestName(int bookingId);
        public void Save(Booking booking);
    }
}
