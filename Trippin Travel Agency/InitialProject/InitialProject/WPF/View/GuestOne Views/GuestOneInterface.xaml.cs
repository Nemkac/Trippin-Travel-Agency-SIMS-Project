using InitialProject.Context;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
using Dapper;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Configuration;
using InitialProject.DTO;
using System.Diagnostics;
using System.Xml.Linq;
using InitialProject.WPF.ViewModels.GuestOneViewModels;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;

namespace InitialProject.WPF.View.GuestOne_Views
{

    public partial class GuestOneInterface : Window
    {
        public GuestOneInterface()
        {
            GuestOneStaticHelper.guestOneInterface = this;
            this.DataContext = new GuestOneViewModel();
            InitializeComponent();
        }
    }
}
