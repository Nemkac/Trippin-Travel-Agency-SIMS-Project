using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.WPF.View.TourGuideViews
{
    public partial class TourGuide_SpecificTourPart : UserControl
    {
        private DataBaseContext _context;

        public TourGuide_SpecificTourPart()
        {
            InitializeComponent();
            _context = new DataBaseContext();
            this.Loaded += requestDataLoaded;
            headerTextBlock.Text = TourGuide_RequestTimeSlots.selectedRequestId.ToString();
            this.DataContext = this;
        }

        public void requestDataLoaded(object sender, RoutedEventArgs e)
        {
            TourRequest tr = GetTourRequestById(TourGuide_RequestTimeSlots.selectedRequestId);
            if (tr != null)
            {
                startingDateLabel.Content = tr.startDate.ToString("yyyy-MM-dd");
                endingDateLabel.Content = tr.endDate.ToString("yyyy-MM-dd");
                ShowGuideAvailability(LoggedUser.id, tr.startDate, tr.endDate);
            }
        }

        private void ShowGuideAvailability(int guideId, DateTime startDate, DateTime endDate)
        {
            // Fetch the guide's busy dates
            var busyDates = _context.TourGuideBusyDates
                .Where(b => b.UserId == guideId && b.BusyDate >= startDate && b.BusyDate <= endDate)
                .Select(b => b.BusyDate)
                .ToList();

            // Create a date range
            var dateRange = Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                .Select(i => startDate.AddDays(i));

            // Filter the dates that the guide is available (not in the busy dates)
            var availableDates = dateRange.Except(busyDates);

            // Add the available dates to the list box
            foreach (var date in availableDates)
            {
                dateList.Items.Add(date.ToString("yyyy-MM-dd"));
            }
        }

        private void acceptRequest_ButtonClick(object sender, RoutedEventArgs e)
        {
            // Assuming the date selected in the list box is the date for the request
            if (dateList.SelectedItem != null)
            {
                DateTime selectedDate = DateTime.Parse(dateList.SelectedItem.ToString());
                TourRequest tr = GetTourRequestById(TourGuide_RequestTimeSlots.selectedRequestId); 
                // Create a new TourGuideBusy record
                var newBusyDate = new TourGuideBusy
                {
                    UserId = LoggedUser.id,
                    BusyDate = selectedDate
                };

                bool hasConflictingTours = _context.Tours
                .Any(t => t.startDates == selectedDate);

                if (hasConflictingTours)
                {
                    MessageBox.Show("There is already a tour scheduled for the selected date.");
                    return;
                }
                tr.status = TourRequestStatus.Accepted;
                tr.acceptedDate = selectedDate;
                _context.TourRequests.Update(tr);
                _context.TourGuideBusyDates.Add(newBusyDate);
                _context.SaveChanges();
                MessageBox.Show("Tour accepted.");

                // Remove the selected date from the list box
                dateList.Items.Remove(dateList.SelectedItem);
            }
        }

        public TourRequest GetTourRequestById(int id)
        {
            return _context.TourRequests.Find(id);
        }
    }
}
