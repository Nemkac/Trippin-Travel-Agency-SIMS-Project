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
        private int c,v;
        private List<IInputElement> n;
        private List<IInputElement> m;
        public GuestOneInterface()
        {
            GuestOneStaticHelper.guestOneInterface = this;
            this.DataContext = new GuestOneViewModel();
            InitializeComponent();
            n = new List<IInputElement>() { n0, n1, n2, n3, n4, n5};
            m = new List<IInputElement>() { d0, d1, d2};
            c = 0;
            v = -1;
        }

        public void F(object s, KeyEventArgs k)
        { if (k.Key == Key.LeftCtrl) { if (c == 0) { c = 5; } else { c--; } Keyboard.Focus(n[c]); } if (k.Key == Key.RightCtrl) { if (c == 5) { c = 0; } else { c++; } Keyboard.Focus(n[c]); } }

        public void G(object s, KeyEventArgs k)
        {if(k.Key == Key.F1){if (v == 2){v = 0;} else{v++;}Keyboard.Focus(m[v]);}}
    }
}
