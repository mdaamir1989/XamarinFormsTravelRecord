using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
            }
        }

        private string venueName;
        public string VenueName
        {
            get { return venueName; }
            set
            {
                venueName = value;
                OnPropertyChanged("VenueName");
            }
        }

        private string categoryId;
        public string CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }


        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }


        private string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        private Venue venue;
        [JsonIgnore]
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;

                if (Venue.categories != null)
                {
                    var firstCategory = venue.categories.FirstOrDefault();

                    if (firstCategory != null)
                    {
                        CategoryId = firstCategory.id;
                        CategoryName = firstCategory.name;
                    }

                    if (venue.location != null)
                    {

                        Address = venue.location.address;
                        Distance = venue.location.distance;
                        Latitude = venue.location.lat;
                        Longitude = venue.location.lng;
                    }

                    VenueName = venue.name;
                    UserId = App.User.Id;
                }


                OnPropertyChanged("Venue");
            }
        }

        private DateTimeOffset createdat;

        public DateTimeOffset CREATEDAT
        {
            get { return createdat; }
            set { createdat = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public async static Task Insert(Post post)
        {
            //await App.MobileService.GetTable<Post>().InsertAsync(post);
            await App.postsTable.InsertAsync(post);
            await App.MobileService.SyncContext.PushAsync();
        }

        public async static Task<bool> Delete(Post post)
        {
            try
            {
                //await App.MobileService.GetTable<Post>().DeleteAsync(post);
                await App.postsTable.DeleteAsync(post);
                await App.MobileService.SyncContext.PushAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async static Task<List<Post>> GetAllPosts()
        {
            //var posts = await App.MobileService.GetTable<Post>().Where(x => x.UserId == App.User.Id).ToListAsync();
            var posts = await App.postsTable.Where(x => x.UserId == App.User.Id).ToListAsync();
            return posts;
        }

        public static Dictionary<string, int> GetCategories(List<Post> posts)
        {
            var categories = (from p in posts
                              orderby p.CategoryId
                              select p.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
            foreach (var category in categories)
            {
                var count = (from post in posts
                             where post.CategoryName == category
                             select post).ToList().Count;

                categoriesCount.Add(category, count);
            }

            return categoriesCount;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
