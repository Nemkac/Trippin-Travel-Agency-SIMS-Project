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
using InitialProject.WPF.View.GuestOne_Views;
using System.Windows.Media;
using System.Windows.Controls;
using InitialProject.WPF.View.Owner_Views;
using SharpVectors.Dom;
using System.IO.Packaging;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class GenerateReportViewModel : ViewModelBase
    {
        private BookingService bookingService { get; set; }
        public ViewModelCommand GeneratePDF { get; set; }
        public ViewModelCommand Help { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand GoBack { get; set; }
        bool isHelpOn = false;
        public GenerateReportViewModel()
        {
            bookingService = new BookingService(new BookingRepository());
            GeneratePDF = new ViewModelCommand(PDF);
            Help = new ViewModelCommand(ShowHelp);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            GoBack = new ViewModelCommand(GoToPastBookings);
            Introduction = "Here you can generate a report with all of your bookings or only canceled ones, in one certain period you pick.\nIf you want canceled bookings only, drag a slider to right, otherwise leave it to the left.";
            StartingDate = DateTime.Today;
            EndingDate = DateTime.Today;
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

        private string helpLand;
        public string HelpLand
        {
            get { return helpLand; }
            set
            {
                if (helpLand != value)
                {
                    helpLand = value;
                    OnPropertyChanged(nameof(HelpLand));
                }
            }
        }

        private string helpExit;
        public string HelpExit
        {
            get { return helpExit; }
            set
            {
                if (helpExit != value)
                {
                    helpExit = value;
                    OnPropertyChanged(nameof(HelpExit));
                }
            }
        }

        private string warningMessage;
        public string WarningMessage
        {
            get { return warningMessage; }
            set
            {
                if (warningMessage != value)
                {
                    warningMessage = value;
                    OnPropertyChanged(nameof(WarningMessage));
                }
            }
        }

        public void ShowHelp(object sedner)
        {
            if (isHelpOn)
            {
                HelpLand = string.Empty;
                HelpExit = string.Empty;
                isHelpOn = false;
            }
            else
            {
                HelpLand = "Select dates to define a date range.\nPDF report will contain all the bookings/canceled bookings in stated date range.";
                HelpExit = "To exit Help, press CTRL + H again";
                isHelpOn = true;
            }
        }

        private void ShowNavigator(object sender)
        {
            GuestOneStaticHelper.InterfaceToGoBack = GuestOneStaticHelper.generateReportInterface;
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.generateReportInterface.Left + (GuestOneStaticHelper.generateReportInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.generateReportInterface.Top + (GuestOneStaticHelper.generateReportInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.generateReportInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private void GoToPastBookings(object sender)
        {
            PastBookingsInterface pastBookingsInterface = new PastBookingsInterface();
            pastBookingsInterface.Left = GuestOneStaticHelper.generateReportInterface.Left;
            pastBookingsInterface.Top = GuestOneStaticHelper.generateReportInterface.Top;
            GuestOneStaticHelper.generateReportInterface.Hide();
            pastBookingsInterface.Show();
        }

        private void PDF(object sender)
        {
            if ((StartingDate == DateTime.Today && EndingDate == DateTime.Today) || (StartingDate == EndingDate) || (StartingDate > EndingDate))
            {
                WarningMessage = "Incorect date inputs";
            }
            else
            {
                if (Choice == 0)
                {
                    GuestOneStaticHelper.ifChoseCanceled = false;
                    GuestOneStaticHelper.bookingsForReport = bookingService.GetAllInDateRange(StartingDate, EndingDate);
                }
                else
                {
                    GuestOneStaticHelper.ifChoseCanceled = true;
                    GuestOneStaticHelper.canceledBookingsForReport = bookingService.GetAllCanceledInDateRange(StartingDate, EndingDate);

                }
                GuestOneStaticHelper.chosenStartingDate = StartingDate;
                GuestOneStaticHelper.chosenEndingDate = EndingDate;
                MemoryStream lMemoryStream = new MemoryStream();
                Package package = Package.Open(lMemoryStream, FileMode.Create);
                XpsDocument doc = new XpsDocument(package);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                PDF_Report report = new PDF_Report();
                writer.Write(report);
                doc.Close();
                package.Close();
                MemoryStream outStream = new MemoryStream();
                PdfSharp.Xps.XpsConverter.Convert(lMemoryStream, outStream, false);
                FileStream fileStream = new FileStream("C:\\Trippin_Travel_Bookings.pdf", FileMode.Create);
                outStream.CopyTo(fileStream);
                outStream.Flush();
                outStream.Close();
                fileStream.Flush();
                fileStream.Close();
                WarningMessage = string.Empty;
                PdfGeneratedConfirmation pdfGeneratedConfirmation = new PdfGeneratedConfirmation();
                pdfGeneratedConfirmation.Left = GuestOneStaticHelper.generateReportInterface.Left + (GuestOneStaticHelper.generateReportInterface.Width - pdfGeneratedConfirmation.Width) / 2;
                pdfGeneratedConfirmation.Top = GuestOneStaticHelper.generateReportInterface.Top + (GuestOneStaticHelper.generateReportInterface.Height - pdfGeneratedConfirmation.Height) / 2;
                GuestOneStaticHelper.generateReportInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                pdfGeneratedConfirmation.Show();
            }
        }


    }
}
