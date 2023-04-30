using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Definitions.Charts;
using InitialProject.WPF.ViewModels.OwnerViewModels;

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AccommodationsMonthlyStatisticsView.xaml
    /// </summary>
    public partial class AccommodationsMonthlyStatisticsView : UserControl
    {
        public AccommodationsMonthlyStatisticsView()
        {
            InitializeComponent();
            this.DataContext = new AccommodationMonthlyStatisticsViewModel();
        }
    }
}
