﻿using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{

    public partial class OwnersBookingInterface : UserControl
    {
        public OwnersBookingInterface()
        {
            InitializeComponent();
            List<BookingDTO> dataGridData = ShowBookings();
            bookingDataGrid.ItemsSource = dataGridData;
        }

        private List<BookingDTO> ShowBookings()
        {
            GuestRateService guestRateService = new GuestRateService();
            BookingService bookingService = new BookingService();
            DataBaseContext bookingContext = new DataBaseContext();
            List<BookingDTO> dataList = new List<BookingDTO>();
            BookingDTO dto = new BookingDTO();

            foreach (Booking booking in bookingContext.Bookings.ToList())
            {
                dto = bookingService.CreateBookingDTO(booking);
                dataList.Add(dto);
            }

            return dataList;
        }
    }
}
