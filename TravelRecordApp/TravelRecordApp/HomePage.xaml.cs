using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        HomeVM ViewModel;
        public HomePage()
        {
            InitializeComponent();
            ViewModel = new HomeVM();
            BindingContext = ViewModel;
        }
    }
}