using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.ViewModels.GuestOneViewModels;
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

namespace InitialProject.WPF.View.GuestOne_Views
{

    public partial class SendBookingDelaymentInterface : Window
    {
        public SendBookingDelaymentInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.sendBookingDelaymentInterface = this;
            this.DataContext = new SendBookingDelaymentViewModel();
        }
    }
}
