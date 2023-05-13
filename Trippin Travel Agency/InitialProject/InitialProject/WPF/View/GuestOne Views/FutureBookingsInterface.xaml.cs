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
            debuger.Text = GuestOneStaticHelper.temp;
            GuestOneStaticHelper.futureBookingsInterface = this;
            this.DataContext = new FutureBookingsViewModel();
        }
    }
}
