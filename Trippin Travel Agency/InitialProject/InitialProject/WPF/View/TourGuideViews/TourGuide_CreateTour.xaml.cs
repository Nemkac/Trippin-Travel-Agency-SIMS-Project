using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_CreateTour : UserControl
    {
        private TourService tourService;
        public TourGuide_CreateTour()
        {
            InitializeComponent();
            FillCountryComboBox();
            this.tourService = new(new TourRepository());
        }

        List<TextBox> dynamicTextBoxes = new List<TextBox>();
        List<TextBox> dynamicImageLinksTextBoxes = new List<TextBox>();

        private void FillCountryComboBox()
        {
            DataBaseContext countryToursContext = new DataBaseContext();

            List<TourLocation> countryList = countryToursContext.TourLocation.ToList();
            foreach (TourLocation location in countryList.ToList())
            {
                if (!tourCountryComboBox.Items.Contains(location.country))
                {
                    tourCountryComboBox.Items.Add(location.country);
                }
            }
        }
        private void tourCountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourCityComboBox.Items.Clear();
            string selectedCountry = tourCountryComboBox.SelectedValue.ToString();

            GetCitiesByCountry(selectedCountry);
        }
        private void GetCitiesByCountry(string selectedCountry)
        {
            DataBaseContext cityContext = new DataBaseContext();
            List<TourLocation> cityList = cityContext.TourLocation.ToList();

            foreach (TourLocation location in cityList.ToList())
            {
                if (location.country.ToString() == selectedCountry)
                {
                    if (!tourCityComboBox.Items.Contains(location.city))
                    {
                        tourCityComboBox.Items.Add(location.city);
                    }
                }
            }
        }

        // TOUR SAVING


        private void Save(object sender, RoutedEventArgs e)
        {
            string name, description;
            TourLocation location;
            int guestLimit, hoursDuration;
            language languageInput;
            DateTime selectedDate;
            bool active;
            CreateTourBasicProperties(out name, out location, out guestLimit, out hoursDuration, out description, out languageInput, out selectedDate, out active);

            ICollection<KeyPoint> keyPoints = CreateKeyPoints();

            List<Model.Image> imageLinks = CreateImageLinks();

            Tour tour = new Tour(name, location.id, keyPoints, description, languageInput, guestLimit, selectedDate, hoursDuration, imageLinks, active,LoggedUser.id);

            DoesTourExist(name, selectedDate, tour);
        }

        private void CreateTourBasicProperties(out string name, out TourLocation location, out int guestLimit, out int hoursDuration, out string description, out language languageInput, out DateTime selectedDate, out bool active)
        {
            name = tourNameTextBox.Text;
            string country = tourCountryComboBox.SelectedValue.ToString();
            string city = tourCityComboBox.SelectedValue.ToString();
            location = this.tourService.GetTourLocation(country, city);
            string guestLimitInput = tourMaximumNumberOfGuestsTextBox.Text;
            guestLimit = int.Parse(guestLimitInput);
            string hoursDurationInput = tourDurationTextBox.Text;
            hoursDuration = int.Parse(hoursDurationInput);
            description = tourDescriptionTextBox.Text;
            languageInput = (language)tourLanguageComboBox.SelectedValue;
            selectedDate = tourCalendar.SelectedDate ?? DateTime.Today;
            active = false;
        }

        private void DoesTourExist(string name, DateTime selectedDate, Tour tour)
        {
            bool tourExists = TourService.CheckExistence(name, selectedDate);
            if (!tourExists)
            {
                this.tourService.Save(tour);
                clearInputs();
            }
            else
            {
                MessageBox.Show("Tour with the same name and date already exists.");
                return;

            }
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

        private ICollection<KeyPoint> CreateKeyPoints()
        {
            ICollection<KeyPoint> keyPoints = new List<KeyPoint>
            {
                new KeyPoint(tourStartingPointTextBox.Text, false)
            };

            foreach (var textBox in dynamicTextBoxes)
            {
                KeyPoint kp = new KeyPoint(textBox.Text, false);
                keyPoints.Add(kp);
            }

            keyPoints.Add(new KeyPoint(tourEndingPointTextBox.Text, false));
            return keyPoints;
        }

        private void clearInputs()
        {
            tourCalendar.SelectedDate = null;
        }

        // CHECKPOINTS 

        private int checkpointCounter = 1;
        private StackPanel lastCreatedStackPanelKeyPoints;

        private void addKeyPoint_IfChecked(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForKeyPoint();
            TextBox newTextBox = CreateNewTextBoxForKeyPoint();
            StackPanel newStackPanel = CreateNewStackPanelWithElementsForKeyPoint(newLabel, newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicTextBoxes.Add(newTextBox);

            // Remove the previous StackPanel
            RemovePreviousStackPanelKeyPoints();

            // Add the new StackPanel in the same place
            containerKeyPoints.Children.Insert(containerKeyPoints.Children.IndexOf(addCheckpointButton) + 1, newStackPanel);

            // Store the last created StackPanel
            lastCreatedStackPanelKeyPoints = newStackPanel;

            // Update the Margin of the new StackPanel to align it with the button
            newStackPanel.Margin = new Thickness(15, 10, 0, 0);

            checkpointCounter++;
            ResetRadioButton(sender);
        }

        private void RemovePreviousStackPanelKeyPoints()
        {
            if (lastCreatedStackPanelKeyPoints != null)
            {
                containerKeyPoints.Children.Remove(lastCreatedStackPanelKeyPoints);
                lastCreatedStackPanelKeyPoints = null;
            }
        }

        private static void ResetRadioButton(object sender)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                radioButton.IsChecked = false;
            }
        }

        private static StackPanel CreateNewStackPanelWithElementsForKeyPoint(Label newLabel, TextBox newTextBox)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newLabel);
            newTextBox.Margin = new Thickness(20, 0, 0, 0);
            newStackPanel.Children.Add(newTextBox);
            return newStackPanel;
        }

        private TextBox CreateNewTextBoxForKeyPoint()
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Width = 170;
            return newTextBox;
        }

        private Label CreateNewLabelForKeyPoint()
        {
            Label newLabel = new Label();
            newLabel.Content = "Checkpoint " + checkpointCounter.ToString();
            newLabel.MinWidth = addCheckpointButton.ActualWidth / 2 - 10;
            return newLabel;
        }

        // IMAGES 

        private int imageCounter = 1;
        private StackPanel lastCreatedStackPanelImages;

        private void RemovePreviousStackPanelImages()
        {
            if (lastCreatedStackPanelImages != null)
            {
                containerImages.Children.Remove(lastCreatedStackPanelImages);
                lastCreatedStackPanelImages = null;
            }
        }

        private void addImage_IfChecked(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForImageLink();
            TextBox newTextBox = CreateNewTextBoxForImageLink();
            StackPanel newStackPanel = CreateNewStackPanelWithElementsForImageLink(newLabel, newTextBox);

            dynamicImageLinksTextBoxes.Add(newTextBox);
            RemovePreviousStackPanelImages();
            containerImages.Children.Insert(containerImages.Children.IndexOf(addimageButton) + 1, newStackPanel);
            lastCreatedStackPanelImages = newStackPanel;

            imageCounter++;
            ResetRadioButton(sender);
        }

        private static StackPanel CreateNewStackPanelWithElementsForImageLink(Label newLabel, TextBox newTextBox)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Margin = new Thickness(10);
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newLabel);
            newStackPanel.Children.Add(newTextBox);
            return newStackPanel;
        }

        private TextBox CreateNewTextBoxForImageLink()
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Width = 200;
            return newTextBox;
        }

        private Label CreateNewLabelForImageLink()
        {
            Label newLabel = new Label();
            newLabel.Content = "Image " + imageCounter.ToString();
            newLabel.MinWidth = addimageButton.ActualWidth / 2 - 10;
            return newLabel;
        }

    }
}
