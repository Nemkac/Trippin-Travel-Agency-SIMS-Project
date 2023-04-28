using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
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

            if (startDate.HasValue && endDate.HasValue)
            {
                filteredRequests = filteredRequests.Where(r =>
                    (startDate.Value >= r.startDate && startDate.Value <= r.endDate) ||
                    (endDate.Value >= r.startDate && endDate.Value <= r.endDate) ||
                    (r.startDate >= startDate.Value && r.startDate <= endDate.Value) ||
                    (r.endDate >= startDate.Value && r.endDate <= endDate.Value));

                var maxEndDate = filteredRequests.Select(r => r.endDate).Max();
                if (maxEndDate < endingDateDatePicker.SelectedDate)
                {
                    MessageBox.Show("Selected end date is after tour request's end date.");
                    endingDateDatePicker.SelectedDate = null;
                    return;
                }

                var minStartDate = filteredRequests.Select(r => r.startDate).Min();
                if (minStartDate > startingDateDatePicker.SelectedDate)
                {
                    MessageBox.Show("Selected start date is before tour request's start date.");
                    startingDateDatePicker.SelectedDate = null;
                    return;
                }
            }

            tourRequestsDataGrid.ItemsSource = filteredRequests.ToList();

        }
        private void FilterParametersChanged(object sender, EventArgs e)
        {
            FilterData();
        }
        public void acceptTourRequest_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (tourRequestsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select a request to proceed");
                return;
            }
            else
            {
                TourRequest request = tourRequestsDataGrid.SelectedItem as TourRequest;
                DataBaseContext context = new DataBaseContext();
                AcceptedTourRequestViewTransfer accepted = new AcceptedTourRequestViewTransfer(request.id);
                context.AcceptedTourRequestViewTransfers.Add(accepted);
                context.SaveChanges();
            }
        }

    }





}
