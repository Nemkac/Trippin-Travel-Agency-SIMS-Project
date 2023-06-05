using InitialProject.Model;
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
    /// Interaction logic for ForumsInterface.xaml
    /// </summary>
    public partial class ForumsInterface : Window
    {
        public ForumsInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.forumsInterface = this;
            this.DataContext = new ForumsViewModel();
        }

        public void F(object s, KeyEventArgs k){if(k.Key == Key.LeftCtrl){Keyboard.Focus(s0);}if(k.Key == Key.RightCtrl){Keyboard.Focus(s1);}}
    }
}
