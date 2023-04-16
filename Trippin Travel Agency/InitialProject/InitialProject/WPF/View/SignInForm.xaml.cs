using InitialProject.Context;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.WPF.View;
using InitialProject.WPF.View.Owner_Views;
using InitialProject.WPF.View.GuestOne_Views;
using InitialProject.WPF.View.GuestTwoViews;
using InitialProject.WPF.View.TourGuideViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;


namespace InitialProject.WPF
{
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {

                if(user.password == txtPassword.Password)
                {
                    OwnerInterface ownerInterface = new OwnerInterface();
                    TourGuideInterface tourGuideInterface = new TourGuideInterface();
                    GuestTwoInterface guestTwoInterface = new GuestTwoInterface();
                    GuestOneInterface guestOneInterface = new GuestOneInterface();
                   
                    LoggedUser.id = user.id;
                    LoggedUser.username = user.username;
                    LoggedUser.role = user.role;
                    LoggedUser.firstName = user.firstName;
                    LoggedUser.lastName = user.lastName;
                    LoggedUser.email = user.email;


                    DataBaseContext context = new DataBaseContext();
                    List<Coupon> coupons = context.Coupons.ToList();
                    DateTime now = DateTime.Now;
                    foreach (Coupon coupon in coupons) {
                        if (coupon.exiresOn < now) {
                            context.Remove(coupon);
                            context.SaveChanges();
                        }
                    }
                    
                    foreach (TourRequest tourRequest in context.TourRequests.ToList())
                    {
                        if (DateTime.Now >= tourRequest.endDate.AddHours(-48))
                        {
                            context.Remove(tourRequest);
                            context.SaveChanges();
                        }
                    }

                    if (user.role == "Owner")
                    {
                        ownerInterface.Show();
                    }
                    else if (user.role == "TourGuide")
                    {
                        tourGuideInterface.Show();

                    }
                    else if (user.role == "GuestTwo")
                    {
                        guestTwoInterface.Show();
                    }
                    else if (user.role == "GuestOne")
                    {
                        guestOneInterface.Show();
                    }
                    Close();
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
