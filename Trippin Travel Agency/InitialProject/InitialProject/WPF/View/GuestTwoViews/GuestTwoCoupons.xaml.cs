using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.WPF.ViewModels.GuestTwoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for GuestTwoCoupons.xaml
    /// </summary>
    public partial class GuestTwoCoupons : UserControl
    {
        public GuestTwoCoupons()
        {
            InitializeComponent();
            this.DataContext = new GuestTwoCouponsViewModel();
        }
    }     
}
