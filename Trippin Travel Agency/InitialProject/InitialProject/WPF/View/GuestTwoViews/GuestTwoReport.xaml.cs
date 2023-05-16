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

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for GuestTwoReport.xaml
    /// </summary>
    public partial class GuestTwoReport : UserControl
    {
        public GuestTwoReport(string TourName, string CityName, string CountryName, string KeyPoints, int Duration, int GuestNumber)
        {
            InitializeComponent();
            this.TourName.Content = TourName;
            this.CityName.Content = CityName;
            this.CountryName.Content = CountryName;
            this.KeyPoints.Text = KeyPoints;
            this.Duration.Content = Duration.ToString();
            this.NumberOfGuests.Content = GuestNumber.ToString();
        }
        
    }
}
