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
using InitialProject.WPF.ViewModels.GuestOneViewModels;


namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for GenerateReportInterface.xaml
    /// </summary>
    public partial class GenerateReportInterface : Window
    {
        public GenerateReportInterface()
        {
            InitializeComponent();
            this.DataContext = new GenerateReportViewModel();
            GuestOneStaticHelper.generateReportInterface = this;
        }
    }
}
