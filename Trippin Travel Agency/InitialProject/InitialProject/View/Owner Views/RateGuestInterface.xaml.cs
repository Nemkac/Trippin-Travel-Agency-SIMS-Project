using InitialProject.Model;
using InitialProject.Service;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RateGuestInterface.xaml
    /// </summary>
    public partial class RateGuestInterface : Window
    {
        private int bookingId;
        public RateGuestInterface(int bookingId)
        {
            InitializeComponent();
            this.bookingId = bookingId;
        }

        private void SaveRate(object sender, RoutedEventArgs e)
        {
            BookingService bookingService = new BookingService();
            int cleannessRate = GetCleanness();
            int rulesRate = GetRulesRespecting();
            string comment = commentTB.Text;
            int guestId = bookingService.GetGuestId(bookingId);
            GuestRate newGuestRate = new GuestRate(cleannessRate, rulesRate, comment, guestId, bookingId);
            GuestRateService.Save(newGuestRate);
            this.Close();
        }

        private int GetCleanness()
        {
            int cleannessRate;
            if(cleannesRateRadioButton1.IsChecked == true)
            {
                cleannessRate = 1;
            }
            else if(cleannesRateRadioButton2.IsChecked == true)
            {
                cleannessRate = 2;
            }
            else if (cleannesRateRadioButton3.IsChecked == true)
            {
                cleannessRate = 3;
            }
            else if (cleannesRateRadioButton4.IsChecked == true)
            {
                cleannessRate = 4;
            }
            else
            {
                cleannessRate = 5;
            }

            return cleannessRate;  
        }

        private int GetRulesRespecting()
        {
            int rulesRate;
            if (rulesRateRadioButton1.IsChecked == true)
            {
                rulesRate = 1;
            }
            else if (rulesRateRadioButton2.IsChecked == true)
            {
                rulesRate = 2;
            }
            else if (rulesRateRadioButton3.IsChecked == true)
            {
                rulesRate = 3;
            }
            else if (rulesRateRadioButton4.IsChecked == true)
            {
                rulesRate = 4;
            }
            else
            {
                rulesRate = 5;
            }

            return rulesRate;
        }
    }
}
