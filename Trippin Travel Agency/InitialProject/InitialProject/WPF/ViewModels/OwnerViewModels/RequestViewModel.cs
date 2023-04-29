using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.WPF.View.GuestOne_Views;
using InitialProject.WPF.View.Owner_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        private BookingService bookingService = new(new BookingRepository());

        public ViewModelCommand ShowAcceptDenyViewCommand { get; private set; }
        private readonly OwnerInterfaceViewModel _mainViewModel;
        public ObservableCollection<RequestDTO> requests { get; set; } = new ObservableCollection<RequestDTO>();


        private RequestDTO selectedRequest;
        public RequestDTO SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
                if (selectedRequest != value)
                {
                    selectedRequest = value;
                    OnPropertyChanged(nameof(SelectedRequest));
                }
            }
        }


        public RequestViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowAcceptDenyViewCommand = new ViewModelCommand(ShowAcceptDenyView);
            ShowRequests();
        }

        public void ShowAcceptDenyView(object obj)
        {
            RequestDTO? selectedRequest = SelectedRequest;
            DataBaseContext requestContext = new DataBaseContext();
            DataBaseContext transferContext = new DataBaseContext();

            var transfers = transferContext.SelectedRequestTransfers.ToList();
            transferContext.SelectedRequestTransfers.RemoveRange(transfers);
            transferContext.SaveChanges();

            requestContext.SelectedRequestTransfers.Add(selectedRequest);
            requestContext.SaveChanges();
            _mainViewModel.ExecuteShowAcceptDenyViewCommand(null);
        }

        private void ShowRequests()
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

            foreach (RequestDTO req in dataList)
            {
                requests.Add(req);
            }
        }
    }
}
