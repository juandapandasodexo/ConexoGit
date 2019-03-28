using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Conexo.ViewModels;
using FreshMvvm;
using Xamarin.Forms;

namespace Conexo.Navigation
{
    public class CustomImplementedNav : Xamarin.Forms.MasterDetailPage, IFreshNavigationService
    {
        FreshMasterDetailNavigationContainer freshMasterDetailNavigation;
        Page _choiceCucPage, _changePasswordPage, _loginPage;
        public string NavigationServiceName { get; private set; }


        public CustomImplementedNav()
        {
            NavigationServiceName = "CustomImplementedNav";
            BackgroundColor = Color.FromRgb(41, 40, 99);
            SetupPage();
            CreateMenuPage("Menu");
            RegisterNavigation();
        }

        private void SetupPage()
        {
            var page = FreshPageModelResolver.ResolvePageModel<ChoiceCucViewModel>();

            var basicNavContainer = new FreshNavigationContainer(page);
            basicNavContainer.BarBackgroundColor = GetBarColor();
            basicNavContainer.BarTextColor = Color.White;
            this.Detail = basicNavContainer;
        }

        protected void CreateMenuPage(string menuPageTitle)
        {
            var _menuPage = new ContentPage();
            _menuPage.Title = menuPageTitle;

            var _logo = new Image();
            _logo.Source = "logoSodexo.png";
            _logo.HeightRequest = 100;

            ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
            menuItems.Add(
                new MenuItem()
                {
                    Id = "1",
                    Img = "",
                    Title = "Punto de Venta"
                }
            );
            menuItems.Add(
                new MenuItem()
                {
                    Id = "2",
                    Img = "",
                    Title = "Cambiar Contraseña"
                }
            );
            menuItems.Add(
                new MenuItem()
                {
                    Id = "3",
                    Img = "",
                    Title = "Logout"
                }
            );

            var listView = new ListView();
            listView.BackgroundColor = Color.White;
            listView.HorizontalOptions = LayoutOptions.FillAndExpand;
            listView.VerticalOptions = LayoutOptions.FillAndExpand;
            listView.SeparatorColor = Color.FromHex("292863");

            listView.ItemsSource = menuItems;
            listView.ItemTemplate = new DataTemplate(typeof(CustomCell));
            listView.ItemSelected += async (sender, args) =>
            {
                MenuItem menu = (MenuItem)args.SelectedItem;
                switch (menu.Title)
                {
                    case "Punto de Venta":
                        _choiceCucPage = FreshPageModelResolver.ResolvePageModel<ChoiceCucViewModel>();
                        await PushPage(_choiceCucPage, null);
                        break;
                    case "Cambiar Contraseña":
                        _changePasswordPage = FreshPageModelResolver.ResolvePageModel<ChangePasswordViewModel>();
                        await PushPage(_changePasswordPage, null);
                        break;
                    case "Logout":
                        _loginPage = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
                        await PushPage(_loginPage, null, true, false);
                        break;
                    default:
                        break;
                }
                IsPresented = false;
            };

            var _stackLayout = new StackLayout()
            {
                Padding = new Thickness(20),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,
                Children = {
                    _logo, listView
                }
            };

            _menuPage.Content = _stackLayout;

            Master = new NavigationPage(_menuPage) { Title = "Menu", Icon = "menu" };
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public async Task PopToRoot(bool animate = true)
        {
            await Navigation.PopToRootAsync(animate);
        }

        public async Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
            {
                await Navigation.PushModalAsync(new NavigationPage(page), animate);
            }
            else {
                var basicNavContainer = new FreshNavigationContainer(page);
                basicNavContainer.BarBackgroundColor = GetBarColor();
                basicNavContainer.BarTextColor = Color.White;
                Detail = basicNavContainer;
            }
        }

        public async Task PopPage(bool modal = false, bool animate = true)
        {
            await Navigation.PopAsync(animate);
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            return null;
        }

        public void NotifyChildrenPageWasPopped()
        {

        }

        private Color GetBarColor()
        {
            Color myRgbColor = new Color();
            myRgbColor = Color.FromRgb(41, 40, 99);
            return myRgbColor;
        }

    }

}