using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_FinishedToursViewModel : ViewModelBase
    {
        // commands
        public ViewModelCommand ShowToursCommand { get; private set; }
        public ViewModelCommand ShowTourDataCommand { get; private set; }

        // assets 
        private TourService tourService;
        public ObservableCollection<FinishedTourDTO> observableFinishedTourDTOs { get; private set; } = new ObservableCollection<FinishedTourDTO>();
        
        private FinishedTourDTO selectedFinishedTourDTO;
        public FinishedTourDTO SelectedFinishedTourDTO
        {
            get { return selectedFinishedTourDTO; }
            set
            {
                if (selectedFinishedTourDTO != value)
                {
                    selectedFinishedTourDTO = value;
                    OnPropertyChanged(nameof(SelectedFinishedTourDTO));
                }
            }
        }
        // command 
        public TourGuide_MainViewModel _mainViewModel;

        public TourGuide_FinishedToursViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowToursCommand = new ViewModelCommand(ShowTours);
            ShowTourDataCommand = new ViewModelCommand(ShowTourData);
            ShowDTOsOnDataGrid();
        }

        public void ShowDTOsOnDataGrid()
        {
            this.tourService = new(new TourRepository());
            List<Tour> finishedTours = this.tourService.GetAllFinishedTours();
            List<FinishedTourDTO> finishedToursDtos = new List<FinishedTourDTO>();

            foreach (Tour t in finishedTours)
            {
                finishedToursDtos.Add(tourService.createFinishedToursDTO(t));

            }
            foreach (FinishedTourDTO f in finishedToursDtos)
            {
                observableFinishedTourDTOs.Add(f);
            }
        }
        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);
        }
        public void ShowTourData(object obj)
        {
            if (SelectedFinishedTourDTO == null)
            {
                MessageBox.Show("Please select a tour from the list to show its data.");
                return;
            }

            FinishedTourDTO tourData = SelectedFinishedTourDTO; 
            DataBaseContext dataBaseContext = new DataBaseContext();
            TourLiveViewTransfer tourLiveViewTransfer = new TourLiveViewTransfer(tourData.id);
            dataBaseContext.TourLiveViewTransfers.Add(tourLiveViewTransfer);
            dataBaseContext.SaveChanges();
            _mainViewModel.ExecuteShowTourGuideTourDataViewCommand(null);

        }
    }
}
