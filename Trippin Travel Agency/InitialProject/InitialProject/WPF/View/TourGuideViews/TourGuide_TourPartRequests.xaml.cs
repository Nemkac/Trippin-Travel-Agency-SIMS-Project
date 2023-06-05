using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_TourPartRequests : UserControl
    {
        public static int selectedComplexId;
        private TourService tourService;
        private TourRequestService tourRequestService;
        public TourGuide_TourPartRequests()
        {
            InitializeComponent();
            this.tourService = new(new TourRepository());
            this.tourRequestService = new(new TourRepository());
            List<TourRequest> requests = tourRequestService.GetAllFullTourRequests();
            DataBaseContext context = new DataBaseContext();
            tourRequestsDataGrid.ItemsSource = context.ComplexTourRequests.ToList();           
            
        }
        public void NewWindow(object sender,RoutedEventArgs e)
        {
            ComplexTourRequest temp = this.tourRequestsDataGrid.SelectedItem as ComplexTourRequest;
            selectedComplexId = temp.id;

        }
    }
}
