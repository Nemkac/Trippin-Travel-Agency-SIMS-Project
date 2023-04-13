using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public ViewModelCommand ShowReviewsViewCommand { get; private set; }
        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ProfileViewModel(OwnerInterfaceViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ShowReviewsViewCommand = new ViewModelCommand(ShowReviewsView);
        }

        public void ShowReviewsView(object obj)
        {
            _mainViewModel.ExecuteShowReviewsViewCommand(null);
        }
    }
}
