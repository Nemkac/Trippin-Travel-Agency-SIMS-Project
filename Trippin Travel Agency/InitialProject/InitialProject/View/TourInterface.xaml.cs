using InitialProject.Model;
using System.Windows;
using System;
using InitialProject.Context;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using InitialProject.Service;
using System.Collections;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.View
{
    public partial class TourInterface : Window
    {
        public TourInterface()
        {
            InitializeComponent();
            FillCountryComboBox();

        }

        private void LeadTrackTourLive(object sender, RoutedEventArgs e)
        {
            TrackTourLiveInterface TrackTourLiveInterface = new TrackTourLiveInterface();
            TrackTourLiveInterface.Show();

        }
        private void FillCountryComboBox()
        {
            DataBaseContext countryToursContext = new DataBaseContext();

            List<TourLocation> countryList = countryToursContext.TourLocation.ToList();
            foreach (TourLocation location in countryList.ToList())
            {
                if (!countryComboBox.Items.Contains(location.country))
                {
                    countryComboBox.Items.Add(location.country);
                }
            }
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
            List<TourLocation> cityList = cityContext.TourLocation.ToList();

            foreach (TourLocation location in cityList.ToList())
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

        // Lists used for creating dynamic TextBox elements for both images and key points
        List<TextBox> dynamicTextBoxes = new List<TextBox>();
        List<TextBox> dynamicImageLinksTextBoxes = new List<TextBox>();
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
            string country = countryComboBox.SelectedValue.ToString();
            string city = cityComboBox.SelectedValue.ToString();
            location = TourService.GetTourLocation(country, city);
            string guestLimitInput = guestLimitTextBox.Text;
            guestLimit = int.Parse(guestLimitInput);
            string hoursDurationInput = hoursDurationTextBox.Text;
            hoursDuration = int.Parse(hoursDurationInput);
            description = descriptionTextBox.Text;
            languageInput = (language)languageComboBox.SelectedValue;
            selectedDate = datePicker.SelectedDate ?? DateTime.Today;
            active = false;
        }

        private void DoesTourExist(string name, DateTime selectedDate, Tour tour)
        {
            bool tourExists = TourService.CheckExistence(name, selectedDate);
            if (!tourExists)
            {
                TourService.Save(tour);
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
                new KeyPoint(startingPointTextBox.Text, false)
            };

            foreach (var textBox in dynamicTextBoxes)
            {
                KeyPoint kp = new KeyPoint(textBox.Text, false);
                keyPoints.Add(kp);
            }

            keyPoints.Add(new KeyPoint(endingPointTextBox.Text, false));
            return keyPoints;
        }

        private void clearInputs()
        {
            datePicker.SelectedDate = null; 
        }

        private int checkpointCounter = 1;
        private void addCheckpointButton_Click(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForKeyPoint();
            TextBox newTextBox = CreateNewTextBoxForKeyPoint();
            StackPanel newStackPanel = CreateNewStackPanelWithElementsForKeyPoint(newLabel, newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicTextBoxes.Add(newTextBox);

            // Add the new StackPanel below the button
            containerStackPanel.Children.Insert(containerStackPanel.Children.IndexOf(addCheckpointButton) + 1, newStackPanel);

            // Update the Margin of the new StackPanel to align it with the button
            Thickness buttonMargin = addCheckpointButton.Margin;
            newStackPanel.Margin = new Thickness(buttonMargin.Left, 10, 0, 0);

            checkpointCounter++;
        }

        private static StackPanel CreateNewStackPanelWithElementsForKeyPoint(Label newLabel, TextBox newTextBox)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newLabel);
            newStackPanel.Children.Add(newTextBox);
            return newStackPanel;
        }

        private TextBox CreateNewTextBoxForKeyPoint()
        {
            TextBox newTextBox = new TextBox();
            newTextBox.Width = addCheckpointButton.ActualWidth / 2 - 10;
            return newTextBox;
        }

        private Label CreateNewLabelForKeyPoint()
        {
            // Create new Label
            Label newLabel = new Label();
            newLabel.Content = "Checkpoint " + checkpointCounter.ToString();
            newLabel.Width = addCheckpointButton.ActualWidth / 2 - 10;
            return newLabel;
        }

        private int imageCounter = 1;
        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForImageLink();
            TextBox newTextBox = CreateNewTextBoxForImageLink();
            StackPanel newStackPanel = CreateNewStackPanelWithElementsForImageLink(newLabel, newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicImageLinksTextBoxes.Add(newTextBox);

            // Add the new StackPanel below the button
            containerImageStackPanel.Children.Insert(containerImageStackPanel.Children.IndexOf(addImageButton) + 1, newStackPanel);

            // Update the Margin of the new StackPanel to align it with the button
            Thickness buttonMargin = addImageButton.Margin;
            newStackPanel.Margin = new Thickness(buttonMargin.Left, 10, 0, 0);

            imageCounter++;
        }

        private static StackPanel CreateNewStackPanelWithElementsForImageLink(Label newLabel, TextBox newTextBox)
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
            newLabel.Width = addImageButton.ActualWidth / 2 - 10;
            return newLabel;
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

