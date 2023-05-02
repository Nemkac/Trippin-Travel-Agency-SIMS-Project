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

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
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

        public ViewModelCommand ScheduleNewRenovationView { get; private set; }

        public NewRenovationViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            ScheduleNewRenovationView = new ViewModelCommand(ShowScheduleNewRenovationView);
            ShowOwnersAccommodations();

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
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
