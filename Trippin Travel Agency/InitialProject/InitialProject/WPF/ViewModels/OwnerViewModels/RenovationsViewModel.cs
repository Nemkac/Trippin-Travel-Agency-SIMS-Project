using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RenovationsViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        public ObservableCollection<AccommodationRenovation> Renovations { get; set; } = new ObservableCollection<AccommodationRenovation>();
        
        private AccommodationRenovation selectedRenovation;
        public AccommodationRenovation SelectedRenovation
        {
            get { return selectedRenovation; }
            set
            {
                if (selectedRenovation != value)
                {
                    selectedRenovation = value;
                    OnPropertyChanged(nameof(SelectedRenovation));
                }
            }
        }

        private string _contentTextColor;
        public string ContentTextColor
        {
            get { return _contentTextColor; }
            set
            {
                _contentTextColor = value;
                OnPropertyChanged(nameof(ContentTextColor));
            }
        }

        private string _contentHintColor;
        public string ContentHintColor
        {
            get { return _contentHintColor; }
            set
            {
                _contentHintColor = value;
                OnPropertyChanged(nameof(ContentHintColor));
            }
        }


        private Style _dataGridStyle;
        public Style DataGridStyle
        {
            get { return _dataGridStyle; }
            set
            {
                _dataGridStyle = value;
                OnPropertyChanged(nameof(DataGridStyle));
            }
        }

        private Style _dataGridCellStyle;
        public Style DataGridCellStyle
        {
            get { return _dataGridCellStyle; }
            set
            {
                _dataGridCellStyle = value;
                OnPropertyChanged(nameof(DataGridCellStyle));
            }
        }
        private Style _dataGridColumnHeaderStyle;
        public Style DataGridColumnHeaderStyle
        {
            get { return _dataGridColumnHeaderStyle; }
            set
            {
                _dataGridColumnHeaderStyle = value;
                OnPropertyChanged(nameof(DataGridColumnHeaderStyle));
            }
        }

        private Style _dataGridRowStyle;
        public Style DataGridRowStyle
        {
            get { return _dataGridRowStyle; }
            set
            {
                _dataGridRowStyle = value;
                OnPropertyChanged(nameof(DataGridRowStyle));
            }
        }

        public ViewModelCommand CancelRenovationCommand { get; }

        private readonly OwnerInterfaceViewModel _mainViewModel;

        public ViewModelCommand ShowNewRenovationView { get; }

        public RenovationsViewModel()
        {
            _mainViewModel = LoggedUser._mainViewModel;
            ShowNewRenovationView = new ViewModelCommand(ExecuteShowNewRenovationView);
            CancelRenovationCommand = new ViewModelCommand(CancelRenovation);
            ShowRenovation();

            Mediator.IsCheckedChanged += OnIsCheckedChanged;

            ContentTextColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#192a56";
            ContentHintColor = Mediator.GetCurrentIsChecked() ? "#F4F6F8" : "#353b48";

            DataGridStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = Mediator.GetCurrentIsChecked() ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }

        private void OnIsCheckedChanged(object sender, bool isChecked)
        {
            ContentTextColor = isChecked ? "#F4F6F8" : "#192a56";
            ContentHintColor = isChecked ? "#F4F6F8" : "#353b48";

            DataGridStyle = isChecked ? (Style)Application.Current.Resources["DataGridStyle2"] : (Style)Application.Current.Resources["DataGridStyle1"];

            DataGridCellStyle = isChecked ? (Style)Application.Current.Resources["DataGridCellStyle2"] : (Style)Application.Current.Resources["DataGridCellStyle1"];

            DataGridColumnHeaderStyle = isChecked ? (Style)Application.Current.Resources["DataGridColumnHeaderStyle2"] : (Style)Application.Current.Resources["DataGridColumnHeaderStyle1"];

            DataGridRowStyle = isChecked ? (Style)Application.Current.Resources["DataGridRowStyle2"] : (Style)Application.Current.Resources["DataGridRowStyle1"];
        }


        public void ExecuteShowNewRenovationView(object obj)
        {
            _mainViewModel.ExecuteShowNewRenovationViewCommand(null);
        }

        public void ShowRenovation()
        {
            List<AccommodationRenovation> renovations = this.accommodationService.GetAllRenovations();
            List<AccommodationRenovation> renovationsToShow = new List<AccommodationRenovation>();

            foreach (AccommodationRenovation renovation in renovations)
            {
                Accommodation accommodation = this.accommodationService.GetById(renovation.accommodationId);

                if (accommodation.ownerId == LoggedUser.id)
                {
                    renovationsToShow.Add(renovation);
                }

            }

            foreach(AccommodationRenovation renovation in renovationsToShow)
            {
                Renovations.Add(renovation);
            }
        }

        public void CancelRenovation(object obj)
        {
            if (SelectedRenovation != null)
            {
                AccommodationRenovation selectedRenovation = SelectedRenovation;
                DateTime endDate = DateTime.ParseExact(selectedRenovation.endDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                DateTime startDate = DateTime.ParseExact(selectedRenovation.startDate, "M/d/yyyy", CultureInfo.InvariantCulture);
                DateTime currentDate = DateTime.Now;
                DataBaseContext renovationsContext = new DataBaseContext();

                if (selectedRenovation != null)
                {
                    if (currentDate >= endDate)
                    {
                        MessageBox.Show("This renovation is finished!");
                        SelectedRenovation = null;
                        return;
                    }
                    else if (currentDate >= startDate && currentDate <= endDate)
                    {
                        MessageBox.Show("Renovation is in progress");
                        SelectedRenovation = null;
                        return;
                    }
                    else if (startDate <= currentDate.AddDays(5))
                    {
                        MessageBox.Show("Renovation can be canceled at least 5 days before start date!");
                        SelectedRenovation = null;
                        return;
                    }
                    else
                    {
                        Renovations.Remove(selectedRenovation);
                        SelectedRenovation = null;
                        renovationsContext.AccommodationRenovations.Remove(selectedRenovation);
                        renovationsContext.SaveChanges();
                    }
                }
            }
            else MessageBox.Show("You must select an renovation in order to cancel it!");
        }
    }
}
