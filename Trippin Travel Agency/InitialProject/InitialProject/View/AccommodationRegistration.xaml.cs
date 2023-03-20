using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Service;
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

namespace InitialProject.View
{
    public partial class AccommodationRegistration : UserControl
    {
        public AccommodationRegistration()
        {
            InitializeComponent();
            FillCountryComboBox();
        }

        private void FillCountryComboBox()
        {
            DataBaseContext countryContext = new DataBaseContext();
            List<AccommodationLocation> countryList = countryContext.LocationsOfAccommodations.ToList();
            foreach (AccommodationLocation location in countryList.ToList())
            {
                if (!countryComboBox.Items.Contains(location.country))
                {
                    countryComboBox.Items.Add(location.country);
                }
            }
        }

        private void SaveAccommodation(object sender, RoutedEventArgs e)
        {
            string name;
            AccommodationLocation location;
            int guestLimit, minDaysBooked, bookingCancelPeriod;
            GetAccommodationBasicProperties(out name, out location, out guestLimit, out minDaysBooked, out bookingCancelPeriod);
            Model.Type type = GetAccommodationType();

            // Add image links
            List<Model.Image> imageLinks = CreateImageLinks();

            Accommodation accommodation = new Accommodation(name, location, guestLimit, minDaysBooked, bookingCancelPeriod, type, imageLinks);
            AccommodationService.Save(accommodation);
            ClearInputs();
        }

        private void GetAccommodationBasicProperties(out string name, out AccommodationLocation location, out int guestLimit, out int minDaysBooked, out int bookingCancelPeriod)
        {
            name = accommodationNameTB.Text;
            string country = countryComboBox.SelectedValue.ToString();
            string city = cityComboBox.SelectedValue.ToString();
            location = AccommodationService.GetLocation(country, city);
            string guestLimitInput = guestLimitTB.Text;
            guestLimit = int.Parse(guestLimitInput);
            string minDaysBookedInput = minDaysBookedTB.Text;
            minDaysBooked = int.Parse(minDaysBookedInput);
            string bookingCancelPeriodInput = bookingCancelPeriodTB.Text;
            bookingCancelPeriod = int.Parse(bookingCancelPeriodInput);
        }

        // Fot putting them in dynamic Text Boxes 
        List<TextBox> dynamicImageLinksTextBoxes = new List<TextBox>();
        private int imageCounter = 1;
        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForImageLink();
            TextBox newTextBox = CreateNewTextBoxForImageLink();
            StackPanel newStackPanel = FillStackPanelWithElements(newLabel, newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicImageLinksTextBoxes.Add(newTextBox);

            // Add the new StackPanel below the button
            containerImageStackPanel.Children.Insert(containerImageStackPanel.Children.IndexOf(addImageButton) + 1, newStackPanel);

            // Update the Margin of the new StackPanel to align it with the button
            Thickness buttonMargin = addImageButton.Margin;
            newStackPanel.Margin = new Thickness(buttonMargin.Left, 10, 0, 0);

            imageCounter++;
        }

        private List<Model.Image> CreateImageLinks()
        {
            List<Model.Image> imageLinks = new List<Model.Image>();

            foreach (var textBox in dynamicImageLinksTextBoxes)
            {
                Model.Image image = new Model.Image();
                image.imageLink = textBox.Text;
                imageLinks.Add(image);
            }

            return imageLinks;
        }

        private static StackPanel FillStackPanelWithElements(Label newLabel, TextBox newTextBox)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newLabel);
            newStackPanel.Children.Add(newTextBox);
            return newStackPanel;
        }

        private TextBox CreateNewTextBoxForImageLink()
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Width = addImageButton.ActualWidth;
            return newTextBox;
        }

        private Label CreateNewLabelForImageLink()
        {
            Label newLabel = new Label();
            newLabel.Content = "Image " + imageCounter.ToString();
            newLabel.Width = addImageButton.ActualWidth / 2 + 15;
            return newLabel;
        }

        private Model.Type GetAccommodationType()
        {
            Model.Type type;
            if (houseRadioButton.IsChecked == true)
            {
                type = 0;
            }
            else if (apartmentRadioButton.IsChecked == true)
            {
                type = (Model.Type)1;
            }
            else
            {
                type = (Model.Type)2;
            }

            return type;
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityComboBox.Items.Clear();
            string selectedCountry = countryComboBox.SelectedValue.ToString();
            GetCitiesByCountry(selectedCountry);
        }

        private void GetCitiesByCountry(string selectedCountry)
        {
            DataBaseContext cityContext = new DataBaseContext();
            List<AccommodationLocation> cityList = cityContext.LocationsOfAccommodations.ToList();

            foreach (AccommodationLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!cityComboBox.Items.Contains(location.city))
                    {
                        cityComboBox.Items.Add(location.city);
                    }
                }
            }
        }

        private void ClearInputs()
        {
            accommodationNameTB.Clear();
            guestLimitTB.Clear();
            minDaysBookedTB.Clear();
            bookingCancelPeriodTB.Clear();
            houseRadioButton.IsChecked = false;
            hutRadioButton.IsChecked = false;
            apartmentRadioButton.IsChecked = false;
        }
    }
}
