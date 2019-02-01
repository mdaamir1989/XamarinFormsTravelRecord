using System.ComponentModel;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public RegisterVM()
        {
            User = new User();
            RegisterCommand = new RegisterCommand(this);
        }


        public async void Register()
        {
            await User.Insert(User);

            //Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());

            //if (passwordEntry.Text == confirmPasswordEntry.Text)
            //{
            //    //User user = new User
            //    //{
            //    //    Email = emailEntry.Text,
            //    //    Password = passwordEntry.Text
            //    //};

            //    //await App.MobileService.GetTable<User>().InsertAsync(user);
            //    await User.Insert(user);
            //}
            //else
            //{
            //    await DisplayAlert("Error", "Passwords do not match", "OK");
            //}
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
