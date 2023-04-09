﻿using InitialProject.Context;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
using Dapper;
using InitialProject.Model;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Configuration;

namespace InitialProject.View
{
    public partial class BookAccommodationInterface
    {

        public int accommodationId;
        public int userId;
        public BookAccommodationInterface()
        {
            InitializeComponent();
        }

        public void ShowBookings(dynamic result)
        {
            AccommodationService accommodationService = new AccommodationService();
            Accommodation accommodation = accommodationService.GetById(accommodationId);
            AccommodationRateService accommodationRateService = new AccommodationRateService();
            string accommodationInfoLabels = string.Empty;
            string accommodationInfo = string.Empty;
            accommodationInfoLabels = "Accommodation name:" + "\n\nCountry:" + "\n\nCity:" + "\n\nMaximum number of guests:" + "\n\nRating out of 10:";
            accommodationInfo = accommodation.name + "\n\n" + accommodationService.GetAccommodationLocation(accommodationId)[0] + "\n\n" + accommodationService.GetAccommodationLocation(accommodationId)[1] + "\n\n" + accommodation.guestLimit + "\n\n" + accommodationRateService.GetAccommodationAverageRate(accommodationId);
            dataGrid.ItemsSource = result;
            accommodationInfoLabelsBlock.Text = accommodationInfoLabels;
            accommodationInfoBlock.Text = accommodationInfo;

        }
        public void SetAttributes(int accommodationId, int userId)
        {
            this.accommodationId = accommodationId;
            this.userId = userId;
        }
        private void BookAccommodation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AccommodationService accommodationService;
            string arrival, departure, guestsNumber;
            GetBasicAccommodationBookingProperties(out accommodationService, out arrival, out departure, out guestsNumber);
            if (int.Parse(guestsNumber) > accommodationService.GetById(accommodationId).guestLimit)
            {
                warningText.Text = accommodationService.GetById(accommodationId).name + " cannot take more then " + accommodationService.GetById(accommodationId).guestLimit.ToString() + " guests.";
            }
            else
            {
                Book(accommodationService, arrival, departure, guestsNumber);
            }

        }

        private void Book(AccommodationService accommodationService, string arrival, string departure, string guestsNumber)
        {
            Booking booking = new Booking(accommodationId, arrival, departure, (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days, userId);
            Service.BookingService.Save(booking);
        }

        private void GetBasicAccommodationBookingProperties(out AccommodationService accommodationService, out string arrival, out string departure, out string guestsNumber)
        {
            accommodationService = new AccommodationService();
            string selectedDate = dataGrid.SelectedItem.ToString();
            selectedDate = selectedDate.Substring(10, selectedDate.Length - 12);
            List<string> dates = selectedDate.Split("-").ToList();
            arrival = dates[0].Substring(0, dates[0].Length - 2);
            departure = dates[1].Substring(2, dates[1].Length - 2);
            guestsNumber = numberOfGuests.Text;
        }
    }
}