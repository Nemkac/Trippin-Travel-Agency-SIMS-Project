using InitialProject.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using InitialProject.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service.BookingServices;
using InitialProject.Repository;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GenerateReportViewModel : ViewModelBase
    {
        private BookingService bookingService { get; set; }
        public ViewModelCommand GeneratePDF { get; set; }
        public GenerateReportViewModel()
        {
            bookingService = new BookingService(new BookingRepository());
            GeneratePDF = new ViewModelCommand(PDF);
            Introduction = "Here you can generate a report with all of your bookings or only canceled ones, in one certain period you pick.\nIf you want canceled bookings only, drag a slider to right, otherwise leave it to the left.";
        }

        private DateTime startingDate;
        public DateTime StartingDate
        {
            get { return startingDate; }
            set
            {
                if (startingDate != value)
                {
                    startingDate = value;
                    OnPropertyChanged(nameof(StartingDate));
                }
            }
        }

        private DateTime endingDate;
        public DateTime EndingDate
        {
            get { return endingDate; }
            set
            {
                if (endingDate != value)
                {
                    endingDate = value;
                    OnPropertyChanged(nameof(EndingDate));
                }
            }
        }

        private int choice;
        public int Choice
        {
            get { return choice; }
            set
            {
                if (choice != value)
                {
                    choice = value;
                    OnPropertyChanged(nameof(Choice));
                }
            }
        }

        private string debug;
        public string Debug
        {
            get { return debug; }
            set
            {
                if (debug != value)
                {
                    debug = value;
                    OnPropertyChanged(nameof(Debug));
                }
            }
        }

        private string introduction;
        public string Introduction
        {
            get { return introduction; }
            set
            {
                if (introduction != value)
                {
                    introduction = value;
                    OnPropertyChanged(nameof(Introduction));
                }
            }
        }

        private ObservableCollection<Booking> bookingsGrid;
        public ObservableCollection<Booking> BookingsGrid
        {
            get { return bookingsGrid; }
            set
            {
                if (bookingsGrid != value)
                {
                    bookingsGrid = value;
                    OnPropertyChanged(nameof(BookingsGrid));
                }
            }
        }

        private ObservableCollection<CanceledBooking> canceledGrid;
        public ObservableCollection<CanceledBooking> CanceledGrid
        {
            get { return canceledGrid; }
            set
            {
                if (canceledGrid != value)
                {
                    canceledGrid = value;
                    OnPropertyChanged(nameof(CanceledGrid));
                }
            }
        }

        public void PDF(object sender)
        {
            if (Choice == 0)
            {
                BookingsGrid = new ObservableCollection<Booking>(bookingService.GetAllInDateRange(StartingDate, EndingDate));
            }
            else
            {
                CanceledGrid = new ObservableCollection<CanceledBooking>(bookingService.GetAllCanceledInDateRange(StartingDate, EndingDate));
            }
        }


    }
}
