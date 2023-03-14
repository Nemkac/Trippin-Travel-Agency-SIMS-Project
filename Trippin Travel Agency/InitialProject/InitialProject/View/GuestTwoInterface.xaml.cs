using Dapper;
using InitialProject.Context;
using InitialProject.Model;
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
            /* DataBaseContext context = new DataBaseContext();
             SQLiteConnection conn = new SQLiteConnection("Data Source = MyDatabase.sqlite");
             conn.Open();
             SQLiteCommand cmd = new SQLiteCommand("select * from Users", conn);
             SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
             DataTable dataTable = new DataTable();
             adapter.Fill(dataTable);
             this.dataGrid.ItemsSource = dataTable.DefaultView; */

            DataBaseContext context = new DataBaseContext();
            List<User> dataList = context.Users.ToList();
            foreach (User user in dataList.ToList())
            {
                if (user.Role == "TourGuide") {
                    dataList.Remove(user);
                }

            }
            this.dataGrid.ItemsSource = dataList;
        }
    }
}
