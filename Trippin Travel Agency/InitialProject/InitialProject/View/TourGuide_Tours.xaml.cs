using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_Tours : UserControl
    {
        public TourGuide_Tours()
        {
            InitializeComponent();
            FillYearComboBox();
        }

        private void FillYearComboBox()
        {
            for (int year = 2015; year <= 2023; year++)
            {
                yearComboBox.Items.Add(year.ToString());
            }
            yearComboBox.Items.Add("All time");
        }

        private void ShowTourStatistics()
        {
            int? selectedYear = GetSelectedYear();

            var mostVisitedTour = GetMostVisitedTour(selectedYear);

            // Update the tour statistics labels
            if (mostVisitedTour != null)
            {
                statisticsTourName.Content = mostVisitedTour.name;
                statisticsVisitedBy.Content = $"{mostVisitedTour.touristLimit} guests";
                statisticsDate.Content = mostVisitedTour.startDates.ToString("MMM dd, yyyy");
            }
            else
            {
                statisticsTourName.Content = "N/A";
                statisticsVisitedBy.Content = "N/A";
                statisticsDate.Content = "N/A";
            }
        }

        private int? GetSelectedYear()
        {
            int? selectedYear = null;
            if (yearComboBox.SelectedItem != null && yearComboBox.SelectedItem.ToString() != "All time")
            {
                selectedYear = int.Parse(yearComboBox.SelectedItem.ToString());
            }

            return selectedYear;
        }

        private Tour GetMostVisitedTour(int? year)
        {
            using (var db = new DataBaseContext())
            {
                var query = from tour in db.Tours
                            join attendance in db.TourAttendances on tour.id equals attendance.tourId
                            where year == null || tour.startDates.Year == year
                            group attendance by tour into g
                            orderby g.Sum(a => a.numberOfGuests) descending
                            select new
                            {
                                Tour = g.Key,
                                TotalGuests = g.Sum(a => a.numberOfGuests)
                            };

                var result = query.FirstOrDefault();
                return result?.Tour;
            }
        }

        private void ShowTourStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTourStatistics();
        }
    }
}
