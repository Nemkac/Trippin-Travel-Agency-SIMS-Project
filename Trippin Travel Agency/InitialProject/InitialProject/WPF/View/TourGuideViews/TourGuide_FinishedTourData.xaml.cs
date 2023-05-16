using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.GuestServices;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_FinishedTourData : UserControl
    {
        private readonly TourService tourService;
        private readonly MessageService messageService = new MessageService();
        private readonly TourReviewService tourReviewService = new TourReviewService();
        private readonly UserService userService = new UserService();

        public TourGuide_FinishedTourData()
        {
            InitializeComponent();
            this.Loaded += tourDataLoaded;
            this.tourService = new(new TourRepository());
            this.DataContext = new TourGuide_FinishedToursViewModel(); 
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = this.tourService.GetById(requests.Last().tourId);
            List<TourAttendance> attendances = tourService.GetTourAttendances(tour.id);
            double under18Count = 0;
            double between18and50Count = 0;
            double above50Count = 0;
            int withVoucherCount = 0;
            int withoutVoucherCount = 0;
            foreach (TourAttendance attendance in attendances)
            {
                int age = userService.GetById(attendance.guestID).age;
                if (age < 18)
                {
                    under18Count++;
                }
                else if (age >= 18 && age <= 50)
                {
                    between18and50Count++;
                }
                else
                {
                    above50Count++;
                }
            }
            VoucherPossession(tour, context, attendances, ref withVoucherCount, ref withoutVoucherCount);
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "With voucher",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(withVoucherCount) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Without voucher",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(withoutVoucherCount) },
                    DataLabels = true
                }
            };

            YearsCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Number of people by age",
                    Values = new ChartValues<double>{ under18Count, between18and50Count, above50Count }
                }
            };
            BarLabels = new[] { "18-", "18 - 50", "50+" };
            Formatter = value => value.ToString();
            DataContext = this; 
        }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection YearsCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("Current value: " + chartPoint.Y + "(" + (chartPoint.Participation * 100).ToString() + "%" + ")");
        }
        public void tourDataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = this.tourService.GetById(requests.Last().tourId);
            TourMessage message = messageService.GetByTourId(tour.id);
            this.headerTextBlock.Text = tour.name;
            List<TourAndGuideRateDTO> finishedTourReviewDtos = new List<TourAndGuideRateDTO>();

            List<TourAndGuideRate> reviews = tourService.GetTourRatingsById(tour.id);
            foreach (TourAndGuideRate tr in reviews)
            {
                finishedTourReviewDtos.Add(tourReviewService.transformTourReviewToDTO(tr, message.keyPointId));
            }
            this.reviewsDataGrid.ItemsSource = finishedTourReviewDtos;

            UpdateAttendanceSummary(tour, context);
        }

        private void UpdateAttendanceSummary(Tour tour, DataBaseContext context)
        {
            List<TourAttendance> attendances = tourService.GetTourAttendances(tour.id);

            int withVoucherCount = 0;
            int withoutVoucherCount = 0;
            VoucherPossession(tour, context, attendances, ref withVoucherCount, ref withoutVoucherCount);
            double withVoucherPercentage, withoutVoucherPercentage;
            ConvertToPercentages(withVoucherCount, withoutVoucherCount, out withVoucherPercentage, out withoutVoucherPercentage);
        }

        private static void ConvertToPercentages(int withVoucherCount, int withoutVoucherCount, out double withVoucherPercentage, out double withoutVoucherPercentage)
        {
            int totalCount = withVoucherCount + withoutVoucherCount;
            withVoucherPercentage = totalCount > 0 ? (double)withVoucherCount / totalCount * 100 : 0;
            withoutVoucherPercentage = totalCount > 0 ? (double)withoutVoucherCount / totalCount * 100 : 0;
        }

        private static void VoucherPossession(Tour tour, DataBaseContext context, List<TourAttendance> attendances, ref int withVoucherCount, ref int withoutVoucherCount)
        {
            foreach (TourAttendance attendance in attendances)
            {
                int guestId = attendance.guestID;
                bool hasVoucher = context.TourReservations
                    .Where(tr => tr.guestId == guestId && tr.tourId == tour.id)
                    .Select(tr => tr.withVoucher)
                    .FirstOrDefault();
                if (hasVoucher)
                {
                    withVoucherCount++;
                }
                else
                {
                    withoutVoucherCount++;
                }
            }
        }

        public void report_ButtonClick(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            List<TourLiveViewTransfer> requests = context.TourLiveViewTransfers.ToList();
            Tour tour = this.tourService.GetById(requests.Last().tourId);
            var selectedReview = (TourAndGuideRateDTO)reviewsDataGrid.SelectedItem;
            TourAndGuideRate tagr = tourReviewService.GetById(selectedReview.id);
            tagr.valid = false; 
            context.TourAndGuideRates.Update(tagr);
            context.SaveChanges();
            RefreshReviews(tour);
        }

        private void RefreshReviews(Tour tour)
        {
            List<TourAndGuideRateDTO> dtos = new List<TourAndGuideRateDTO>();
            TourMessage message = messageService.GetByTourId(tour.id);
            List<TourAndGuideRate> reviews = tourReviewService.GetReviewsById(tour.id);
            foreach (TourAndGuideRate tr in reviews)
            {
                dtos.Add(tourReviewService.transformTourReviewToDTO(tr, message.keyPointId));
            }
            reviewsDataGrid.ItemsSource = dtos;
        }



    }
}
