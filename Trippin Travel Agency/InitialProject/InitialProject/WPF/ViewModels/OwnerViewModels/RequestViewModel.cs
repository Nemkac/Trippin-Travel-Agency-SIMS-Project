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

        private string _contentHintColor;
        public string ContentHintColor
        {
            get { return _contentHintColor; }
            set
            {
                _contentHintColor = value;
                OnPropertyChanged(nameof(ContentHintColor));
            }
        }

        private string _detailsButtonColor;
        public string DetailsButtonColor
        {
            get { return _detailsButtonColor; }
            set
            {
                _detailsButtonColor = value;
                OnPropertyChanged(nameof(DetailsButtonColor));
            }
        }


        private Style _dataGridStyle;
        public Style DataGridStyle
        {
            get { return _dataGridStyle; }
            set
            {
                _dataGridStyle = value;
                OnPropertyChanged(nameof(DataGridStyle));
            }
        }

        private Style _dataGridCellStyle;
        public Style DataGridCellStyle
        {
            get { return _dataGridCellStyle; }
            set
            {
                _dataGridCellStyle = value;
                OnPropertyChanged(nameof(DataGridCellStyle));
            }
        }
        private Style _dataGridColumnHeaderStyle;
        public Style DataGridColumnHeaderStyle
        {
            get { return _dataGridColumnHeaderStyle; }
            set
            {
                _dataGridColumnHeaderStyle = value;
                OnPropertyChanged(nameof(DataGridColumnHeaderStyle));
            }
        }

        private Style _dataGridRowStyle;
        public Style DataGridRowStyle
        {
            get { return _dataGridRowStyle; }
            set
            {
                _dataGridRowStyle = value;
                OnPropertyChanged(nameof(DataGridRowStyle));
            }
        }


        public RequestViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowAcceptDenyViewCommand = new ViewModelCommand(ShowAcceptDenyView);
            ShowRequests();

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";
            DetailsButtonColor = Mediator.GetCurrentIsChecked() ? "#718093" : "#2f3640";

            DataGridStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
            DetailsButtonColor = isChecked ? "#718093" : "#2f3640";

            DataGridStyle = isChecked ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = isChecked ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = isChecked ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = isChecked ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        public void ShowAcceptDenyView(object obj)
        {
            if (SelectedRequest != null)
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
            else MessageBox.Show("You must select an request before proceeding to request regulation!");
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
