using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Model.TransferModels;
using InitialProject.Repository;
using InitialProject.Service.BookingServices;
using InitialProject.Service.GuestServices;
using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RateGuestViewModel : ViewModelBase
    {

        private BookingService bookingService = new(new BookingRepository());
        private GuestRateService guestRateService = new(new GuestRateRepository());
        public int bookingId { get; set; }
        public bool SelectedRulesRespectingRadioButton1
        {
            get { return _selectedRulesRespectingRadioButton1; }
            set
            {
                if (_selectedRulesRespectingRadioButton1 != value)
                {
                    _selectedRulesRespectingRadioButton1 = value;
                    OnPropertyChanged(nameof(SelectedRulesRespectingRadioButton1));
                }
            }
        }
        private bool _selectedRulesRespectingRadioButton1;

        public bool SelectedRulesRespectingRadioButton2
        {
            get { return _selectedRulesRespectingRadioButton2; }
            set
            {
                if (_selectedRulesRespectingRadioButton2 != value)
                {
                    _selectedRulesRespectingRadioButton2 = value;
                    OnPropertyChanged(nameof(SelectedRulesRespectingRadioButton2));
                }
            }
        }
        private bool _selectedRulesRespectingRadioButton2;

        public bool SelectedRulesRespectingRadioButton3
        {
            get { return _selectedRulesRespectingRadioButton3; }
            set
            {
                if (_selectedRulesRespectingRadioButton3 != value)
                {
                    _selectedRulesRespectingRadioButton3 = value;
                    OnPropertyChanged(nameof(SelectedRulesRespectingRadioButton3));
                }
            }
        }
        private bool _selectedRulesRespectingRadioButton3;

        public bool SelectedRulesRespectingRadioButton4
        {
            get { return _selectedRulesRespectingRadioButton4; }
            set
            {
                if (_selectedRulesRespectingRadioButton4 != value)
                {
                    _selectedRulesRespectingRadioButton4 = value;
                    OnPropertyChanged(nameof(SelectedRulesRespectingRadioButton4));
                }
            }
        }
        private bool _selectedRulesRespectingRadioButton4;

        public bool SelectedRulesRespectingRadioButton5
        {
            get { return _selectedRulesRespectingRadioButton5; }
            set
            {
                if (_selectedRulesRespectingRadioButton5 != value)
                {
                    _selectedRulesRespectingRadioButton5 = value;
                    OnPropertyChanged(nameof(SelectedRulesRespectingRadioButton5));
                }
            }
        }
        private bool _selectedRulesRespectingRadioButton5;



        public bool SelectedCleannessRadioButton1
        {
            get { return __selectedCleannessRadioButton1; }
            set
            {
                if (__selectedCleannessRadioButton1 != value)
                {
                    __selectedCleannessRadioButton1 = value;
                    OnPropertyChanged(nameof(SelectedCleannessRadioButton1));
                }
            }
        }
        private bool __selectedCleannessRadioButton1;

        public bool SelectedCleannessRadioButton2
        {
            get { return __selectedCleannessRadioButton2; }
            set
            {
                if (__selectedCleannessRadioButton2 != value)
                {
                    __selectedCleannessRadioButton2 = value;
                    OnPropertyChanged(nameof(SelectedCleannessRadioButton2));
                }
            }
        }
        private bool __selectedCleannessRadioButton2;

        public bool SelectedCleannessRadioButton3
        {
            get { return __selectedCleannessRadioButton3; }
            set
            {
                if (__selectedCleannessRadioButton3 != value)
                {
                    __selectedCleannessRadioButton3 = value;
                    OnPropertyChanged(nameof(SelectedCleannessRadioButton3));
                }
            }
        }
        private bool __selectedCleannessRadioButton3;

        public bool SelectedCleannessRadioButton4
        {
            get { return __selectedCleannessRadioButton4; }
            set
            {
                if (__selectedCleannessRadioButton4 != value)
                {
                    __selectedCleannessRadioButton4 = value;
                    OnPropertyChanged(nameof(SelectedCleannessRadioButton4));
                }
            }
        }
        private bool __selectedCleannessRadioButton4;

        public bool SelectedCleannessRadioButton5
        {
            get { return __selectedCleannessRadioButton5; }
            set
            {
                if (__selectedCleannessRadioButton5 != value)
                {
                    __selectedCleannessRadioButton5 = value;
                    OnPropertyChanged(nameof(SelectedCleannessRadioButton5));
                }
            }
        }
        private bool __selectedCleannessRadioButton5;


        private string feedBack;
        public string FeedBack
        {
            get { return feedBack; }
            set
            {
                if (feedBack != value)
                {
                    feedBack = value;
                    OnPropertyChanged(nameof(FeedBack));
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        public ViewModelCommand SaveRatingCommand { get; set; }

        public RateGuestViewModel() 
        {
            DataBaseContext ratingContext = new DataBaseContext();
            BookingTransfer transferedBooking = ratingContext.SelectedRatingNotificationTransfer.First();
            this.bookingId = transferedBooking.bookingId;
            SaveRatingCommand = new ViewModelCommand(SaveRate);
        }

        private void SaveRate(object obj)
        {
            //BookingService bookingService = new BookingService();
            int cleannessRate = GetCleanness();
            int rulesRate = GetRulesRespecting();
            string comment = Comment;
            int guestId = bookingService.GetGuestId(this.bookingId);
            GuestRate newGuestRate = new GuestRate(cleannessRate, rulesRate, comment, guestId, this.bookingId);
            guestRateService.Save(newGuestRate);
            DataBaseContext transferedBooking = new DataBaseContext();
            transferedBooking.SelectedRatingNotificationTransfer.Remove(transferedBooking.SelectedRatingNotificationTransfer.First());
            transferedBooking.SaveChanges();
            FeedBack = "Rating successfully saved!";
            ClearInput();
        }

        private int GetCleanness()
        {
            int cleannessRate;
            if (SelectedCleannessRadioButton1 == true)
            {
                cleannessRate = 1;
            }
            else if (SelectedCleannessRadioButton2 == true)
            {
                cleannessRate = 2;
            }
            else if (SelectedCleannessRadioButton3 == true)
            {
                cleannessRate = 3;
            }
            else if (SelectedCleannessRadioButton4 == true)
            {
                cleannessRate = 4;
            }
            else
            {
                cleannessRate = 5;
            }

            return cleannessRate;
        }

        private int GetRulesRespecting()
        {
            int rulesRate;
            if (SelectedRulesRespectingRadioButton1 == true)
            {
                rulesRate = 1;
            }
            else if (SelectedRulesRespectingRadioButton2 == true)
            {
                rulesRate = 2;
            }
            else if (SelectedRulesRespectingRadioButton3 == true)
            {
                rulesRate = 3;
            }
            else if (SelectedRulesRespectingRadioButton4 == true)
            {
                rulesRate = 4;
            }
            else
            {
                rulesRate = 5;
            }

            return rulesRate;
        }

        private void ClearInput()
        {
            SelectedCleannessRadioButton1 = false;
            SelectedCleannessRadioButton2 = false;
            SelectedCleannessRadioButton3 = false;
            SelectedCleannessRadioButton4 = false;
            SelectedCleannessRadioButton5 = false;

            SelectedRulesRespectingRadioButton1 = false;
            SelectedRulesRespectingRadioButton2 = false;
            SelectedRulesRespectingRadioButton3 = false;
            SelectedRulesRespectingRadioButton4 = false;
            SelectedRulesRespectingRadioButton5 = false;

            Comment = null;
        }
    }
}
