using System;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RegisterVM ViewModel;

        public RegisterPage()
        {
            InitializeComponent();
            ViewModel = new RegisterVM();
            BindingContext = ViewModel;
        }

        //private async void RegisterBtn_Clicked(object sender, EventArgs e)
        //{
        //    if (passwordEntry.Text == confirmPasswordEntry.Text)
        //    {
        //        //User user = new User
        //        //{
        //        //    Email = emailEntry.Text,
        //        //    Password = passwordEntry.Text
        //        //};

        //        //await App.MobileService.GetTable<User>().InsertAsync(user);
        //        await User.Insert(user);
        //    }
        //    else
        //    {
        //        await DisplayAlert("Error", "Passwords do not match", "OK");
        //    }
        //}
    }
}