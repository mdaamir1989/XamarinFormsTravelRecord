using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryVM ViewModel;

        public HistoryPage()
        {
            InitializeComponent();
            ViewModel = new HistoryVM();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await ViewModel.UpdatePosts();
                await AzureAppServiceHelper.SyncAsync();
            }
            catch (System.Exception ex)
            {
            }
        }

        private async void MenuItem_Clicked(object sender, System.EventArgs e)
        {
            var post = (Post)(((MenuItem)sender).CommandParameter);
            ViewModel.DeletePost(post);

            await ViewModel.UpdatePosts();
        }

        private async void ExperienceList_Refreshing(object sender, System.EventArgs e)
        {
            await ViewModel.UpdatePosts();
            await AzureAppServiceHelper.SyncAsync();
            ExperienceList.IsRefreshing = false;
        }
    }
}