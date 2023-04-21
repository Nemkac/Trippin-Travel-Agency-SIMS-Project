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
    public partial class TourGuide_FullTourRequests : UserControl
    {
        private TourRequestService tourRequestService;
        public TourGuide_FullTourRequests()
        {
            InitializeComponent();
            this.tourRequestService = new(new TourRepository());
            List<TourRequest> requests = tourRequestService.GetAllFullTourRequests();
            tourRequestsDataGrid.ItemsSource = requests;
        }
        private void FilterData()
        {
            string country = tourCountryTextBox.Text;
            string city = tourCityTextBox.Text;
            string numOfPeople = tourRequestCountryComboBox.Text;
            DateTime? startDate = startingDateDatePicker.SelectedDate;
            DateTime? endDate = endingDateDatePicker.SelectedDate;
            string language = tourRequestCityComboBox.Text;

            IEnumerable<TourRequest> filteredRequests = tourRequestService.GetAllFullTourRequests();

            if (!string.IsNullOrWhiteSpace(country))
            {
                filteredRequests = filteredRequests.Where(r => r.country.StartsWith(country, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                filteredRequests = filteredRequests.Where(r => r.city.StartsWith(city, StringComparison.InvariantCultureIgnoreCase));
            }


            if (!string.IsNullOrWhiteSpace(numOfPeople))
            {
                filteredRequests = filteredRequests.Where(r => r.numberOfTourists.ToString().StartsWith(numOfPeople));
            }

            if (!string.IsNullOrWhiteSpace(language))
            {
                filteredRequests = filteredRequests.Where(r => r.language.ToString().StartsWith(language, StringComparison.InvariantCultureIgnoreCase));
            }

            if (startDate.HasValue)
                filteredRequests = filteredRequests.Where(r => r.startDate >= startDate.Value);

            if (endDate.HasValue)
                filteredRequests = filteredRequests.Where(r => r.endDate <= endDate.Value);

            tourRequestsDataGrid.ItemsSource = filteredRequests.ToList();
        }





        private void FilterParametersChanged(object sender, EventArgs e)
        {
            FilterData();
        }
    }
    

    


}
