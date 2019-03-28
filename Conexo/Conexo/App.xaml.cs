using Conexo.Views;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using FreshMvvm;
using Conexo.ViewModels;
using Conexo.Classes.IOC;
using Conexo.Navigation;

namespace Conexo
{
    public partial class App : Application
    {
        public static App Instance { get; private set; }

        public App()
        {
            Instance = this;

            //Inicializamos el IoC (Inyeccion de dependencia)
            FormsIOCConfiguration.Instance.Init();
            InitializeComponent();
            LoadLogin();
        }


        public void LoadLogin()
        {
            LoadRootNav<LoginViewModel>();
        }


        public void LoadRootNav<T>() where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>();
            var basicNavContainer = new FreshNavigationContainer(page);

            basicNavContainer.BarBackgroundColor = GetBarColor();
            basicNavContainer.BarTextColor = Color.White;

            MainPage = basicNavContainer;
        }

        public void LoadCustomNav()
        {
            MainPage = new CustomImplementedNav();
        }

        private Color GetBarColor(){
            Color myRgbColor = new Color();
            myRgbColor = Color.FromRgb(41, 40, 99);
            return myRgbColor;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=f124f731-5de9-4d64-be60-b6c2305b8233;"
                            + "android=c542f1e3-4fe0-414e-99f4-e741942f8c9b", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
