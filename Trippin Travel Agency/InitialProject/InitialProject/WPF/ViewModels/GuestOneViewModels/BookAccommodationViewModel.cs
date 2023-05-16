using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service.AccommodationServices;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using InitialProject.WPF.View.GuestOne_Views;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace InitialProject.WPF.ViewModels.GuestOneViewModels
{
    public class BookAccommodationViewModel : ViewModelBase
    {
        private AccommodationService accommodationService = new(new AccommodationRepository());
        private BookingService bookingService = new(new BookingRepository());
        private AccommodationRateService accommodationRateService = new(new AccommodationRateRepository());
        private UserService userService = new UserService();

        int imageCounter = 0;
        bool isHelpOn = false;
        public ViewModelCommand DataGridKeyDown { get; set; }
        public ViewModelCommand GoToPreviousWindow { get; set; }
        public ViewModelCommand OpenNavigator { get; set; }
        public ViewModelCommand Help {  get; set; }
        public ViewModelCommand BookAccommodation { get; set; }
        public ViewModelCommand NextImage { get; set; }
        public ViewModelCommand PreviousImage { get; set; }


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

        private string bonusPoints;
        public string BonusPoints
        {
            get { return bonusPoints; }
            set
            {
                if (bonusPoints != value)
                {
                    bonusPoints = value;
                    OnPropertyChanged(nameof(BonusPoints));
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

        private string helpInfo;
        public string HelpInfo
        {
            get { return helpInfo; }
            set
            {
                if (helpInfo != value)
                {
                    helpInfo = value;
                    OnPropertyChanged(nameof(HelpInfo));
                }
            }
        }

        private string helpImage;
        public string HelpImage
        {
            get { return helpImage; }
            set
            {
                if (helpImage != value)
                {
                    helpImage = value;
                    OnPropertyChanged(nameof(HelpImage));
                }
            }
        }

        private string helpDates;
        public string HelpDates
        {
            get { return helpDates; }
            set
            {
                if (helpDates != value)
                {
                    helpDates = value;
                    OnPropertyChanged(nameof(HelpDates));
                }
            }
        }

        private string helpGuests;
        public string HelpGuests
        {
            get { return helpGuests; }
            set
            {
                if (helpGuests != value)
                {
                    helpGuests = value;
                    OnPropertyChanged(nameof(HelpGuests));
                }
            }
        }

        private string helpBook;
        public string HelpBook
        {
            get { return helpBook; }
            set
            {
                if (helpBook != value)
                {
                    helpBook = value;
                    OnPropertyChanged(nameof(HelpBook));
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
                    OnPropertyChanged(nameof(helpExit));
                }
            }
        }

        private string ifRecentlyRenovated;
        public string IfRecentlyRenovated
        {
            get { return ifRecentlyRenovated; }
            set
            {
                if (ifRecentlyRenovated != value)
                {
                    ifRecentlyRenovated = value;
                    OnPropertyChanged(nameof(IfRecentlyRenovated));
                }
            }
        }

        public BookAccommodationViewModel()
        {
            DataBaseContext context = new DataBaseContext();
            //List<Model.Image> images = context.Images.ToList();
            Accommodation accommodation = accommodationService.GetById(GuestOneStaticHelper.id);
            AccommodationInfoLabels = "Accommodation name:" + "\n\nLocation:" + "\n\nMaximum number of guests:" + "\n\nRating out of 10:";
            //AccommodationImage = images[0].imageLink;
            AccommodationInfo = GuestOneStaticHelper.id.ToString();
            AccommodationInfo = accommodation.name + "\n\n" + this.accommodationService.GetAccommodationLocation(GuestOneStaticHelper.id)[0]+ " , " +
                                accommodationService.GetAccommodationLocation(GuestOneStaticHelper.id)[1] + "\n\n" + accommodation.guestLimit + "\n\n" +
                                Math.Round(accommodationRateService.GetAccommodationAverageRate(GuestOneStaticHelper.id),1);

            AvaialableDatesGrid = GuestOneStaticHelper.result;
            BookAccommodation = new ViewModelCommand(Book);
            NextImage = new ViewModelCommand(ShowNextImage);
            PreviousImage = new ViewModelCommand(ShowPreviousImage);
            GoToPreviousWindow = new ViewModelCommand(GoBack);
            OpenNavigator = new ViewModelCommand(ShowNavigator);
            Help = new ViewModelCommand(ShowHelp);
            CheckIfRecentlyRenovated();
            BonusPointsText();
        }

        public void CheckIfRecentlyRenovated()
        {
            if (accommodationService.IfAccommodationRecentlyRenovated(GuestOneStaticHelper.id))
            {
                IfRecentlyRenovated = "* This accommodation is renovated in past one year ! *";
            }
        }
        
        public void BonusPointsText()
        {
            if (userService.IsSuperGuest() != null)
            {
                BonusPoints = "You have " + userService.IsSuperGuest().points.ToString() + " bonus points available !";
            }
        }

        public void ShowHelp(object sender)
        {
            if (isHelpOn)
            {
                HelpInfo = string.Empty;
                HelpImage = string.Empty; 
                HelpDates = string.Empty; 
                HelpGuests = string.Empty; 
                HelpBook = string.Empty; 
                HelpExit = string.Empty; 
                isHelpOn = false;
            }
            else
            {
                HelpInfo = "Here you can see informations about the acccommodation you have selected";
                HelpImage = "You can go through photos with LEFT and RIGHT arrows on keyboards";
                HelpDates = "By pressing TAB you will access the list of available dates. You can go through them with UP and DOWN arrows";
                HelpGuests = "When dates are selected, press LEFT SHIFT and then enter number of guests";
                HelpBook = "When dates are selected and number of guests entered, all there left is to press ENTER and you have made your booking";
                HelpExit = "To exit Help, press CTRL + H again";
                WarningMessage = string.Empty;
                isHelpOn = true;
            }
        }

        private void ShowNavigator(object sender)
        {
            Navigator navigator = new Navigator();
            navigator.Left = GuestOneStaticHelper.bookAccommodationInterface.Left + (GuestOneStaticHelper.bookAccommodationInterface.Width - navigator.Width) / 2;
            navigator.Top = GuestOneStaticHelper.bookAccommodationInterface.Top + (GuestOneStaticHelper.bookAccommodationInterface.Height - navigator.Height) / 2;
            GuestOneStaticHelper.bookAccommodationInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
            navigator.Show();
        }

        private void GoBack(object sender)
        {
            GuestOneStaticHelper.guestOneInterface.Show();
            GuestOneStaticHelper.bookAccommodationInterface.Close();
        }

        private void Book(object sender)
        {
            if ((NumberOfGuests == null && SelectedDateRange == null) || (SelectedDateRange == null))
            {
                WarningMessage = "You must select arrival and departure dates";
            }
            else if (NumberOfGuests == null)
            {
                WarningMessage = "You must enter the number of guests";
            }
            else if (!Regex.IsMatch(NumberOfGuests, @"^\d+$"))
            {
                WarningMessage = "Invalid input for number of guests";
            }
            else if (int.Parse(NumberOfGuests) > this.accommodationService.GetById(GuestOneStaticHelper.id).guestLimit)
            {
                WarningMessage = accommodationService.GetById(GuestOneStaticHelper.id).name + " cannot take more then " + this.accommodationService.GetById(GuestOneStaticHelper.id).guestLimit.ToString() + " guests";
            }
            else
            {
                if(userService.IsSuperGuest() != null)
                {
                    SuperGuest superGuest = userService.IsSuperGuest();
                    if (superGuest.points > 1)
                    {
                        superGuest.points--;
                        DataBaseContext saveContext = new DataBaseContext();
                        saveContext.Update(superGuest);
                        saveContext.SaveChanges();
                        BonusPoints = "You have " + userService.IsSuperGuest().points.ToString() + " bonus points available !";
                    }
                    else
                    {
                        BonusPoints = string.Empty;
                        DataBaseContext context = new DataBaseContext();
                        foreach (SuperGuest superGuest1 in context.SuperGuests.ToList())
                        {
                            if (superGuest1.guestId == LoggedUser.id && superGuest1.ifActive == 1)
                            {
                                superGuest1.ifActive = 0;
                                context.Update(superGuest1);
                                context.SaveChanges();
                            }
                            else if (superGuest1.guestId == LoggedUser.id && superGuest1.ifActive == 0)
                            {
                                context.Remove(superGuest1);
                                context.SaveChanges();
                            }
                        }
                    }
                }
                string arrival, departure, guestsNumber;
                GetBasicAccommodationBookingProperties(out arrival, out departure, out guestsNumber);
                SaveBooking(accommodationService, arrival, departure, guestsNumber);
                WarningMessage = string.Empty;
                BookingConfirmationInterface bookingConfirmationInterface = new BookingConfirmationInterface();
                bookingConfirmationInterface.Left = GuestOneStaticHelper.bookAccommodationInterface.Left + (GuestOneStaticHelper.bookAccommodationInterface.Width - bookingConfirmationInterface.Width)/2;
                bookingConfirmationInterface.Top = GuestOneStaticHelper.bookAccommodationInterface.Top + (GuestOneStaticHelper.bookAccommodationInterface.Height - bookingConfirmationInterface.Height) / 2;
                GuestOneStaticHelper.bookAccommodationInterface.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#dcdde1");
                bookingConfirmationInterface.Show();
                bookingConfirmationInterface.Focus();
            }
        }

        public void ShowNextImage(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            if (context.Images.ToList().Count != 0)
            {
                List<Model.Image> images = context.Images.ToList();
                if (imageCounter < images.Count - 1)
                {
                    imageCounter++;
                }
                else
                {
                    imageCounter = 0;
                }
                AccommodationImage = images[imageCounter].imageLink;
            }
        }

        public void ShowPreviousImage(object sender)
        {
            DataBaseContext context = new DataBaseContext();
            if (context.Images.ToList().Count != 0)
            {
                List<Model.Image> images = context.Images.ToList();
                if (imageCounter > 0)
                {
                    imageCounter--;
                }
                else
                {
                    imageCounter = images.Count - 1;
                }
                AccommodationImage = images[imageCounter].imageLink;
            }
        }

        private void GetBasicAccommodationBookingProperties(out string arrival, out string departure, out string guestsNumber)
        {
            string selectedDate = SelectedDateRange;
            selectedDate = selectedDate.Substring(10, selectedDate.Length - 12);
            List<string> dates = selectedDate.Split("-").ToList();
            arrival = dates[0].Substring(0, dates[0].Length - 2);
            departure = dates[1].Substring(2, dates[1].Length - 2);
            guestsNumber = NumberOfGuests;
        }

        private void SaveBooking(AccommodationService accommodationService, string arrival, string departure, string guestsNumber)
        {
            Booking booking = new Booking(GuestOneStaticHelper.id, arrival, departure, (DateTime.Parse(departure).Subtract(DateTime.Parse(arrival))).Days, LoggedUser.id);
            bookingService.Save(booking);
        }

    }
}
