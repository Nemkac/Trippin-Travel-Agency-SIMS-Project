using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Context;
using InitialProject.Model;
using System.Windows;
using InitialProject.Repository;
using InitialProject.Service.TourServices;
using InitialProject.WPF.ViewModels;
using InitialProject.DTO;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.Win32;
using MessageBox = System.Windows.MessageBox;

namespace InitialProject.WPF.ViewModels.GuestTwoViewModels
{    
    public class JoinLiveTourViewModel : ViewModelBase
    {        
        private readonly TourService tourService = new(new TourRepository());

        public ObservableCollection<KeyPointDTO> keyPoitsGrid { get; set; } = new ObservableCollection<KeyPointDTO>();


        private bool submitButtonEnabled;
        public bool SubmitButtonEnabled
        {
            get { return submitButtonEnabled; }
            set
            {
                if (submitButtonEnabled != value)
                {
                    submitButtonEnabled = value;
                    OnPropertyChanged(nameof(SubmitButtonEnabled));
                }
            }
        }

        private bool commentBoxEnabled;
        public bool CommentBoxEnabled
        {
            get { return commentBoxEnabled; }
            set
            {
                if (commentBoxEnabled != value)
                {
                    commentBoxEnabled = value;
                    OnPropertyChanged(nameof(CommentBoxEnabled));
                }
            }
        }

        private bool uploadButtonEnabled;
        public bool UploadButtonEnabled
        {
            get { return uploadButtonEnabled; }
            set
            {
                if (uploadButtonEnabled != value)
                {
                    uploadButtonEnabled = value;
                    OnPropertyChanged(nameof(UploadButtonEnabled));
                }
            }
        }

        private string tourName;
        public string TourName
        {
            get { return tourName; }
            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }

        private string keyPointLabel;
        public string KeyPointLabel
        {
            get { return keyPointLabel; }
            set
            {
                if (keyPointLabel != value)
                {
                    keyPointLabel = value;
                    OnPropertyChanged(nameof(KeyPointLabel));
                }
            }
        }
        private string commentBox;
        public string CommentBox
        {
            get { return commentBox; }
            set
            {
                if (commentBox != value)
                {
                    commentBox = value;
                    OnPropertyChanged(nameof(CommentBox));
                }
            }
        }
        
        private bool knowledge1;
        public bool Knowledge1
        {
            get { return knowledge1; }
            set
            {
                if (knowledge1 != value)
                {
                    knowledge1 = value;
                    OnPropertyChanged(nameof(Knowledge1));
                }
            }
        }
        private bool knowledge2;
        public bool Knowledge2
        {
            get { return knowledge2; }
            set
            {
                if (knowledge2 != value)
                {
                    knowledge2 = value;
                    OnPropertyChanged(nameof(Knowledge2));
                }
            }
        }
        private bool knowledge3;
        public bool Knowledge3
        {
            get { return knowledge3; }
            set
            {
                if (knowledge3 != value)
                {
                    knowledge3 = value;
                    OnPropertyChanged(nameof(Knowledge3));
                }
            }
        }

        private bool knowledge4;
        public bool Knowledge4
        {
            get { return knowledge4; }
            set
            {
                if (knowledge4 != value)
                {
                    knowledge4 = value;
                    OnPropertyChanged(nameof(Knowledge4));
                }
            }
        }

        private bool knowledge5;
        public bool Knowledge5
        {
            get { return knowledge5; }
            set
            {
                if (knowledge5 != value)
                {
                    knowledge5 = value;
                    OnPropertyChanged(nameof(Knowledge5));
                }
            }
        }
        
        private bool content1;
        public bool Content1
        {
            get { return content1; }
            set
            {
                if (content1 != value)
                {
                    content1 = value;
                    OnPropertyChanged(nameof(Content1));
                }
            }
        }

        private bool content2;
        public bool Content2
        {
            get { return content2; }
            set
            {
                if (content2 != value)
                {
                    content2 = value;
                    OnPropertyChanged(nameof(Content2));
                }
            }
        }

        private bool content3;
        public bool Content3
        {
            get { return content3; }
            set
            {
                if (content3 != value)
                {
                    content3 = value;
                    OnPropertyChanged(nameof(Content3));
                }
            }
        }

        private bool content4;
        public bool Content4
        {
            get { return content4; }
            set
            {
                if (content4 != value)
                {
                    content4 = value;
                    OnPropertyChanged(nameof(Content4));
                }
            }
        }

        private bool content5;
        public bool Content5
        {
            get { return content5; }
            set
            {
                if (content5 != value)
                {
                    content5 = value;
                    OnPropertyChanged(nameof(Content5));
                }
            }
        }

        private bool translation1;
        public bool Translation1
        {
            get { return translation1; }
            set
            {
                if (translation1 != value)
                {
                    translation1 = value;
                    OnPropertyChanged(nameof(Translation1));
                }
            }
        }

        private bool translation2;
        public bool Translation2
        {
            get { return translation2; }
            set
            {
                if (translation2 != value)
                {
                    translation2 = value;
                    OnPropertyChanged(nameof(Translation2));
                }
            }
        }

        private bool translation3;
        public bool Translation3
        {
            get { return translation3; }
            set
            {
                if (translation3 != value)
                {
                    translation3 = value;
                    OnPropertyChanged(nameof(Translation3));
                }
            }
        }


