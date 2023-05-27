using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationRegistrationViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        public ObservableCollection<string> countries { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> cities { get; set; } = new ObservableCollection<string>();

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                if (selectedCountry != value)
                {
                    selectedCountry = value;
                    countryComboBox_SelectionChanged();
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        private string selectedCity;
        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                if (selectedCity != value)
                {
                    selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private string guestLimit;
        public string GuestLimit
        {
            get { return guestLimit; }
            set
            {
                if (guestLimit != value)
                {
                    guestLimit = value;
                    OnPropertyChanged(nameof(GuestLimit));
                }
            }
        }

        private string minDaysBooked;
        public string MinDaysBooked
        {
            get { return minDaysBooked; }
            set
            {
                if (minDaysBooked != value)
                {
                    minDaysBooked = value;
                    OnPropertyChanged(nameof(MinDaysBooked));
                }
            }
        }

        private string bookingCancelationPeriod;
        public string BookingCancelationPeriod
        {
            get { return bookingCancelationPeriod; }
            set
            {
                if (bookingCancelationPeriod != value)
                {
                    bookingCancelationPeriod = value;
                    OnPropertyChanged(nameof(BookingCancelationPeriod));
                }
            }
        }

        public bool Apartment
        {
            get { return _apartment; }
            set
            {
                if (_apartment != value)
                {
                    _apartment = value;
                    OnPropertyChanged(nameof(Apartment));
                }
            }
        }
        private bool _apartment;

        public bool House
        {
            get { return _house; }
            set
            {
                if (_house != value)
                {
                    _house = value;
                    OnPropertyChanged(nameof(House));
                }
            }
        }
        private bool _house;

        public bool Hut
        {
            get { return _hut; }
            set
            {
                if (_hut != value)
                {
                    _hut = value;
                    OnPropertyChanged(nameof(Hut));
                }
            }
        }
        private bool _hut;

        private string feedBack;
        public string FeedBack
        {
            get { return feedBack; }
            set
            {
                if (feedBack != value)
                {
                    feedBack = value;
                    OnPropertyChanged(nameof(FeedBack));
                }
            }
        }

        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url != value)
                {
                    url = value;
                    OnPropertyChanged(nameof(Url));
                }
            }
        }

        private string imageNumber;
        public string ImageNumber
        {
            get { return imageNumber; }
            set
            {
                if (imageNumber != value)
                {
                    imageNumber = value;
                    OnPropertyChanged(nameof(ImageNumber));
                }
            }
        }

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }

        public ViewModelCommand AddImageCommand { get; set; }
      
        public ViewModelCommand SaveAccommodationCommand { get; set; }

        public AccommodationRegistrationViewModel() 
        {
            FillCountryComboBox();
            SaveAccommodationCommand = new ViewModelCommand(SaveAccommodation);
            AddImageCommand = new ViewModelCommand(AddImage);
            ImageNumber = "Image " + imageCounter;

            if(LoggedUser.creatingAccommodationFromRecomendation == false)
            {
                SelectedCountry = LoggedUser.MostPopularCountry;
                SelectedCity = LoggedUser.MostPopularCity;
            } 
            else
            {
                SelectedCountry = null;
                selectedCity = null;
            }

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
        }

        private void FillCountryComboBox()
        {
            DataBaseContext countryContext = new DataBaseContext();
            List<AccommodationLocation> countryList = countryContext.AccommodationLocation.ToList();
            foreach (AccommodationLocation location in countryList.ToList())
            {
                if (!countries.Contains(location.country))
                {
                    countries.Add(location.country);
                }
            }
        }

        private void countryComboBox_SelectionChanged()
        {
            cities.Clear();
            string selectedCountry = SelectedCountry;
            GetCitiesByCountry(selectedCountry);
        }

        private void GetCitiesByCountry(string selectedCountry)
        {
            DataBaseContext cityContext = new DataBaseContext();
            List<AccommodationLocation> cityList = cityContext.AccommodationLocation.ToList();

            foreach (AccommodationLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!cities.Contains(location.city))
                    {
                        cities.Add(location.city);
                    }
                }
            }
        }

        private void GetAccommodationBasicProperties(out string name, out AccommodationLocation location, out int guestLimit, out int minDaysBooked, out int bookingCancelPeriod)
        {
            name = AccommodationName;
            string country = SelectedCountry;
            string city = SelectedCity;
            location = this.accommodationService.GetLocation(country, city);
            string guestLimitInput = GuestLimit;
            guestLimit = int.Parse(guestLimitInput);
            string minDaysBookedInput = MinDaysBooked;
            minDaysBooked = int.Parse(minDaysBookedInput);
            string bookingCancelPeriodInput = BookingCancelationPeriod;
            bookingCancelPeriod = int.Parse(bookingCancelPeriodInput);
        }

        private Model.Type GetAccommodationType()
        {
            Model.Type type;
            if (Apartment == true)
            {
                type = 0;
                return type;
            }
            if (House == true)
            {
                type = (Model.Type)1;
                return type;
            }
            if(Hut == true)
            {
                type = (Model.Type)2;
                return type;
            }

            return 0;
        }

        private void ClearInputs()
        {
            AccommodationName = null;
            GuestLimit = null;
            MinDaysBooked = null;
            BookingCancelationPeriod = null;
            House = false;
            Hut = false;
            Apartment = false;
            imageCounter = 1;
            ImageNumber = "Image " + imageCounter;
        }

        private bool IsPropertyNull()
        {
            if ((AccommodationName == null || GuestLimit == null || MinDaysBooked == null  || BookingCancelationPeriod == null) && (Hut == false || Apartment == false || House == false)) return false;
            return true;
        }

        private void SaveAccommodation(object obj)
        {
            bool checkValuesExistance = IsPropertyNull();

            if (checkValuesExistance)
            {
                string name;
                AccommodationLocation location = new AccommodationLocation();
                int guestLimit, minDaysBooked, bookingCancelPeriod;
                GetAccommodationBasicProperties(out name, out location, out guestLimit, out minDaysBooked, out bookingCancelPeriod);
                Model.Type type = GetAccommodationType();

                // Add image links
                List<Model.Image> imageLinks = CreateImageLinks();

                Accommodation accommodation = new Accommodation(name, location, guestLimit, minDaysBooked, bookingCancelPeriod, type, imageLinks, LoggedUser.id);
                accommodationService.Save(accommodation);
                ClearInputs();
                FeedBack = "Accommodation registered successfully!";
            }
            else
            {
                MessageBox.Show("You must enter a valid data before registering accommodation!");
            }
        }

        List<string> dynamicImageLinksTextBoxes = new List<string>();
        private int imageCounter = 1;

        private List<Model.Image> CreateImageLinks()
        {
            List<Model.Image> imageLinks = new List<Model.Image>();

            foreach(string url in dynamicImageLinksTextBoxes)
            {
                Model.Image image = new Model.Image();
                image.imageLink = url;
                imageLinks.Add(image);
            }


            return imageLinks;
        }

        public void AddImage(object obj)
        {
            dynamicImageLinksTextBoxes.Add(Url);
            Url = null;
            imageCounter++;
            ImageNumber = "Image " + imageCounter;
        }
    }
}
