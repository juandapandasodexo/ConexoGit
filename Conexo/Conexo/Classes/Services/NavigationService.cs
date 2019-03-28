using System;
using System.Threading.Tasks;
using Conexo.ViewModels;
using Conexo.Views;
using FreshMvvm;
using Xamarin.Forms;

namespace Conexo.Classes.Helpers
{
    public class NavigationService
    {
        public enum MainPageName{
            ChoiceCuc,
            Login
        }

        public void SetMainPage(MainPageName pageName)
        {
            switch (pageName)
            {
                case MainPageName.ChoiceCuc:
                    App.Instance.LoadCustomNav();
                    //App.Instance.LoadRootNavHamburg();
                    //App.Instance.LoadRootNav<ChoiceCucViewModel>();
                    break;
                case MainPageName.Login:
                    App.Instance.LoadRootNav<LoginViewModel>();
                    break;
                default:
                    break;
            }
        }

        public async Task PopPage(){
            IFreshNavigationService rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>(Constants.DefaultNavigationServiceName);
            await rootNavigation.PopPage();
        }


    }
}
