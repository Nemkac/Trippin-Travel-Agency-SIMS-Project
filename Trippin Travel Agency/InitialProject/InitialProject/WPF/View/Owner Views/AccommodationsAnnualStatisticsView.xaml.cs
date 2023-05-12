using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using InitialProject.WPF.ViewModels.OwnerViewModels;

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AccommodationsAnnualStatisticsView.xaml
    /// </summary>
    public partial class AccommodationsAnnualStatisticsView : UserControl
    {

        public AccommodationsAnnualStatisticsView()
        {
            InitializeComponent();
            this.DataContext = new AccommodationAnnualStatisticsViewModel();
        }
    }
}
