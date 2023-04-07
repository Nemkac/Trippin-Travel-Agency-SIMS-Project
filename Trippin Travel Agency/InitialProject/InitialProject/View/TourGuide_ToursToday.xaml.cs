using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using InitialProject.ViewModels;
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
    public partial class TourGuide_ToursToday : UserControl
    {
        private readonly TourGuide_ToursTodayViewModel _viewModel;

        public TourGuide_ToursToday()
        {
            InitializeComponent();

            // Instantiate TourGuide_MainViewModel
            TourGuide_MainViewModel mainViewModel = new TourGuide_MainViewModel();

            DataContext = _viewModel = new TourGuide_ToursTodayViewModel(mainViewModel);

            TourService tourService = new TourService();
            List<Tour> toursToday = tourService.GetToursToday();

            // Select only the columns you want to show in the DataGrid
            var tourData = from tour in toursToday
                           select new
                           {
                               tour.id,
                               tour.name,
                               tour.language
                           };

            tourDataGrid.ItemsSource = tourData;

            tourDataGrid.SelectionChanged += TourDataGrid_SelectionChanged;
        }

        private void TourDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected tour from the DataGrid
            dynamic selectedTour = tourDataGrid.SelectedItem;
            if (selectedTour != null)
            {
                int selectedTourId = selectedTour.id;
                Tour selectedTourObject = new Tour { id = selectedTourId, name = selectedTour.name };
                DataBaseContext dbContext = new DataBaseContext();
                Tour tour = dbContext.Tours.FirstOrDefault(t => t.id == selectedTourId);
                _viewModel.SelectedTourName = selectedTour.name; // Update SelectedTourName property
            }
        }
    }
}
