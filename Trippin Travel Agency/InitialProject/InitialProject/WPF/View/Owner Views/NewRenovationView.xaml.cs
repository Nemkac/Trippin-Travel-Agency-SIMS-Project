﻿using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace InitialProject.WPF.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for NewRenovationView.xaml
    /// </summary>
    public partial class NewRenovationView : UserControl
    {
        public AccommodationService accommodationService = new(new AccommodationRepository());
        public NewRenovationView()
        {
            InitializeComponent();
            List<AccommodationStatisticsDTO> dataGridData = ShowOwnersAccommodations();
            this.newRenovationDataGrid.ItemsSource = dataGridData;
        }

        private List<AccommodationStatisticsDTO> ShowOwnersAccommodations()
        {
            AccommodationStatisticsDTO dto;
            List<AccommodationStatisticsDTO> accommodationsToShow = new List<AccommodationStatisticsDTO>();
            List<Accommodation> ownersAccommodations = this.accommodationService.GetAccommodationsByOwnerId(LoggedUser.id);
            foreach (Accommodation accommodation in ownersAccommodations)
            {
                dto = this.accommodationService.CreateAccommodationStatisticsDTO(accommodation);
                accommodationsToShow.Add(dto);
            }

            return accommodationsToShow;
        }

    }
}
