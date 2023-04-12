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

    public partial class GuestTwoInterface : Window
    {
        public GuestTwoInterface()
        {
            InitializeComponent();
           
        } 
    }
}
