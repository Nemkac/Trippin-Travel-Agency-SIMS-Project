using InitialProject.WPF.View.GuestTwoViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{
    
    public class DetailedTourViewModel : ViewModelBase
    {
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public DetailedTourViewModel() 
        {
            WindowLoaded();
        }

        public void WindowLoaded()
        {

            if (TourDisplayViewModel.DetailedId != -1)
            {
                Id = TourDisplayViewModel.DetailedId.ToString();
                TourDisplayViewModel.DetailedId = -1;
            }
            if (GuestTwoMesagesViewModel.tourIdTransfer != -1)
            {
                Id = GuestTwoMesagesViewModel.tourIdTransfer.ToString();
                GuestTwoMesagesViewModel.tourIdTransfer = -1;
            }
        }

    }
}
