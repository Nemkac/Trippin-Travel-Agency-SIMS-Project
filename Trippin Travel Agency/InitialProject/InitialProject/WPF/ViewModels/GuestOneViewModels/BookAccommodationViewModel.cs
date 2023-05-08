using InitialProject.Context;
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
        int imageCounter = 0;
        public ViewModelCommand DataGridKeyDown { get; set; }
        public ViewModelCommand GoToPreviousWindow { get; set; }


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


        private string accommodationImage;
        public string AccommodationImage
        {
            get { return accommodationImage; }
            set
            {
                if (accommodationImage != value)
                {
                    accommodationImage = value;
                    OnPropertyChanged(nameof(AccommodationImage));
                }
            }
        }

        public ViewModelCommand BookAccommodation { get; set; }
        public ViewModelCommand NextImage { get; set; }
        public ViewModelCommand PreviousImage { get; set; }
        public BookAccommodationViewModel()
        {
            DataBaseContext context = new DataBaseContext();
            List<Model.Image> images = context.Images.ToList();
            Accommodation accommodation = accommodationService.GetById(GuestOneStaticHelper.id);
            AccommodationInfoLabels = "Accommodation name:" + "\n\nLocation:" + "\n\nMaximum number of guests:" + "\n\nRating out of 10:";
            AccommodationImage = images[0].imageLink;
            AccommodationInfo = GuestOneStaticHelper.id.ToString();
            AccommodationInfo = accommodation.name + "\n\n" + this.accommodationService.GetAccommodationLocation(GuestOneStaticHelper.id)[0]+ " , " +
                accommodationService.GetAccommodationLocation(GuestOneStaticHelper.id)[1] + "\n\n" + accommodation.guestLimit + "\n\n" +
                accommodationRateService.GetAccommodationAverageRate(GuestOneStaticHelper.id);

            AvaialableDatesGrid = GuestOneStaticHelper.result;
            BookAccommodation = new ViewModelCommand(Book);
            NextImage = new ViewModelCommand(ShowNextImage);
            PreviousImage = new ViewModelCommand(ShowPreviousImage);
            GoToPreviousWindow = new ViewModelCommand(GoBack);
        }

        private void GoBack(object sender)
        {
            
        }

        private void Book(object sender)
        {
            string arrival, departure, guestsNumber;
            GetBasicAccommodationBookingProperties(out arrival, out departure, out guestsNumber);
            if (int.Parse(guestsNumber) > this.accommodationService.GetById(GuestOneStaticHelper.id).guestLimit)
            {
                WarningMessage = accommodationService.GetById(GuestOneStaticHelper.id).name + " cannot take more then " + this.accommodationService.GetById(GuestOneStaticHelper.id).guestLimit.ToString() + " guests";
            }
            else
            {
                SaveBooking(accommodationService, arrival, departure, guestsNumber);
                WarningMessage = string.Empty;
            }
        }

        public void ShowNextImage(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Model.Image> images = context.Images.ToList();
            if (imageCounter < images.Count-1)
            {
                imageCounter++;
            } else
            {
                imageCounter = 0;
            }
            AccommodationImage = images[imageCounter].imageLink;
        }

        public void ShowPreviousImage(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            List<Model.Image> images = context.Images.ToList();
            if (imageCounter > 0)
            {
                imageCounter--;
            }
            else
            {
                imageCounter = images.Count-1;
            }
            AccommodationImage = images[imageCounter].imageLink;
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
            Booking booking = new Booking(GuestOneStaticHelper.id, arrival, departure, (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days, LoggedUser.id);
            bookingService.Save(booking);
        }
    }
}
