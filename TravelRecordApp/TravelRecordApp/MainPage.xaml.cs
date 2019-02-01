using System;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        MainVM ViewModel;

        public MainPage()
        {
            InitializeComponent();

            LoginImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", typeof(MainPage));

            ViewModel = new MainVM();
            BindingContext = ViewModel;
        }
    }
}
