using System.Collections.Generic;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var posts = await Post.GetAllPosts();

            
            CategoriesListView.ItemsSource = Post.GetCategories(posts);
            PostCountLabel.Text = posts.Count.ToString();
        }
    }
}