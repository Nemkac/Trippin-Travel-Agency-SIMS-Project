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
using InitialProject.Model;
using InitialProject.Context;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using System.Globalization;
using InitialProject.WPF.ViewModels.GuestOneViewModels;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for FutureBookingsInterface.xaml
    /// </summary>
    public partial class FutureBookingsInterface : Window
    {
        public FutureBookingsInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.futureBookingsInterface = this;
            this.DataContext = new FutureBookingsViewModel();
        }
    }
}