        private bool translation4;
        public bool Translation4
        {
            get { return translation4; }
            set
            {
                if (translation4 != value)
                {
                    translation4 = value;
                    OnPropertyChanged(nameof(Translation4));
                }
            }
        }

        private bool translation5;
        public bool Translation5
        {
            get { return translation5; }
            set
            {
                if (translation5 != value)
                {
                    translation5 = value;
                    OnPropertyChanged(nameof(Translation5));
                }
            }
        }


        public ViewModelCommand JoinTourCommand { get; private set; }
        public ViewModelCommand SubmitRatingCommand { get; private set; }
        public ViewModelCommand UploadPhoto { get; private set; }

        public JoinLiveTourViewModel() 
        {
            JoinTourCommand = new ViewModelCommand(JoinTour);
            SubmitRatingCommand = new ViewModelCommand(SubmitRating);
            UploadPhoto = new ViewModelCommand(UploadImage);
            LoadTourInfo();
        }

        public void LoadTourInfo()
        {

            DataBaseContext context = new DataBaseContext();
            Tour activeTour = this.tourService.GetActiveTour(context);

            SubmitButtonEnabled = false;
            CommentBoxEnabled = false;
            UploadButtonEnabled = true;
            if (activeTour != null)
            {
                TourName = activeTour.name;
                List<KeyPoint> keyPoints = this.tourService.GetKeyPoints(activeTour.id, context);
                foreach (KeyPoint kp in keyPoints) 
                {
                    KeyPointDTO keyPointFilter = new KeyPointDTO(kp.name, kp.visited);
                    keyPoitsGrid.Add(keyPointFilter);
                }
                foreach (KeyPoint keyPoint in keyPoints)
                {
                    if (!keyPoint.visited)
                    {
                        KeyPointLabel = "'" + keyPoint.name + "'";
                        break;
                    }
                }
                CheckForAttendanceConfirmation(context, activeTour);
            }
        }
        public void CheckForAttendanceConfirmation(DataBaseContext context, Tour activeTour)
        {
            foreach (TourReservation reservation in context.TourReservations.ToList())
            {
                if (activeTour.id == reservation.tourId && reservation.guideConfirmed == true)
                {
                    SubmitButtonEnabled = true;
                    UploadButtonEnabled = true;
                    CommentBoxEnabled = true;
                }
            }
        }

        private void JoinTour(object obj)
        {
            DataBaseContext context = new DataBaseContext();
            Tour activeTour = this.tourService.GetActiveTour(context);

            foreach (TourReservation tourReservation in context.TourReservations.ToList())
            {
                if (tourReservation.tourId == activeTour.id)
                {
                    tourReservation.guestJoined = true;
                    context.SaveChanges();
                }
            }
        }
        public void SubmitRating(object obj)
        {

            int guideKnowledge = -1;
            int contentRating = -1;
            int guideLanguageUsage = -1;
            string comment = string.Empty;

            if ((bool)Knowledge1)
            {
                guideKnowledge = 1;
            }
            else if ((bool)Knowledge2)
            {
                guideKnowledge = 2;
            }
            else if ((bool)Knowledge3)
            {
                guideKnowledge = 3;
            }
            else if ((bool)Knowledge4)
            {
                guideKnowledge = 4;
            }
            else if ((bool)Knowledge5)
            {
                guideKnowledge = 5;
            }

            if ((bool)Content1)
            {
                contentRating = 1;
            }
            else if ((bool)Content2)
            {
                contentRating = 2;
            }
            else if ((bool)Content3)
            {
                contentRating = 3;
            }
            else if ((bool)Content4)
            {
                contentRating = 4;
            }
            else if ((bool)Content5)
            {
                contentRating = 5;
            }

            if ((bool)Translation1)
            {
                guideLanguageUsage = 1;
            }
            else if ((bool)Translation2)
            {
                guideLanguageUsage = 2;
            }
            else if ((bool)Translation3)
            {
                guideLanguageUsage = 3;
            }
            else if ((bool)Translation4)
            {
                guideLanguageUsage = 4;
            }
            else if ((bool)Translation5)
            {
                guideLanguageUsage = 5;
            }

            if (CommentBox != null)
            {
                comment = CommentBox;
            }

            DataBaseContext context = new DataBaseContext();
            Tour activeTour = this.tourService.GetActiveTour(context);
            TourAndGuideRate tourAndGuideRate = new TourAndGuideRate(LoggedUser.id, activeTour.id, guideKnowledge, guideLanguageUsage, contentRating, comment, activeTour.guideId);
            context.TourAndGuideRates.Add(tourAndGuideRate);
            context.SaveChanges();
            SubmitButtonEnabled = false;
            CommentBoxEnabled = false;
            
        }
        public void UploadImage(object obj)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";           
            bool? response = fileDialog.ShowDialog();  
            if(response == true)
            {

                string filepath = fileDialog.FileName;
                string destination = System.IO.Path.Combine("Assets",System.IO.Path.GetFileName(filepath));
                System.IO.File.Copy(filepath, destination, true);
                int tourId = tourService.GetByName(TourName).id;
                DataBaseContext context = new DataBaseContext();                
                destination = destination.Replace("\\" , "/");
                context.Images.Add(new Image("pack://application:,,,/" + destination, tourId));
                context.SaveChanges();
            }
        }
    }
}
