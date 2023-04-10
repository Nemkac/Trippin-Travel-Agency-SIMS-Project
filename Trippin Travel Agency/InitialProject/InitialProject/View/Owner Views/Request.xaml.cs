using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Service;
using InitialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for Request.xaml
    /// </summary>
    public partial class Request : UserControl
    {
        public Request()
        {
            InitializeComponent();
            List<RequestDTO> requestDataGridData = ShowRequests();
            requestsDataGrid.ItemsSource = requestDataGridData;
        }

        private List<RequestDTO> ShowRequests()
        {
            BookingService bookingService = new BookingService();
            DataBaseContext requestContext = new DataBaseContext();
            List<RequestDTO> dataList = new List<RequestDTO>();
            RequestDTO dto = new RequestDTO();

            foreach (BookingDelaymentRequest bookingDelaymentRequest in requestContext.BookingDelaymentRequests.ToList())
            {
                dto = bookingService.CreateRequestDTO(bookingDelaymentRequest);
                dataList.Add(dto);

            }
            
            return dataList;
        }

        private void GetSelection(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = requestsDataGrid.SelectedItem as RequestDTO;

        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {
            RequestDTO? selectedRequest = this.requestsDataGrid.SelectedItem as RequestDTO;
            DataBaseContext requestContext = new DataBaseContext();
            DataBaseContext transferContext = new DataBaseContext();

            var transfers = transferContext.SelectedRequestTransfers.ToList();
            transferContext.SelectedRequestTransfers.RemoveRange(transfers);
            transferContext.SaveChanges();

            requestContext.SelectedRequestTransfers.Add(selectedRequest);
            requestContext.SaveChanges();
        }
    }
}
