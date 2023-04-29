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


        public ICommand ShowOurRecommendationsViewCommand { get; private set; }
        public ViewModelCommand ShowAnnualStatisticsViewCommand { get; private set; }

        public AccommodationStatisticsViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowOurRecommendationsViewCommand = new ViewModelCommand(ExecuteShowOurRecommendationsViewCommand);
            ShowAnnualStatisticsViewCommand = new ViewModelCommand(ShowAnnualStatistics);
            ShowOwnersAccommodations();
            ExecuteShowOurRecommendationsViewCommand(null);
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
