using InitialProject.Model;
using InitialProject.WPF.View.GuestOne_Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GuestsAccountViewModel : ViewModelBase
    {
        public ViewModelCommand OpenNavigator;
        public GuestsAccountViewModel()
        {
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            SuperGuestText = GenerateSuperGuestText();
            discountPoints = "5";
            discountPointsText = "Number of discount points\n(lasting until 5/12/2024)";
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.guestsAccountInterface.Left + (GuestOneStaticHelper.guestsAccountInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.guestsAccountInterface.Top + (GuestOneStaticHelper.guestsAccountInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.guestsAccountInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private string superGuestText;
        public string SuperGuestText
        {
            get { return superGuestText; }
            set
            {
                if (superGuestText != value)
                {
                    superGuestText = value;
                    OnPropertyChanged(nameof(SuperGuestText));
                }
            }
        }

        private string discountPointsText;
        public string DiscountPointsText
        {
            get { return discountPointsText; }
            set
            {
                if (discountPointsText != value)
                {
                    discountPointsText = value;
                    OnPropertyChanged(nameof(DiscountPointsText));
                }
            }
        }

        private string discountPoints;
        public string DiscountPoints
        {
            get { return discountPoints; }
            set
            {
                if (discountPoints != value)
                {
                    discountPoints = value;
                    OnPropertyChanged(nameof(DiscountPoints));
                }
            }
        }

        public string GenerateSuperGuestText()
        {
            return "This is a place where you can see how many discount points are there left.\n\nWhat are discount points and how they work?\n\n" +
                "By booking 10 accommodation in a time span of one year, you are acquiring 5 discount points.\n\nIf you accomplish this, " +
                "you are becoming what call a \"Super Guest\".\n\nOne discount point means one discount on your next booking.\n\n" +
                "These points last one year after acquiring, unless you keep the title of super guest !";
        }
    }
}
