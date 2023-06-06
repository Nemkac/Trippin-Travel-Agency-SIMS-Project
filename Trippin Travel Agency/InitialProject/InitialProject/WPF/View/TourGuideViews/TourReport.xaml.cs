using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.WPF.View.TourGuideViews
{
    public partial class TourReport : UserControl
    {
        private readonly TourService tourService;
        private readonly DataBaseContext dbContext;
        public ObservableCollection<TourAttendance> TourAttendances { get; set; }

        public TourReport(int tourId)
        {
            InitializeComponent();
            this.tourService = new(new TourRepository());
            this.dbContext = new DataBaseContext();
            /*this.TourAttendances = new ObservableCollection<TourAttendance>(
            dbContext.TourAttendances
                .Select(ta => new
                {
                    TourAttendance = ta,
                    GuestName = GetGuestName(ta.guestID)
                })
                .ToList()
                .Select(x =>
                {
                    x.TourAttendance.guestID = x.GuestName;
                    return x.TourAttendance;
                }));*/

            this.DataContext = this;
            //this.DataContext = new TourReport
            LoadData(tourId);
        }

        private void LoadData(int tourId)
        {
            Tour tour = this.tourService.GetById(tourId);
            this.tourNameTextBlock.Text = tour.name;
            this.tourIdTextBlock.Text = tour.id.ToString();
            this.tourDescriptionTextBlock.Text = tour.description;
            this.tourTouristLimitTextBlock.Text = tour.touristLimit.ToString();
            this.tourHoursDurationTextBlock.Text = tour.hoursDuration.ToString();
            this.tourStartDatesTextBlock.Text = tour.startDates.ToString();

            using (var dbContext = new DataBaseContext())
            {
                var attendanceList = dbContext.TourAttendances.ToList();

                var attendanceViewList = attendanceList.Select(ta => new
                {
                    ta.id,
                    ta.tourId,
                    ta.keyPointId, 
                    ta.guestID, 
                    ta.numberOfGuests, 
                    ta.checkedForCoupon,

                    GuestName = GetGuestName(ta.guestID)
                }).ToList();

                attendanceDataGrid.ItemsSource = attendanceViewList;
            }
        }
        public string GetGuestName(int guestId)
        {
            using (var dbContext = new DataBaseContext())
            {
                var user = dbContext.Users.Find(guestId);
                return $"{user.firstName} {user.lastName}";
            }
        }

    }
}
