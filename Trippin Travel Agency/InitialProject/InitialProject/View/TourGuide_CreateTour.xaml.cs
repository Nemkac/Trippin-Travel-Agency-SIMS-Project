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
    /// <summary>
    /// Interaction logic for TourGuide_Tours.xaml
    /// </summary>
    public partial class TourGuide_CreateTour : UserControl
    {
        public TourGuide_CreateTour()
        {
            InitializeComponent();
        }

        // Lists used for creating dynamic TextBox elements for both images and key points
        List<TextBox> dynamicTextBoxes = new List<TextBox>();
        List<TextBox> dynamicImageLinksTextBoxes = new List<TextBox>();

        private int checkpointCounter = 1;
        private StackPanel lastCreatedStackPanel;

        private void addKeyPoint_IfChecked(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForKeyPoint();
            TextBox newTextBox = CreateNewTextBoxForKeyPoint();
            StackPanel newStackPanel = CreateNewStackPanelWithElementsForKeyPoint(newLabel, newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicTextBoxes.Add(newTextBox);

            // Remove the previous StackPanel
            RemovePreviousStackPanel();

            // Add the new StackPanel in the same place
            containerKeyPoints.Children.Insert(containerKeyPoints.Children.IndexOf(addCheckpointButton) + 1, newStackPanel);

            // Store the last created StackPanel
            lastCreatedStackPanel = newStackPanel;

            // Update the Margin of the new StackPanel to align it with the button
            newStackPanel.Margin = new Thickness(15, 10, 0, 0);

            checkpointCounter++;
            ResetRadioButton(sender);
        }

        private void RemovePreviousStackPanel()
        {
            if (lastCreatedStackPanel != null)
            {
                containerKeyPoints.Children.Remove(lastCreatedStackPanel);
                lastCreatedStackPanel = null;
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

    }
}
