using InitialProject.Model;
using InitialProject.Service;
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
    /// <summary>
    /// Interaction logic for DelaymentRequestUpdate.xaml
    /// </summary>
    public partial class DelaymentRequestUpdate : Window
    {
        BookingDelaymentRequest bookingDelaymentRequest = new BookingDelaymentRequest();
        public DelaymentRequestUpdate()
        {
            InitializeComponent();
        }

        public void SetAttributes(BookingDelaymentRequest bookingDelaymentRequest)
        {
            this.bookingDelaymentRequest = bookingDelaymentRequest;
        }
    }
}
