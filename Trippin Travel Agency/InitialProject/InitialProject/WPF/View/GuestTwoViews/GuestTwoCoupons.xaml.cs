using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for GuestTwoCoupons.xaml
    /// </summary>
    public partial class GuestTwoCoupons : UserControl
    {
        public GuestTwoCoupons()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        public void Window_Loaded(object sender, RoutedEventArgs e) {
            this.UsernameLabel.Content = "Hello, " + LoggedUser.username + "!";
            this.UsernameLabel2.Content = "@" + LoggedUser.username;
            this.AccountTypeLabel.Content = "Account type:  " + LoggedUser.role;
            
            DataBaseContext context = new DataBaseContext();
            LoadCoupons(context);
        }



        public void LoadCoupons(DataBaseContext context) {
            List<Coupon> coupons = context.Coupons.ToList();

            List<CouponDTO> dataList = new List<CouponDTO>();
            int counter = 0;
            foreach (Coupon coup in coupons)
            {
                if (coup.userId == LoggedUser.id)
                {
                    counter += 1;
                    dataList.Add(new CouponDTO(coup.id, "Coupon" + counter, coup.exiresOn));
                }
            }
            this.dataGrid.ItemsSource = dataList;
        }
    }
}
