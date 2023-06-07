using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ShowAccommodationImagesViewModel : ViewModelBase
    {

        public int imagecounter = 1;
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set 
            { 
                _imagePath = value; 
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private string _headerButtonIconColor;
        public string HeaderButtonIconColor
        {
            get { return _headerButtonIconColor; }
            set
            {
                _headerButtonIconColor = value;
                OnPropertyChanged(nameof(HeaderButtonIconColor));
            }
        }

        public ICommand GoForward { get; }
        public ICommand GoBack { get; }

        public ShowAccommodationImagesViewModel()
        {
            GoForward = new ViewModelCommand(NextImage);
            GoBack = new ViewModelCommand(PreviousImage);

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            HeaderButtonIconColor = Mediator.GetCurrentIsChecked() ? "#487eb0" : "#192a56";

            ImagePath = "pack://application:,,,/Assets/Existing Assets/image1.jpg";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            HeaderButtonIconColor = isChecked ? "#487eb0" : "#192a56";
        }

        public void NextImage(object obj)
        {
            if (imagecounter < 4)
            {
                imagecounter += 1;
                ImagePath = "pack://application:,,,/Assets/Existing Assets/image" + imagecounter.ToString() + ".jpg";
            }
            if(imagecounter == 4)
            {
                imagecounter = 1;
                ImagePath = "pack://application:,,,/Assets/Existing Assets/image" + imagecounter.ToString() + ".jpg";
            }
            
        }

        public void PreviousImage(object obj)
        {
            if (imagecounter > 1)
            {
                imagecounter -= 1;
                ImagePath = "pack://application:,,,/Assets/Existing Assets/image" + imagecounter.ToString() + ".jpg";
            }
            if (imagecounter == 1)
            {
                imagecounter = 3;
                ImagePath = "pack://application:,,,/Assets/Existing Assets/image" + imagecounter.ToString() + ".jpg";
            }
        }
    }
}
