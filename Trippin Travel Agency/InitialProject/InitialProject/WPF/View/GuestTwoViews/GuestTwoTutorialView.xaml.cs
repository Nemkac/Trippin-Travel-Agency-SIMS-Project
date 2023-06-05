using InitialProject.WPF.ViewModels.GuestTwoViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View.GuestTwoViews
{
    /// <summary>
    /// Interaction logic for GuestTwoTutorialView.xaml
    /// </summary>
    public partial class GuestTwoTutorialView : UserControl
    {
        public GuestTwoTutorialView()
        {
            InitializeComponent();
            this.Tutorial.Play();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            this.Tutorial.Play();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
           this.Tutorial.Pause();
        }
    }
}
