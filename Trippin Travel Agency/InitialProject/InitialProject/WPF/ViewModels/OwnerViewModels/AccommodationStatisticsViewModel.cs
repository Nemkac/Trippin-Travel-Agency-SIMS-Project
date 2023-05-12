using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;
        private AccommodationService accommodationService = new(new AccommodationRepository());
        public ObservableCollection<AccommodationStatisticsDTO> accommodations { get; set; } = new ObservableCollection<AccommodationStatisticsDTO>();
        private ViewModelBase _secondChildView;
        public ViewModelBase SecondChildView
        {
            get
            {
                return _secondChildView;
            }

            set
            {
                _secondChildView = value;
                OnPropertyChanged(nameof(SecondChildView));
            }
        }

        private AccommodationStatisticsDTO selectedAccommodation;
        public AccommodationStatisticsDTO SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                if (selectedAccommodation != value)
                {
                    selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
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

        private string _labelContent;
        public string LabelContent
        {
            get { return _labelContent; }
            set
            {
                _labelContent = value;
                OnPropertyChanged(nameof(LabelContent));
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

        public ICommand ShowOurRecommendationsViewCommand { get; private set; }
        public ViewModelCommand ShowAnnualStatisticsViewCommand { get; private set; }


        public AccommodationStatisticsViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowOurRecommendationsViewCommand = new ViewModelCommand(ExecuteShowOurRecommendationsViewCommand);
            ShowAnnualStatisticsViewCommand = new ViewModelCommand(ShowAnnualStatistics);
            ShowOwnersAccommodations();
            ExecuteShowOurRecommendationsViewCommand(null);

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

        public void ExecuteShowOurRecommendationsViewCommand(object obj)
        {
            SecondChildView = new OurRecommendationsViewModel();
        }

        public void ShowOwnersAccommodations()
        {
            AccommodationStatisticsDTO dto;
            List<AccommodationStatisticsDTO> accommodationsToShow = new List<AccommodationStatisticsDTO>();
            List<Accommodation> ownersAccommodations = this.accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);
            foreach (Accommodation accommodation in ownersAccommodations)
            {
                dto = this.accommodationService.CreateAccommodationStatisticsDTO(accommodation);
                accommodationsToShow.Add(dto);
            }

            foreach(AccommodationStatisticsDTO asDto in accommodationsToShow)
            {
                accommodations.Add(asDto);
            }
        }

        private void ShowAnnualStatistics(object obj)
        {
            if (SelectedAccommodation != null)
            {
                AccommodationStatisticsDTO? selectedAccommodation = SelectedAccommodation;
                DataBaseContext annualTransferContext = new DataBaseContext();
                DataBaseContext transferContext = new DataBaseContext();

                var transfers = transferContext.AccommodationAnnualStatisticsTransfer.ToList();
                transferContext.AccommodationAnnualStatisticsTransfer.RemoveRange(transfers);
                transferContext.SaveChanges();

                AnnualAccommodationTransfer accommodationToTransfer = new AnnualAccommodationTransfer(
                    selectedAccommodation.accommodationId, selectedAccommodation.accommodationName, selectedAccommodation.location, selectedAccommodation.guestLimit);

                annualTransferContext.AccommodationAnnualStatisticsTransfer.Add(accommodationToTransfer);
                annualTransferContext.SaveChanges();
                _mainViewModel.ExecuteShowAnnualStatisticsCommand(null);
            }
            else
            {
                LabelContent = "Please select an accommodation";
                RemoveWarning();
            }
        }

        private void RemoveWarning()
        {
            var timer = new DispatcherTimer();
            timer.Tick += (sender, args) =>
            {
                LabelContent = string.Empty;
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
        }
    }
}
