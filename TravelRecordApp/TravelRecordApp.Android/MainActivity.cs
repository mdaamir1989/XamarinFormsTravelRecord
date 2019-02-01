
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using System.IO;

namespace TravelRecordApp.Droid
{
    [Activity(Label = "TravelRecordApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);
                //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

                Xamarin.FormsMaps.Init(this, savedInstanceState);
                CurrentPlatform.Init();
                CrossCurrentActivity.Current.Init(this, savedInstanceState);
                CrossCurrentActivity.Current.Activity = this;

                string dbName = "travel_db.sqlite";
                string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string fullPath = Path.Combine(folderPath, dbName);

                LoadApplication(new App(fullPath));
            }
            catch (System.Exception ex)
            {
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}