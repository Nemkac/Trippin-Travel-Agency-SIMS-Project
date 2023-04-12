using InitialProject.Context;
using InitialProject.DTO;
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

            //Brisanje eventualno postojeceg sadrzaja iz tabele pre prosledjivanja na drugi interfejs
            DataBaseContext transferContext = new DataBaseContext();
            List<TourStatisticsDTO> transfetTableData = transferContext.TourStatisticsTransfer.ToList();
            transferContext.TourStatisticsTransfer.RemoveRange(transfetTableData);
            transferContext.SaveChanges();

            DataBaseContext attendanceContext = new DataBaseContext();
            DataBaseContext yearsContext = new DataBaseContext();
            List<Tour> tours = yearsContext.Tours.ToList();
            List<TourAttendance> attendances = attendanceContext.TourAttendances.ToList();
            List<int> mostVisitedTours = new List<int>();


            TourStatisticsDTO statisticsToShow = new TourStatisticsDTO();
            if (!GetSelectedAllTime())
            {
                foreach (Tour tour in tours)
                {
                    if (tour.startDates.Year == selectedYear)
                    {
                        foreach (TourAttendance att in attendances)
                        {
                            if (tour.id == att.tourId)
                            {
                                mostVisitedTours.Add(att.numberOfGuests);
                            }
                        }
                    }
                }
                
                /*Treba resiti slucaj kada se selektuje godina za koju ne postoji tura. Kada se selektuje odredjena godina
                 prosledjuje se i tourId u transfer tabelu. */


                int maxAttendance = mostVisitedTours.Max();
                int maxTourId;

                statisticsToShow.numberOfGuests = maxAttendance;

                foreach (TourAttendance attendance in attendances)
                {
                    if (attendance.numberOfGuests == maxAttendance)
                    {
                        maxTourId = attendance.tourId;
                        statisticsToShow.tourId = maxTourId;
                        foreach (Tour tour in tours)
                        {
                            if (tour.id == maxTourId)
                            {
                                statisticsToShow.tourName = tour.name;
                                statisticsToShow.startDate = tour.startDates;
                            }
                        }
                    }
                }

                transferContext.TourStatisticsTransfer.Add(statisticsToShow);
                transferContext.SaveChanges();

            } else {
                foreach (Tour tour in tours)
                {
                    foreach (TourAttendance att in attendances)
                    {
                        mostVisitedTours.Add(att.numberOfGuests);
                    }
                }

                int maxAttendance = mostVisitedTours.Max();
                int maxTourId;
                statisticsToShow.numberOfGuests = maxAttendance;

                foreach (TourAttendance attendance in attendances)
                {
                    if (attendance.numberOfGuests == maxAttendance)
                    {
                        maxTourId = attendance.tourId;
                        statisticsToShow.tourId = maxTourId;
                        statisticsVisitedBy.Content = attendance.numberOfGuests;
                        foreach (Tour tour in tours)
                        {
                            if (tour.id == maxTourId)
                            {
                                statisticsToShow.tourName = tour.name;
                                statisticsToShow.startDate = tour.startDates;
                            }
                        }
                    }
                }

                transferContext.TourStatisticsTransfer.Add(statisticsToShow);
                transferContext.SaveChanges();
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

        private bool GetSelectedAllTime()
        {
            if(yearComboBox.SelectedItem.ToString() == "All time")
            {
                return true;
            }

            return false;
        }

        private void ShowTourStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTourStatistics();
        }
    }
}
