using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;
using Xamarin.Essentials;

namespace TravelRecordApp.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public NewTravelCommand NewTravelCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private Venue venue;
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                Post = new Post
                {
                    Experience = this.Experience,
                    Venue = this.Venue
                };
                OnPropertyChanged("Venue");
            }
        }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                Post = new Post
                {
                    Experience = this.Experience,
                    Venue = this.Venue
                };
                OnPropertyChanged("Experience");
            }
        }

        private Post post;
        public Post Post
        {
            get { return post; }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        public NewTravelVM()
        {
            NewTravelCommand = new NewTravelCommand(this);
            Venue = new Venue();
            Post = new Post();
        }

        public async void AddNewTravel(Post post)
        {
            try
            {
                if (!string.IsNullOrEmpty(Experience))
                {
                    await Post.Insert(post);
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Success", "Experience inserted successfully", "OK");
                }
            }
            catch (NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failure", "Experience not successfully", "OK");
            }
        }

        public async Task GetVenues()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                //Venues = await Venue.GetVenues(location.Latitude, location.Longitude);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
