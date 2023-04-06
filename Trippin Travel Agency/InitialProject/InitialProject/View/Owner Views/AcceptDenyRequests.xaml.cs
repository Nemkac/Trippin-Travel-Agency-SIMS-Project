using InitialProject.DTO;
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

namespace InitialProject.View.Owner_Views
{
    /// <summary>
    /// Interaction logic for AcceptDenyRequests.xaml
    /// </summary>
    public partial class AcceptDenyRequests : UserControl
    {

        
        public AcceptDenyRequests()
        {
            InitializeComponent();
            //MessageBox.Show(_requestDTO.guestName);

        }

        public void DisplayData(RequestDTO requestDTO)
        {
            //_requestDTO = requestDTO;  
            //MessageBox.Show(_requestDTO.guestName);
            string oldArrival = requestDTO.oldArrival.ToString();
            string oldDeparture = requestDTO.oldDeparture.ToString();
            string newArrival = requestDTO.newArrival.ToString();
            string newDeparture = requestDTO.newDeparture.ToString();
            OldArrivalTextBlock.Text = oldArrival;
            OldDepartureTextBlock.Text = oldDeparture;
            NewArrivalTextBlock.Text = newArrival;
            NewDepartureTextBlock.Text = newDeparture;
            
        }
    }
}
