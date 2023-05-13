﻿using InitialProject.Model;
using InitialProject.WPF.ViewModels.GuestOneViewModels;
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
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for GuestsAccountInterface.xaml
    /// </summary>
    public partial class GuestsAccountInterface : Window
    {
        public GuestsAccountInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.guestsAccountInterface = this;
            this.DataContext = new GuestsAccountViewModel();
        }
    }
}