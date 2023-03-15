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
            // UserService uS = new UserService();
            // List<User> dataList = uS.TestFirstView();
            // this.dataGrid.ItemsSource = dataList;               
            TourService tourService = new TourService();
            tourService.createTour();                       // dodaj unique na polja koja to zahtevaju u bazi 

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

        }
    }
}
