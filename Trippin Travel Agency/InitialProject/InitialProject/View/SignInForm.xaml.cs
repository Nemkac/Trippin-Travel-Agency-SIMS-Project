using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
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
