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

        private void LeadTrackTourLive(object sender, RoutedEventArgs e)
        {
            TrackTourLiveInterface TrackTourLiveInterface = new TrackTourLiveInterface();
            TrackTourLiveInterface.Show();

        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityComboBox.Items.Clear();
            string selectedCountry = countryComboBox.SelectedValue.ToString();
            
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

        List<TextBox> dynamicTextBoxes = new List<TextBox>();
        List<TextBox> dynamicImageLinksTextBoxes = new List<TextBox>();
        private void Save(object sender, RoutedEventArgs e)
        {

            // Basic Properties 
            string name = tourNameTextBox.Text;

            string country = countryComboBox.SelectedValue.ToString(); 
            string city = cityComboBox.SelectedValue.ToString();
            TourLocation location = TourService.findLocation(country, city); 

            string guestLimitInput = guestLimitTextBox.Text;
            int guestLimit = int.Parse(guestLimitInput);

            string hoursDurationInput = hoursDurationTextBox.Text;
            int hoursDuration = int.Parse(hoursDurationInput);

            string description = descriptionTextBox.Text;

            language languageInput = (language)languageComboBox.SelectedValue;

            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Today;

            bool active = false;

            // Add KeyPoints

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

            List<Model.Image> imageLinks = new List<Model.Image>();

            foreach (var textBox in dynamicImageLinksTextBoxes)
            {
                Model.Image image = new Model.Image();
                image.imageLink = textBox.Text;
                imageLinks.Add(image);
            }
            
            Tour tour = new Tour(name, location.id, keyPoints, description, languageInput, guestLimit, selectedDate, hoursDuration, imageLinks, active);

            // Does tour already exists
            bool tourExists = TourService.CheckTourExists(name, selectedDate);
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
        private void clearInputs()
        {
            datePicker.SelectedDate = null; 
        }

        int disUnit = 1;
        private int checkpointCounter = 1;
        private void addCheckpointButton_Click(object sender, RoutedEventArgs e)
        {
            // Create new Label
            Label newLabel = new Label();
            newLabel.Content = "Checkpoint " + checkpointCounter.ToString();
            newLabel.Width = addCheckpointButton.ActualWidth / 2 - 10; // Subtract some margin

            // Create new TextBox
            TextBox newTextBox = new TextBox();
            newTextBox.Width = addCheckpointButton.ActualWidth / 2 - 10; // Subtract some margin


            // Add the new Label and TextBox to a new StackPanel
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newLabel);
            newStackPanel.Children.Add(newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicTextBoxes.Add(newTextBox);

            // Add the new StackPanel below the button
            containerStackPanel.Children.Insert(containerStackPanel.Children.IndexOf(addCheckpointButton) + 1, newStackPanel);

            // Update the Margin of the new StackPanel to align it with the button
            Thickness buttonMargin = addCheckpointButton.Margin;
            newStackPanel.Margin = new Thickness(buttonMargin.Left, 10, 0, 0);

            checkpointCounter++;
        }

        private int imageCounter = 1;
        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Create new Label
            Label newLabel = new Label();
            newLabel.Content = "Image " + imageCounter.ToString();
            newLabel.Width = addImageButton.ActualWidth / 2 - 10; // Subtract some margin

            // Create new TextBox
            TextBox newTextBox = new TextBox();
            newTextBox.Width = addImageButton.ActualWidth;

            // Add the new Label and TextBox to a new StackPanel
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newLabel);
            newStackPanel.Children.Add(newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicImageLinksTextBoxes.Add(newTextBox);

            // Add the new StackPanel below the button
            containerImageStackPanel.Children.Insert(containerImageStackPanel.Children.IndexOf(addImageButton) + 1, newStackPanel);

            // Update the Margin of the new StackPanel to align it with the button
            Thickness buttonMargin = addImageButton.Margin;
            newStackPanel.Margin = new Thickness(buttonMargin.Left, 10, 0, 0);

            imageCounter++;
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

