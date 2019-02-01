using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class HomeVM
    {
        public NavigationCommand NavCommand { get; set; }

        public HomeVM()
        {
            NavCommand = new NavigationCommand(this);
        }

        public async void Navigate()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}
