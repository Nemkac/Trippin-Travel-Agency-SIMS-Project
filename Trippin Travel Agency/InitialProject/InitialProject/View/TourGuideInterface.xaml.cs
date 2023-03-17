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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuideInterface.xaml
    /// </summary>
    public partial class TourGuideInterface : Window
    {
        public TourGuideInterface()
        {
            InitializeComponent();
        }

        private void LeadCreateTour(object sender, RoutedEventArgs e)
        {
            TourInterface TourInterface = new TourInterface();
            TourInterface.Show(); 

        }
    }
}
