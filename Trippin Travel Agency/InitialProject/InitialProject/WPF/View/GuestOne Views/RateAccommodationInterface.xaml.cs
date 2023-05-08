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
using System.Diagnostics;
using InitialProject.Context;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.WPF.ViewModels.GuestOneViewModels;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for RateAccommodationInterface.xaml
    /// </summary>
    public partial class RateAccommodationInterface : Window
    {
        public RateAccommodationInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.rateAccommodationInterface = this;
            this.DataContext = new RateAccommodationViewModel();
        }
    }
}
