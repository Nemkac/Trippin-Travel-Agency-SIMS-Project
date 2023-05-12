using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
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
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.ViewModels.GuestOneViewModels;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for PastBookingsInterface.xaml
    /// </summary>
    public partial class PastBookingsInterface : Window
    {
        public PastBookingsInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.pastBookingsInterface = this;
            this.DataContext = new PastBookingsViewModel();
        }
    }
}