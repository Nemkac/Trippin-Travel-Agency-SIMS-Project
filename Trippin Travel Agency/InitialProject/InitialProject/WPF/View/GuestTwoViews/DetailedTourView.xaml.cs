using InitialProject.Context;
using InitialProject.Model.TransferModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for DetailedTourView.xaml
    /// </summary>
    public partial class DetailedTourView : UserControl
    {
        public int TourId { get; set;  } 
        public DetailedTourView()
        {
            InitializeComponent();
            this.Loaded += WindowLoaded;
    
        }

        public void WindowLoaded(object sender, RoutedEventArgs e) {

            DataBaseContext context = new DataBaseContext();
            List<DetailedTourViewTransfer> requests = context.detailedTourViewTransfers.ToList();
            this.TextBlock.Text = requests.Last().tourId.ToString();
           
            
        }
    }
}
