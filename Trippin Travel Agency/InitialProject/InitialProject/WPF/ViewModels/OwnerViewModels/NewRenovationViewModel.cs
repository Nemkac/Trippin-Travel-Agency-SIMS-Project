using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class NewRenovationViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ObservableCollection<AccommodationStatisticsDTO> ownerBookings { get; set; } = new ObservableCollection<AccommodationStatisticsDTO>();

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

        public ViewModelCommand ScheduleNewRenovationView { get; private set; }

        public NewRenovationViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            ScheduleNewRenovationView = new ViewModelCommand(ShowScheduleNewRenovationView);
            ShowOwnersAccommodations();
        }

        public void ShowScheduleNewRenovationView(object obj)
        {
            SelectedAccommodations.selectedAccommodationForRenovation = SelectedAccommodation;
            _mainViewModel.ExecuteShowScheduleNewRenovationCommand(null);
        }

        private void ShowOwnersAccommodations()
        {
            AccommodationStatisticsDTO dto;
            List<AccommodationStatisticsDTO> accommodationsToShow = new List<AccommodationStatisticsDTO>();
            List<Accommodation> ownersAccommodations = accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);
            foreach (Accommodation accommodation in ownersAccommodations)
            {
                dto = this.accommodationService.CreateAccommodationStatisticsDTO(accommodation);
                accommodationsToShow.Add(dto);
            }

            foreach(AccommodationStatisticsDTO asDto in accommodationsToShow)
            {
                ownerBookings.Add(asDto);
            }
        }
    }
}
