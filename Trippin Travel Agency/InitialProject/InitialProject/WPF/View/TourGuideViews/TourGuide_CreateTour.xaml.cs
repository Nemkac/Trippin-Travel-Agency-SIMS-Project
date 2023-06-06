using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Reflection;


namespace InitialProject.WPF.View.TourGuideViews
{
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_CreateTour : UserControl
    {
        private TourService tourService;
        // Za demo 
        private DispatcherTimer timer;
        private string textToFill;
        public TourGuide_CreateTour()
        {
            InitializeComponent();
            FillCountryComboBox();
            this.tourService = new(new TourRepository());
            this.Loaded += DataLoaded;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(400);
            timer.Tick += Timer_Tick;

        }
        public void DataLoaded(object sender, RoutedEventArgs e)
        {
            DataBaseContext context = new DataBaseContext();
            string country, city, language;
            List<TourLocationTransfer> tourLocationTransfers = context.TourLocationTransfers.ToList();
            List<TourLanguageTransfer> tourLanguageTransfers = context.TourLanguageTransfers.ToList();
            List<TourFlagTransfer> tourFlagTransfers = context.TourFlagTransfers.ToList();
            if (tourFlagTransfers.Last().flag == 0)
            {
                country = tourLocationTransfers.Last().country;
                city = tourLocationTransfers.Last().city;
                tourCountryComboBox.Text = country;
                tourCityComboBox.Text = city;
            }
            if(tourFlagTransfers.Last().flag == 1)
            {
                language = tourLanguageTransfers.Last().language;
                tourLanguageComboBox.Text = language;
            }
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

            User user = FetchUserFromDatabase(LoggedUser.id);
            bool isSuper = user.super;
            tourNameTextBox.ClearValue(TextBox.BorderBrushProperty);
            tourCountryComboBox.ClearValue(ComboBox.BorderBrushProperty);
            tourCityComboBox.ClearValue(ComboBox.BorderBrushProperty);
            tourMaximumNumberOfGuestsTextBox.ClearValue(TextBox.BorderBrushProperty);
            tourDurationTextBox.ClearValue(TextBox.BorderBrushProperty);
            tourLanguageComboBox.ClearValue(ComboBox.BorderBrushProperty);
            tourStartingPointTextBox.ClearValue(TextBox.BorderBrushProperty);
            tourEndingPointTextBox.ClearValue(TextBox.BorderBrushProperty);
            tourCalendar.ClearValue(Calendar.BorderBrushProperty);
            tourDescriptionTextBox.ClearValue(TextBox.BorderBrushProperty);

            if (string.IsNullOrWhiteSpace(tourNameTextBox.Text))
            {
                tourNameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (tourCountryComboBox.SelectedItem == null)
            {
                tourCountryComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (tourCityComboBox.SelectedItem == null)
            {
                tourCityComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(tourMaximumNumberOfGuestsTextBox.Text))
            {
                tourMaximumNumberOfGuestsTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(tourDurationTextBox.Text))
            {
                tourDurationTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (tourLanguageComboBox.SelectedItem == null)
            {
                tourLanguageComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(tourStartingPointTextBox.Text))
            {
                tourStartingPointTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(tourEndingPointTextBox.Text))
            {
                tourEndingPointTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (tourCalendar.SelectedDate == null)
            {
                tourCalendar.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(tourDescriptionTextBox.Text))
            {
                tourDescriptionTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(tourNameTextBox.Text) ||
                tourCountryComboBox.SelectedItem == null ||
                tourCityComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(tourMaximumNumberOfGuestsTextBox.Text) ||
                string.IsNullOrWhiteSpace(tourDurationTextBox.Text) ||
                tourLanguageComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(tourStartingPointTextBox.Text) ||
                string.IsNullOrWhiteSpace(tourEndingPointTextBox.Text) ||
                tourCalendar.SelectedDate == null ||
                string.IsNullOrWhiteSpace(tourDescriptionTextBox.Text))
            {
                MessageBox.Show("Fill in all the data.", "Data Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                saveMessageTextBlock.Visibility = Visibility.Collapsed;

                fillAllTheDataMessageTextBlock.Text = "Fill in all the data.";
                fillAllTheDataMessageTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                CreateTourBasicProperties(out name, out location, out guestLimit, out hoursDuration, out description, out languageInput, out selectedDate, out active);

                ICollection<KeyPoint> keyPoints = CreateKeyPoints();

                List<Model.Image> imageLinks = CreateImageLinks();

                Tour tour = new Tour(name, location.id, keyPoints, description, languageInput, guestLimit, selectedDate, hoursDuration, imageLinks, active, LoggedUser.id);
                tour.super = isSuper;
                DoesTourExist(name, selectedDate, tour);
            }
        }
        private User FetchUserFromDatabase(int userId)
        {
            using (DataBaseContext dbContext = new DataBaseContext())
            {
                return dbContext.Users.Find(userId);
            }
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

                saveMessageTextBlock.Text = "Tour saved succesfully";
                saveMessageTextBlock.Visibility = Visibility.Visible;
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

            dynamicTextBoxes.Add(newTextBox);

            RemovePreviousStackPanelKeyPoints();

            containerKeyPoints.Children.Insert(containerKeyPoints.Children.IndexOf(addCheckpointButton) + 1, newStackPanel);

            lastCreatedStackPanelKeyPoints = newStackPanel;

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

        // DEMO 
        // DEMO 
        private void startDemo_Click(object sender, RoutedEventArgs e)
        {
            StartDemo();
        }

        private async void StartDemo()
        {
            await FillTextBoxWithDelay(tourNameTextBox, "Krstarenje Mediteranom", TimeSpan.FromMilliseconds(400));
            await SelectComboBoxItemWithDelay(tourCountryComboBox, 0, TimeSpan.FromMilliseconds(200));
            await SelectComboBoxItemWithDelay(tourCityComboBox, 0, TimeSpan.FromMilliseconds(200));
            await FillTextBoxWithDelay(tourMaximumNumberOfGuestsTextBox, "75", TimeSpan.FromMilliseconds(400));

            ScrollToElement(tourDurationTextBox);
            await FillTextBoxWithDelay(tourDurationTextBox, "12", TimeSpan.FromMilliseconds(400));
            await SelectComboBoxItemWithDelay(tourLanguageComboBox, 0, TimeSpan.FromMilliseconds(200));
            await FillTextBoxWithDelay(tourStartingPointTextBox, "Novi Sad", TimeSpan.FromMilliseconds(400));
            AddCheckpoint();
            await FillDynamicTextBoxWithDelay(dynamicTextBoxes.Last(), "Rim", TimeSpan.FromMilliseconds(400));
            await FillTextBoxWithDelay(tourEndingPointTextBox, "Monako", TimeSpan.FromMilliseconds(400));

            ScrollByPixels(myScrollViewer, 400);
            SelectCalendarDate(27);
            AddImage();
            await FillDynamicTextBoxWithDelay(dynamicImageLinksTextBoxes.First(), "URL Neke slike", TimeSpan.FromMilliseconds(400));

            ScrollByPixels(myScrollViewer, 400);
            await FillTextBoxWithDelay(tourDescriptionTextBox, "Apsolventska ekskurzija", TimeSpan.FromMilliseconds(400));
            await Task.Delay(1000);
            Save(saveButton, new RoutedEventArgs());
        }

        private void SelectCalendarDate(int day)
        {
            DateTime currentDate = DateTime.Today;
            DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);
            tourCalendar.SelectedDate = selectedDate;
        }


        private async Task FillTextBoxWithDelay(TextBox textBox, string text, TimeSpan delay)
        {
            foreach (char c in text)
            {
                textBox.Text += c;
                await Task.Delay(delay);
            }
        }
        private async Task SelectComboBoxItemWithDelay(ComboBox comboBox, int index, TimeSpan delay)
        {
            comboBox.Focus();
            ToggleComboBoxDropdown(comboBox);
            await Task.Delay(delay);
            SelectComboBoxItem(comboBox, index);
        }
        private void ToggleComboBoxDropdown(ComboBox comboBox)
        {
            var toggleButton = comboBox.Template.FindName("ToggleButton", comboBox) as ToggleButton;
            if (toggleButton != null)
            {
                toggleButton.IsChecked = true;
            }
        }
        private void SelectComboBoxItem(ComboBox comboBox, int index)
        {
            if (comboBox.Items.Count > index && index >= 0)
            {
                comboBox.SelectedIndex = index;
            }
        }
        private void ScrollToElement(UIElement element)
        {
            ScrollViewer scrollViewer = FindParentScrollViewer(element);

            if (scrollViewer != null)
            {
                GeneralTransform transform = element.TransformToAncestor(scrollViewer);
                Rect elementRect = transform.TransformBounds(new Rect(0, 0, element.RenderSize.Width, element.RenderSize.Height));
                scrollViewer.ScrollToVerticalOffset(elementRect.Top);
            }
        }
        private ScrollViewer FindParentScrollViewer(UIElement element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);

            while (parent != null && !(parent is ScrollViewer))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ScrollViewer;
        }
        private void AddCheckpoint()
        {
            RadioButton radioButton = addCheckpointButton;
            radioButton.IsChecked = true;
            addKeyPoint_IfChecked(radioButton, null);
        }
        private void AddImage()
        {
            RadioButton radioButton = addimageButton;
            //radioButton.IsChecked = true;
            addImage_IfChecked(radioButton, null);
        }
        private async Task FillDynamicTextBoxWithDelay(TextBox textBox, string text, TimeSpan delay)
        {
            foreach (char c in text)
            {
                textBox.Text += c;
                await Task.Delay(delay);
            }
        }
        private void ScrollByPixels(ScrollViewer scrollViewer, double pixelsToScroll)
        {
            double currentVerticalOffset = scrollViewer.VerticalOffset;
            double targetVerticalOffset = currentVerticalOffset + pixelsToScroll;

            if (targetVerticalOffset < 0)
                targetVerticalOffset = 0;
            else if (targetVerticalOffset > scrollViewer.ScrollableHeight)
                targetVerticalOffset = scrollViewer.ScrollableHeight;

            scrollViewer.ScrollToVerticalOffset(targetVerticalOffset);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (textToFill.Length > 0)
            {
                tourNameTextBox.Text += textToFill[0];
                textToFill = textToFill.Substring(1);
            }
            else if (tourCountryComboBox.SelectedIndex == -1)
            {
                timer.Stop();
                tourCountryComboBox.Focus();
                ToggleComboBoxDropdown(tourCountryComboBox);
                SelectComboBoxItem(tourCountryComboBox, 0);
                timer.Start();
            }
            else if (tourCityComboBox.SelectedIndex == -1)
            {
                timer.Stop();
                tourCityComboBox.Focus();
                ToggleComboBoxDropdown(tourCityComboBox);
                SelectComboBoxItem(tourCityComboBox, 0);
                timer.Start();
            }
            else if (tourMaximumNumberOfGuestsTextBox.Text.Length == 0)
            {
                timer.Stop();
                tourMaximumNumberOfGuestsTextBox.Focus();
                textToFill = "75";
                timer.Interval = TimeSpan.FromMilliseconds(400);
                timer.Start();
            }
            else if (tourDurationTextBox.Text.Length == 0)
            {
                timer.Stop();
                tourDurationTextBox.Focus();
                textToFill = "12";
                timer.Interval = TimeSpan.FromMilliseconds(400);
                timer.Start();
            }
            else if (tourLanguageComboBox.SelectedIndex == -1)
            {
                timer.Stop();
                tourLanguageComboBox.Focus();
                ToggleComboBoxDropdown(tourLanguageComboBox);
                SelectComboBoxItem(tourLanguageComboBox, 0);
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
            
        }

        private void tourMaximumNumberOfGuestsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int maxNumberOfGuests;
            if (!int.TryParse(tourMaximumNumberOfGuestsTextBox.Text, out maxNumberOfGuests))
            {
                errorMessageTextBlock.Text = "Entry must be a number!";
                errorMessageTextBlock.Visibility = Visibility.Visible;
                tourMaximumNumberOfGuestsTextBox.BorderBrush = new SolidColorBrush(Colors.Red); 
            }
            else
            {
                errorMessageTextBlock.Text = string.Empty;
                errorMessageTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void tourDurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int duration;
            if (!int.TryParse(tourDurationTextBox.Text, out duration))
            {
                tourDurationErrorMessageTextBlock.Text = "Entry must be a number!";
                tourDurationErrorMessageTextBlock.Visibility = Visibility.Visible;
                tourDurationTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tourDurationErrorMessageTextBlock.Text = string.Empty;
                tourDurationErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void tourNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tourNameTextBox.Text))
            {
                ScrollByPixels(myScrollViewer, -1000);
                tourNameErrorMessageTextBlock.Text = "Enter tour name!";
                tourNameErrorMessageTextBlock.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                tourNameErrorMessageTextBlock.Visibility = Visibility.Collapsed; 
            }

        }


    }
}
