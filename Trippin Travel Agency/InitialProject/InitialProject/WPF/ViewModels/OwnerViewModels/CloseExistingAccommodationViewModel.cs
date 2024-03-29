﻿using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class CloseExistingAccommodationViewModel : ViewModelBase
    {

        private readonly OwnerInterfaceViewModel _mainViewModel;
        private AccommodationService accommodationService = new(new AccommodationRepository());
        public ObservableCollection<AccommodationStatisticsDTO> accommodations { get; set; } = new ObservableCollection<AccommodationStatisticsDTO>();


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

        private string _hintText;

        public string HintText
        {
            get { return _hintText; }
            set
            {
                _hintText = value;
                OnPropertyChanged(nameof(HintText));
            }
        }

        private string _closeAccommodationText;

        public string CloseAccommodationText
        {
            get { return _closeAccommodationText; }
            set
            {
                _closeAccommodationText = value;
                OnPropertyChanged(nameof(CloseAccommodationText));
            }
        }


        public CloseExistingAccommodationViewModel()
        {
            this._mainViewModel = LoggedUser._mainViewModel;
            Mediator.IsCheckedChanged += OnIsCheckedChanged;
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;
            ShowOwnersAccommodations();

            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";
            DataGridStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];
            DataGridCellStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];
            DataGridColumnHeaderStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];
            DataGridRowStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];

            HintText = Mediator.GetCurrentIsLanguageChecked() ? "Izaberite smestaj i kliknite dugme ispod kako bi videli slike ili godisnju statistiku" :
                                                                "Choose an accommodation and click a button beneath to see accommodation images or annual statistics";
            CloseAccommodationText = Mediator.GetCurrentIsLanguageChecked() ? "Zatvori smestaj" : "Close accommodation";
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            HintText = isChecked ? "Izaberite smestaj i kliknite dugme ispod kako bi ga zatvorili" :
                                                    "Choose an accommodation and click a button beneath to close it.";
            CloseAccommodationText = isChecked ? "Zatvori smestaj" : "Close accommodation";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";
            DataGridStyle = isChecked ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];
            DataGridCellStyle = isChecked ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];
            DataGridColumnHeaderStyle = isChecked ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];
            DataGridRowStyle = isChecked ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        public void ShowOwnersAccommodations()
        {
            AccommodationStatisticsDTO dto;
            List<AccommodationStatisticsDTO> accommodationsToShow = new List<AccommodationStatisticsDTO>();
            List<Accommodation> ownersAccommodations = this.accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);

            string leastPopularLocation = LoggedUser.LeastPopularCountry + ", " + LoggedUser.LeastPopularCity;

            foreach (Accommodation accommodation in ownersAccommodations)
            {
                dto = this.accommodationService.CreateAccommodationStatisticsDTO(accommodation);
                accommodationsToShow.Add(dto);
            }

            foreach (AccommodationStatisticsDTO asDto in accommodationsToShow)
            {
                if (asDto.location == leastPopularLocation)
                {
                    accommodations.Add(asDto);

                }
            }
        }

    }
}
