﻿using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Model;


namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RateAccommodationInterface.xaml
    /// </summary>
    public partial class RateAccommodationInterface : Window
    {
        public int bookingId;
        public RateAccommodationInterface()
        {
            InitializeComponent();
        }

        public void SetAttributes(int bookingId)
        {
            this.bookingId = bookingId;
        }
        
        private void LeaveReview(object sender, RoutedEventArgs e)
        {
            AccommodationRateService accommodationRateService = new AccommodationRateService();
            AccommodationRate accommodationRate = new AccommodationRate(bookingId, Convert.ToInt32(cleannesInput.Value), Convert.ToInt32(ownerRateInput.Value), commentInput.Text);
            AccommodationRateService.Save(accommodationRate);        
        }
    }
}