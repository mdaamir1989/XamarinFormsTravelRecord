using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        NewTravelVM ViewModel;
        public NewTravelPage()
        {
            InitializeComponent();
            ViewModel = new NewTravelVM();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Permission needed", "We need to access your location", "OK");
                    }

                    var requests = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);

                    if (requests.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = requests[Permission.LocationWhenInUse];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        VenueListView.ItemsSource = await Venue.GetVenues(location.Latitude, location.Longitude);
                    }
                }
                else
                {
                    await DisplayAlert("Location denied", "You did not give location access", "OK");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}