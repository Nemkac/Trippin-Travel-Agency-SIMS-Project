using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class BookAccommodationViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private BookingService bookingService = new(new BookingRepository());
        private AccommodationRateService accommodationRateService = new(new AccommodationRateRepository());


        private string accommodationInfo;
        public string AccommodationInfo
        {
            get {  return accommodationInfo;}
            set
            {
                if (accommodationInfo != value)
                {
                    accommodationInfo = value;
                    OnPropertyChanged(nameof(AccommodationInfo));
                }
            }
        }

        private string accommodationInfoLabels;
        public string AccommodationInfoLabels
        {
            get { return accommodationInfoLabels; }
            set
            {
                if (accommodationInfoLabels != value)
                {
                    accommodationInfoLabels = value;
                    OnPropertyChanged(nameof(AccommodationInfoLabels));
                }
            }
        }

        private dynamic avaialableDatesGrid;
        public dynamic AvaialableDatesGrid
        {
            get { return avaialableDatesGrid; }
            set
            {
                if (avaialableDatesGrid != value)
                {
                    avaialableDatesGrid = value;
                    OnPropertyChanged(nameof(AvaialableDatesGrid));
                }
            }
        }

        private string selectedDateRange;
        public string SelectedDateRange
        {
            get { return selectedDateRange; }
            set
            {
                if (selectedDateRange != value)
                {
                    selectedDateRange = value;
                    OnPropertyChanged(nameof(SelectedDateRange));
                }
            }
        }

        private string numberOfGuests;
        public string NumberOfGuests
        {
            get { return numberOfGuests; }
            set
            {
                if (numberOfGuests != value)
                {
                    numberOfGuests = value;
                    OnPropertyChanged(nameof(NumberOfGuests));
                }
            }
        }

        private string warningText;
        public string WarningText
        {
            get { return warningText; }
            set
            {
                if (warningText != value)
                {
                    warningText = value;
                    OnPropertyChanged(nameof(WarningText));
                }
            }
        }

        public ViewModelCommand BookAccommodation { get; set; }
        public BookAccommodationViewModel()
        {
            Accommodation accommodation = accommodationService.GetById(SelectedAccommodationToBook.id);
            AccommodationInfoLabels = "Accommodation name:" + "\n\nCountry:" + "\n\nCity:" + "\n\nMaximum number of guests:" + "\n\nRating out of 10:";

            AccommodationInfo = SelectedAccommodationToBook.id.ToString();
            AccommodationInfo = accommodation.name + "\n\n" + this.accommodationService.GetAccommodationLocation(SelectedAccommodationToBook.id)[0] + "\n\n" +
                accommodationService.GetAccommodationLocation(SelectedAccommodationToBook.id)[1] + "\n\n" + accommodation.guestLimit + "\n\n" +
                accommodationRateService.GetAccommodationAverageRate(SelectedAccommodationToBook.id);

            AvaialableDatesGrid = SelectedAccommodationToBook.result;
            BookAccommodation = new ViewModelCommand(Book);

        }

        private void Book(object sender)
        {
            string arrival, departure, guestsNumber;
            GetBasicAccommodationBookingProperties(out arrival, out departure, out guestsNumber);
            if (int.Parse(guestsNumber) > this.accommodationService.GetById(SelectedAccommodationToBook.id).guestLimit)
            {
                warningText = accommodationService.GetById(SelectedAccommodationToBook.id).name + " cannot take more then " + this.accommodationService.GetById(SelectedAccommodationToBook.id).guestLimit.ToString() + " guests.";
            }
            else
            {
                SaveBooking(accommodationService, arrival, departure, guestsNumber);
            }
        }

        private void GetBasicAccommodationBookingProperties(out string arrival, out string departure, out string guestsNumber)
        {
            string selectedDate = SelectedDateRange;
            selectedDate = selectedDate.Substring(10, selectedDate.Length - 12);
            List<string> dates = selectedDate.Split("-").ToList();
            arrival = dates[0].Substring(0, dates[0].Length - 2);
            departure = dates[1].Substring(2, dates[1].Length - 2);
            guestsNumber = numberOfGuests;
        }

        private void SaveBooking(AccommodationService accommodationService, string arrival, string departure, string guestsNumber)
        {
            Booking booking = new Booking(SelectedAccommodationToBook.id, arrival, departure, (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days, LoggedUser.id);
            bookingService.Save(booking);
        }


    }
}
