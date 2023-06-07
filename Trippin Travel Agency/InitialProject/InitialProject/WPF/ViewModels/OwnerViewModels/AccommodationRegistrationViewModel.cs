using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.TourServices;
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
        //------------------------------------------------------------------------------------
        private string _accommodationNameText;
        public string AccommodationNameText
        {
            get { return _accommodationNameText; }
            set
            {
                if (_accommodationNameText != value)
                {
                    _accommodationNameText = value;
                    OnPropertyChanged(nameof(AccommodationNameText));
                }
            }
        }
        private string _countryText;
        public string CountryText
        {
            get { return _countryText; }
            set
            {
                if (_countryText != value)
                {
                    _countryText = value;
                    OnPropertyChanged(nameof(CountryText));
                }
            }
        }

        private string _cityText;
        public string CityText
        {
            get { return _cityText; }
            set
            {
                if (_cityText != value)
                {
                    _cityText = value;
                    OnPropertyChanged(nameof(CityText));
                }
            }
        }
        private string _accommodationTypeText;
        public string AccommodationTypeText
        {
            get { return _accommodationTypeText; }
            set
            {
                if (_accommodationTypeText != value)
                {
                    _accommodationTypeText = value;
                    OnPropertyChanged(nameof(AccommodationTypeText));
                }
            }
        }
        private string _bookingCancelPeriodText;
        public string BookingCancelPeriodText
        {
            get { return _bookingCancelPeriodText; }
            set
            {
                if (_bookingCancelPeriodText != value)
                {
                    _bookingCancelPeriodText = value;
                    OnPropertyChanged(nameof(BookingCancelPeriodText));
                }
            }
        }
        private string _minDaysText;
        public string MinDaysText
        {
            get { return _minDaysText; }
            set
            {
                if (_minDaysText != value)
                {
                    _minDaysText = value;
                    OnPropertyChanged(nameof(MinDaysText));
                }
            }
        }

        private string _maxGuestsText;
        public string MaxGuestsText
        {
            get { return _maxGuestsText; }
            set
            {
                if (_maxGuestsText != value)
                {
                    _maxGuestsText = value;
                    OnPropertyChanged(nameof(MaxGuestsText));
                }
            }
        }

        private string _houseText;
        public string HouseText
        {
            get { return _houseText; }
            set
            {
                if (_houseText != value)
                {
                    _houseText = value;
                    OnPropertyChanged(nameof(HouseText));
                }
            }
        }

        private string _apartmentText;
        public string ApartmentText
        {
            get { return _apartmentText; }
            set
            {
                if (_apartmentText != value)
                {
                    _apartmentText = value;
                    OnPropertyChanged(nameof(ApartmentText));
                }
            }
        }
        private string _hutText;
        public string HutText
        {
            get { return _hutText; }
            set
            {
                if (_hutText != value)
                {
                    _hutText = value;
                    OnPropertyChanged(nameof(HutText));
                }
            }
        }
        private string _saveAccommodationText;
        public string SaveAccommodationText
        {
            get { return _saveAccommodationText; }
            set
            {
                if (_saveAccommodationText != value)
                {
                    _saveAccommodationText = value;
                    OnPropertyChanged(nameof(SaveAccommodationText));
                }
            }
        }
        private string _addImageText;
        public string AddImageText
        {
            get { return _addImageText; }
            set
            {
                if (_addImageText != value)
                {
                    _addImageText = value;
                    OnPropertyChanged(nameof(AddImageText));
                }
            }

        }

        private string _imagesText;
        public string ImagesText
        {
            get { return _imagesText; }
            set
            {
                if (_imagesText != value)
                {
                    _imagesText = value;
                    OnPropertyChanged(nameof(ImagesText));
                }
            }

        }

        private string _imageNoText;
        public string ImageNoText
        {
            get { return _imageNoText; }
            set
            {
                if (_imageNoText != value)
                {
                    _imageNoText = value;
                    OnPropertyChanged(nameof(ImageNoText));
                }
            }

        }

        public ViewModelCommand AddImageCommand { get; set; }
        public ViewModelCommand SaveAccommodationCommand { get; set; }
        public ViewModelCommand UploadPhoto { get; }

        public AccommodationRegistrationViewModel() 
        {
            SaveAccommodationCommand = new ViewModelCommand(SaveAccommodation);
            UploadPhoto = new ViewModelCommand(UploadImage);

            ImageNumber = "Image " + imageCounter;

            FillCountryComboBox();
            SetCountryFromRecommendation();

            Mediator.IsCheckedChanged += OnIsCheckedChanged;
            Mediator.IsLanguageCheckedChanged += OnIsLanguageCheckChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";

            AccommodationNameText = Mediator.GetCurrentIsLanguageChecked() ? "Naziv Smestaja:" : "Accommodation Name:";
            CountryText = Mediator.GetCurrentIsLanguageChecked() ? "Drzava" : "Country";
            CityText = Mediator.GetCurrentIsLanguageChecked() ? "Grad" : "City";
            AccommodationTypeText = Mediator.GetCurrentIsLanguageChecked() ? "Tip Smestaja:" : "Accommodation Type:";
            MinDaysText = Mediator.GetCurrentIsLanguageChecked() ? "Min. dana za rezervaciju" : "Minimum days to book";
            BookingCancelPeriodText = Mediator.GetCurrentIsLanguageChecked() ? "Otkazni rok" : "Booking cancelation period";
            MaxGuestsText = Mediator.GetCurrentIsLanguageChecked() ? "Maksimalno gostiju" : "Maximum number of guests";
            HouseText = Mediator.GetCurrentIsLanguageChecked() ? "Kuca" : "House";
            ApartmentText = Mediator.GetCurrentIsLanguageChecked() ? "Apartman" : "Apartment";
            HutText = Mediator.GetCurrentIsLanguageChecked() ? "Koliba" : "Hut";
            SaveAccommodationText = Mediator.GetCurrentIsLanguageChecked() ? "Sacuvaj Smestaj" : "Save Accommodation";
            AddImageText = Mediator.GetCurrentIsLanguageChecked() ? "Dodaj sliku" : "Add image";
            ImagesText = Mediator.GetCurrentIsLanguageChecked() ? "Slike" : "Images";
        }

        private void OnIsLanguageCheckChanged(object sender, bool isChecked)
        {
            AccommodationNameText = isChecked ? "Naziv Smestaja:" : "Accommodation Name:";
            CountryText = isChecked ? "Drzava" : "Country";
            CityText = isChecked ? "Grad" : "City";
            AccommodationTypeText = isChecked ? "Tip Smestaja:" : "Accommodation Type:";
            MinDaysText = isChecked ? "Min. dana za rezervaciju" : "Minimum days to book";
            BookingCancelPeriodText = isChecked ? "Otkazni rok" : "Booking cancelation period";
            MaxGuestsText = isChecked ? "Maksimalno gostiju" : "Maximum number of guests";
            HouseText = isChecked ? "Kuca" : "House";
            ApartmentText = isChecked ? "Apartman" : "Apartment";
            HutText = isChecked ? "Koliba" : "Hut";
            SaveAccommodationText = isChecked ? "Sacuvaj Smestaj" : "Save Accommodation";
            AddImageText = isChecked ? "Dodaj sliku" : "Add image";
            ImagesText = isChecked ? "Slike" : "Images";
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

        private void SetCountryFromRecommendation()
        {
            if(LoggedUser.creatingAccommodationFromRecomendation == true)
            {
                string MostPopularCountry = LoggedUser.MostPopularCountry;
                SelectedCountry = MostPopularCountry;

                string MostPopularCity = LoggedUser.MostPopularCity;
                SelectedCity = MostPopularCity;
            }
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

        public void AddImage()
        {
            dynamicImageLinksTextBoxes.Add(Url);
            Url = null;
            imageCounter++;
            ImageNumber = "Image " + imageCounter;
        }

        public void UploadImage(object obj)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            bool? response = fileDialog.ShowDialog();
            if (response == true && AccommodationName != null)
            {

                string filepath = fileDialog.FileName;
                string destination = System.IO.Path.Combine("Assets", System.IO.Path.GetFileName(filepath));
                System.IO.File.Copy(filepath, destination, true);
                AddImage(); 
            }
        }
    }
}
