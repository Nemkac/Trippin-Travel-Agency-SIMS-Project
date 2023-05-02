using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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






        private string _dataGridColumnHeaderColor;
        public string DataGridColumnHeaderColor
        {
            get { return _dataGridColumnHeaderColor; }
            set
            {
                _dataGridColumnHeaderColor = value;
                OnPropertyChanged(nameof(DataGridColumnHeaderColor));
            }
        }

        private string _dataGridSelectionColor;
        public string DataGridSelectionColor
        {
            get { return _dataGridSelectionColor; }
            set
            {
                _dataGridSelectionColor = value;
                OnPropertyChanged(nameof(DataGridSelectionColor));
            }
        }

        private string _dataGridTextSelectionColor;
        public string DataGridTextSelectionColor
        {
            get { return _dataGridTextSelectionColor; }
            set
            {
                _dataGridTextSelectionColor = value;
                OnPropertyChanged(nameof(DataGridTextSelectionColor));
            }
        }

        private string _dataGridRowColor;
        public string DataGridRowColor
        {
            get { return _dataGridRowColor; }
            set
            {
                _dataGridRowColor = value;
                OnPropertyChanged(nameof(DataGridRowColor));
            }
        }

        private string _dataGridTextColor;
        public string DataGridTextColor
        {
            get { return _dataGridTextColor; }
            set
            {
                _dataGridTextColor = value;
                OnPropertyChanged(nameof(DataGridTextColor));
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

            DataGridColumnHeaderColor = Mediator.GetCurrentIsChecked() ? "#1e2226" : "#192a56";
            DataGridSelectionColor = Mediator.GetCurrentIsChecked() ? "#1e2226" : "#192a56";
            DataGridTextSelectionColor = Mediator.GetCurrentIsChecked() ? "#f4fff8" : "#f4fff8";
            DataGridTextColor = Mediator.GetCurrentIsChecked() ? "#f4fff8" : "#222528";
            DataGridRowColor = Mediator.GetCurrentIsChecked() ? "#2f3640" : "#FCFFFC";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
            DetailsButtonColor = isChecked ? "#718093" : "#2f3640";

            DataGridColumnHeaderColor = isChecked ? "#1e2226" : "#192a56";
            DataGridSelectionColor = isChecked ? "#1e2226" : "#192a56";
            DataGridTextSelectionColor = isChecked ? "#f4fff8" : "#f4fff8";
            DataGridTextColor = isChecked ? "#f4fff8" : "#222528";
            DataGridRowColor = isChecked ? "#2f3640" : "#FCFFFC";
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
    }
}
