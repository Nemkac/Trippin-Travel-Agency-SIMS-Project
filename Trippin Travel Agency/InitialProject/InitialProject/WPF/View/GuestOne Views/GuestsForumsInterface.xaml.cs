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
    /// Interaction logic for GuestsForumsInterface.xaml
    /// </summary>
    public partial class GuestsForumsInterface : Window
    {
        private int p;
        private List<IInputElement> n;
        public GuestsForumsInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.guestsForumsInterface = this;
            this.DataContext = new GuestsForumsViewModel();
            p = -1;
            n = new List<IInputElement>() { p0,p1,p2 };
        }

        public void F(object s, KeyEventArgs k) {  if (k.Key == Key.LeftShift) { if (p == 0 || p == -1) { p = 2; } else  {  p--; }  Keyboard.Focus(n[p]);}if (k.Key == Key.RightShift) { if (p == 2 || p == -1) { p = 0; } else { p++; }Keyboard.Focus(n[p]); }} }
}
