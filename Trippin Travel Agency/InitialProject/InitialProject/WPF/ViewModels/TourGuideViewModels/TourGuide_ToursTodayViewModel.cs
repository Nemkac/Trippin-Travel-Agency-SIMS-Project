using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourGuide_ToursTodayViewModel : ViewModelBase
    {
        private TourService tourService;
        public ViewModelCommand ShowToursCommands { get; private set; }
        public ViewModelCommand ShowTourLiveCommand { get; private set; }
        public ViewModelCommand ShowTourImagesCommand { get; private set; }
        public ObservableCollection<ToursTodayDTO> observableToursTodayDTOs { get; private set;  } = new ObservableCollection<ToursTodayDTO>();
        public ToursTodayDTO selectedTourTodayDTO;
        public ToursTodayDTO SelectedToursTodayDTO
        {
            get { return selectedTourTodayDTO; }
            set
            {
                if (selectedTourTodayDTO != value)
                {
                    selectedTourTodayDTO = value;
                    OnPropertyChanged(nameof(SelectedToursTodayDTO));
                }
            }
        }
        public TourGuide_MainViewModel _mainViewModel;

        public TourGuide_ToursTodayViewModel()
        {
            _mainViewModel = LoggedUser.TourGuide_MainViewModel;
            ShowToursCommands = new ViewModelCommand(ShowTours);
            ShowTourLiveCommand = new ViewModelCommand(ShowTourLive);
            ShowTourImagesCommand = new ViewModelCommand(ShowImages);
            ShowToursTodayDTOsOnDataGrid();
        }

        public void ShowToursTodayDTOsOnDataGrid()
        {
            this.tourService = new(new TourRepository());
            List<Tour> toursToday = tourService.GetAllToursToday();
            List<ToursTodayDTO> tourDtosToday = new List<ToursTodayDTO>();

            foreach (Tour t in toursToday)
            {
                tourDtosToday.Add(this.tourService.createToursTodayDTO(t));
            }
            foreach(ToursTodayDTO t in tourDtosToday)
            {
                observableToursTodayDTOs.Add(t);
            }
        }
        public void ShowTours(object obj)
        {
            _mainViewModel.ExecuteShowTourGuideToursViewCommand(null);

        }
        public void ShowTourLive(object obj)
        {
            if (SelectedToursTodayDTO == null)
            {
                MessageBox.Show("You must select a tour to start it");
                return;
            }
            if (SelectedToursTodayDTO != null)
            {
                ToursTodayDTO tourData = SelectedToursTodayDTO;
                DataBaseContext dataBaseContext = new DataBaseContext();
                TourLiveViewTransfer tourLiveViewTransfer = new TourLiveViewTransfer(tourData.id);
                dataBaseContext.TourLiveViewTransfers.Add(tourLiveViewTransfer);
                dataBaseContext.SaveChanges();
                _mainViewModel.ExecuteShowTourGuideTourLiveViewCommand(null);
            }
        }
        public void ShowImages(object obj)
        {
            if (SelectedToursTodayDTO == null)
            {
                MessageBox.Show("You must select a tour to show its images");
                return;
            }
            if (SelectedToursTodayDTO != null)
            {
                ToursTodayDTO tourData = SelectedToursTodayDTO;
                DataBaseContext dataBaseContext = new DataBaseContext();
                TourTodayImagesTransfer tourTodayImagesTransfer = new TourTodayImagesTransfer(tourData.id);
                dataBaseContext.TourTodayImagesTransfers.Add(tourTodayImagesTransfer);
                dataBaseContext.SaveChanges();
                _mainViewModel.ExecuteShowTourGuideToursTodayImagesViewCommand(null);
            }
        }
    }

}
