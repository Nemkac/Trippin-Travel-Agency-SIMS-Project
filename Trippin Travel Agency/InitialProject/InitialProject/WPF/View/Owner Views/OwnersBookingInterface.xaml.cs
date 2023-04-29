using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.ViewModels.OwnerViewModels;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.Owner_Views
{

    public partial class OwnersBookingInterface : UserControl
    {
        public OwnersBookingInterface()
        {
            InitializeComponent();
            this.DataContext = new OwnersBookingDisplayViewModel();
        }
    }
}
