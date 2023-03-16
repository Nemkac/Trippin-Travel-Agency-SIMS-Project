using Dapper;
using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
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
using System.Windows.Shapes;
using InitialProject.DTO;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestTwoInterface.xaml
    /// </summary>
    public partial class GuestTwoInterface : Window
    {
        public GuestTwoInterface()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.languageBox.ItemsSource = Enum.GetValues(typeof(language)).Cast<language>();
            TourService tourService = new TourService();
            //tourService.createTour();                       // dodaj unique na polja koja to zahtevaju u bazi 

            DataBaseContext context = new DataBaseContext();
            List<TourDTO> dataList = new List<TourDTO>();
            TourDTO dto = new TourDTO();

            foreach (Tour tour in context.Tours.ToList())
            {
                dto = tourService.CreateDTO(tour);
                dataList.Add(dto);
            }
            this.dataGrid.ItemsSource = dataList;

        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            DataBaseContext context = new DataBaseContext();
            //List<TourDTO> tourDTOsByLanguage = tourService.GetByInputLanguage(language.Text);


            if (!cityName.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByCityName = tourService.GetByInputCityName(cityName.Text);
                this.dataGrid.ItemsSource = tourDTOsByCityName;

            }
            else if (!countryName.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByCountryName = tourService.GetByInputCountryName(countryName.Text);
                this.dataGrid.ItemsSource = tourDTOsByCountryName;
            }
            else if (!duration.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByDuration = tourService.GetByInputTourDuration(duration.Text);
                this.dataGrid.ItemsSource = tourDTOsByDuration;
            }
            else if (!touristLimit.Text.Equals(""))
            {
                List<TourDTO> tourDTOsByTouristLimit = tourService.GetByInputTouristLimit(touristLimit.Text);
                this.dataGrid.ItemsSource = tourDTOsByTouristLimit;
            }
            else if (languageBox.SelectedIndex > -1)  
            {
                List<TourDTO> tourDTOsByLanguage = tourService.GetByInputLanguage((language)languageBox.Items[languageBox.SelectedIndex]);
                languageBox.SelectedIndex = -1;
                this.dataGrid.ItemsSource = tourDTOsByLanguage;
            }
            else
            {
                List<TourDTO> dataList = new List<TourDTO>();
                TourDTO dto = new TourDTO();

                foreach (Tour tour in context.Tours.ToList())
                {
                    dto = tourService.CreateDTO(tour);
                    dataList.Add(dto);
                }
                this.dataGrid.ItemsSource = dataList;

            }

        }
    }
}
