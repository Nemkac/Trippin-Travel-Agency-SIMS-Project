using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for Request.xaml
    /// </summary>
    public partial class Request : UserControl
    {
        private BookingService bookingService;
        public Request()
        {
            InitializeComponent();
            List<RequestDTO> requestDataGridData = ShowRequests();
            requestsDataGrid.ItemsSource = requestDataGridData;
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
        }

        private List<RequestDTO> ShowRequests()
        {
            BookingRepository bookingRepository = new BookingRepository();
            this.bookingService = new BookingService(bookingRepository);
            DataBaseContext requestContext = new DataBaseContext();
            List<RequestDTO> dataList = new List<RequestDTO>();
            RequestDTO dto = new RequestDTO();

            foreach (BookingDelaymentRequest bookingDelaymentRequest in requestContext.BookingDelaymentRequests.ToList())
            {
                if (bookingDelaymentRequest.status == Status.Pending)
                {
                    dto = this.bookingService.CreateRequestDTO(bookingDelaymentRequest);
                    dataList.Add(dto);
                }
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
