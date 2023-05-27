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
        {
            if (k.Key == Key.LeftShift)
            {
                if (c == 0)
                {
                    c = 3;
                    Keyboard.Focus(n3);
                    debug.Text = "kliknut L na c=0, sad je 3";
                }
                else if(c == 1)
                {
                    c = 0;
                    Keyboard.Focus(n0);
                    debug.Text = "kliknut L na c=1, sad je 0";
                }
                else if (c == 2)
                {
                    c = 1;
                    Keyboard.Focus(n1);
                    debug.Text = "kliknut L na c=2, sad je 1";
                }
                else if (c == 3)
                {
                    c = 2;
                    Keyboard.Focus(n2);
                    debug.Text = "kliknut L na c=3, sad je 2";
                }

            }
            else if (k.Key == Key.RightShift)
            {
                if (c == 3)
                {
                    c = 0;
                    Keyboard.Focus(n0);
                    debug.Text = "kliknut R na c=3, sad je 0";
                }
                else if (c == 0)
                {
                    c = 1;
                    Keyboard.Focus(n1);
                    debug.Text = "kliknut R na c=0, sad je 1";
                }
                else if (c == 1)
                {
                    c = 2;
                    Keyboard.Focus(n2);
                    debug.Text = "kliknut R na c=1, sad je 2";
                }
                else if (c == 2)
                {
                    c = 3;
                    Keyboard.Focus(n3);
                    debug.Text = "kliknut R na c=2, sad je 3";
                }

            }
        }
    }
}
