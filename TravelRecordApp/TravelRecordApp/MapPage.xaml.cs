using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using TravelRecordApp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public bool HasLocationPermission = false;

        public MapPage()
        {
            InitializeComponent();
            //SetCurrentLocationOnMap();
            GetPermissions();
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (HasLocationPermission)
            {
                var position = new Position(e.Position.Latitude, e.Position.Longitude);
                MoveMap(position);
            }
        }

        public async void SetCurrentLocationOnMap()
        {
            try
            {
                if (HasLocationPermission)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        locationsMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                        MoveMap(new Position(location.Latitude, location.Longitude));
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void GetPermissions()
        {
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
                    locationsMap.IsShowingUser = true;
                    HasLocationPermission = true;
                    SetCurrentLocationOnMap();
                }
                else
                {
                    await DisplayAlert("Location denied", "You did not give location access", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (HasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Current_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            SetCurrentLocationOnMap();

            //using (var connection = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    //var connection = new SQLiteConnection(App.DatabaseLocation);
            //    connection.CreateTable<Post>();

            //    var posts = connection.Table<Post>().ToList();
            //    DisplayPinsOnMap(posts);
            //}

            DisplayPinsOnMap(await Post.GetAllPosts());
        }

        private void DisplayPinsOnMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Position(post.Latitude, post.Longitude);
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Current_PositionChanged;
        }

        private void MoveMap(Position position)
        {
            var center = new Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }
    }
}