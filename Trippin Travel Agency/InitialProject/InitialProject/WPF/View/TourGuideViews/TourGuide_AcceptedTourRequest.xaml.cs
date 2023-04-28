using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.View.TourGuideViews
{
    public partial class TourGuide_AcceptedTourRequest : UserControl
    {
        private readonly TourService tourService = new(new TourRepository());
        public TourGuide_AcceptedTourRequest()
        {
            InitializeComponent();
            this.Loaded += requestDataLoaded;
        }
        
        public void requestDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context;
            TourRequest tr;
            GetExact(out context, out tr);
            startingDateLabel.Content = tr.startDate.ToString("yyyy-MM-dd");
            endingDateLabel.Content = tr.endDate.ToString("yyyy-MM-dd");

        }

        private void GetExact(out DataBaseContext context, out TourRequest tr)
        {
            context = new DataBaseContext();
            List<AcceptedTourRequestViewTransfer> acceptedrequests = context.AcceptedTourRequestViewTransfers.ToList();
            tr = this.tourService.GetRequestById(acceptedrequests.Last().requestId);
        }

        private void acceptRequest_ButtonClick(object sender, RoutedEventArgs e)
        {
            DataBaseContext context;
            TourRequest tr;
            GetExact(out context, out tr);

            DateTime selectedDate = tourCalendar.SelectedDate ?? DateTime.MinValue;
            if (selectedDate < tr.startDate || selectedDate > tr.endDate)
            {
                MessageBox.Show("Selected date is not within the tour request's range of dates.");
                return;
            }

            bool hasConflictingTours = context.Tours
                .Any(t => t.startDates == selectedDate);

            if (hasConflictingTours)
            {
                MessageBox.Show("There is already a tour scheduled for the selected date.");
                return;
            }

            tr.status = TourRequestStatus.Accepted;
            tr.acceptedDate = selectedDate;
            context.TourRequests.Update(tr);
            RequestMessage rm = new RequestMessage(tr.id, tr.guestId);
            context.RequestMessages.Add(rm);
            context.SaveChanges();
            MessageBox.Show("Tour accepted.");
        }


    }
}
