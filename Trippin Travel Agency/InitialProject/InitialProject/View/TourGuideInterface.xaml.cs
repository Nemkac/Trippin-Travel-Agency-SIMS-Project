using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; 

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuideInterface.xaml
    /// </summary>
    public partial class TourGuideInterface : Window
    {
        public TourGuideInterface()
        {
            InitializeComponent();
        }

        private void LeadCreateTour(object sender, RoutedEventArgs e)
        {
            TourInterface TourInterface = new TourInterface();
            TourInterface.Show(); 

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 208));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(64, 115, 158));
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(64, 115, 158));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(245, 246, 250));
            }
        }

    }
}
