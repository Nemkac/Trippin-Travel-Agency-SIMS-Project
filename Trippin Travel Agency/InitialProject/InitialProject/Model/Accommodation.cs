﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;


namespace InitialProject.Model
{
    public enum Type
    {
        Apartment,
        House,
        Hut
    }
    public class Accommodation
    {
        public int id { get; set; }
        public string name { get; set; }
        public AccommodationLocation location { get; set; }
        public int guestLimit { get; set; }
        public int minDaysBooked { get; set; }
        public int bookingCancelPeriodDays { get; set; }
        public Type type { get; set; }
        public List<Image> imageLinks { get; set; }
        public Accommodation() { }

        public Accommodation(string name, AccommodationLocation location, int guestLimit, int minDaysBooked, int bookingCancelPeriodDays, Type type, List<Image> imageLinks)
        {
            this.name = name;
            this.location = location;
            this.guestLimit = guestLimit;
            this.minDaysBooked = minDaysBooked;
            this.bookingCancelPeriodDays = bookingCancelPeriodDays;
            this.type = type;
            this.imageLinks = imageLinks;
        }


    }
}
