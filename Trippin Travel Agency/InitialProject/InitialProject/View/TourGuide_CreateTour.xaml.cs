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
            // Get the TourGuide_CreateTour instance
            var createTourControl = this.Parent as TourGuide_CreateTour;

            // Find the addCheckpointButton RadioButton
            var addCheckpointButton = createTourControl.FindName("addCheckpointButton") as RadioButton;

            // Find the containerKeyPoints StackPanel
            var containerKeyPoints = createTourControl.FindName("containerKeyPoints") as StackPanel;
        }

        // Lists used for creating dynamic TextBox elements for both images and key points
        List<TextBox> dynamicTextBoxes = new List<TextBox>();
        List<TextBox> dynamicImageLinksTextBoxes = new List<TextBox>();

        private int checkpointCounter = 1;
        private void addKeyPoint_IfChecked(object sender, RoutedEventArgs e)
        {
            Label newLabel = CreateNewLabelForKeyPoint();
            TextBox newTextBox = CreateNewTextBoxForKeyPoint();
            StackPanel newStackPanel = CreateNewStackPanelWithElementsForKeyPoint(newLabel, newTextBox);

            // Add the textbox to the list of dynamic textboxes
            dynamicTextBoxes.Add(newTextBox);

            // Add the new StackPanel below the button
            containerKeyPoints.Children.Insert(containerKeyPoints.Children.IndexOf(addCheckpointButton) + 1, newStackPanel);

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

    }
}
