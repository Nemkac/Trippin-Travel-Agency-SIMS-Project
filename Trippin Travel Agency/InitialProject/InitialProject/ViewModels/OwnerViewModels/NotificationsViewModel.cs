using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.ViewModels.OwnerViewModels
{
    internal class NotificationsViewModel : ViewModelBase
    {
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ICommand ShowGuestRatingCommand;

        public NotificationsViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowGuestRatingCommand = new ViewModelCommand(ShowGuestRatingView);
        }

        public void ShowGuestRatingView(object obj)
        {
            _mainViewModel.ExecuteShowGuestRatingCommand(obj);
        }
    }
}
