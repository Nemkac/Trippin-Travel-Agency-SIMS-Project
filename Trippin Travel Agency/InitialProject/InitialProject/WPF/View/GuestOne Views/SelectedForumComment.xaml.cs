using InitialProject.Model;
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
    /// Interaction logic for SelectedForumComment.xaml
    /// </summary>
    public partial class SelectedForumComment : Window
    {
        public SelectedForumComment()
        {
            InitializeComponent();
            Writter.Header = GuestOneStaticHelper.writtersName;
            Comment.Text = GuestOneStaticHelper.commentToShow;
        }
        public void Func(object sender, KeyEventArgs k)
        {
            if (k.Key == Key.Enter)
            {
                GuestOneStaticHelper.selectedForumInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f5f6fa");
                this.Close();
            }
        }
    }
}
