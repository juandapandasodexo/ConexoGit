using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Configuration;
using Conexo.Droid.Classes.IOC;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Conexo.Droid
{
    [Activity(Label = "Conexo.Droid", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public static MainActivity Instance{
            get;
            private set;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("21080b33-67fc-41e6-8714-d54eccdb9405",
                   typeof(Analytics), typeof(Crashes));
            //Para tener una instancia. Para las inyecciones de dependencia
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //Nuget de PopUps
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            //Inicializamos el IoC (Inyeccion de dependencia)
            AndroidIOCConfiguration.Instance.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
			LoadApplication(new App());

            // Nuget Scanner
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 41, 40, 99));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
