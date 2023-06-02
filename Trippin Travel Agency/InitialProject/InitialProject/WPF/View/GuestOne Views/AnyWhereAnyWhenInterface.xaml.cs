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
using InitialProject.Model;
using InitialProject.WPF.ViewModels.GuestOneViewModels;
using SharpVectors.Scripting;

namespace InitialProject.WPF.View.GuestOne_Views
{
    /// <summary>
    /// Interaction logic for AnyWhereAnyWhenInterface.xaml
    /// </summary>
    public partial class AnyWhereAnyWhenInterface : Window
    {
        private int c;
        private int a;
        private List<IInputElement> n;
        public AnyWhereAnyWhenInterface()
        {
            InitializeComponent();
            GuestOneStaticHelper.anyWhereAnyWhenInterface = this;
            this.DataContext = new AnyWhereAnyWhenViewModel();
            n = new List<IInputElement>() { n0, n1, n2, n3 };
            c = 0;
        }
        public void F(object s, KeyEventArgs k)
        {if (k.Key == Key.LeftShift){if (c == 0){c = 3;}else{c--;}Keyboard.Focus(n[c]);}if (k.Key == Key.RightShift){if (c == 3){c = 0;}else{c++;}Keyboard.Focus(n[c]);}}
    }
}
