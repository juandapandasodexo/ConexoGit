using System;
using Xamarin.Forms;
using System.Windows.Input;
using Domain.Models.Response;
using Infraestructure.Networking;
using Conexo.Classes.Helpers;
using Rg.Plugins.Popup.Services;
using Conexo.Views.PopUp;
using Conexo.Classes;
using Domain.Models.Request;
using System.ComponentModel;
using Domain.Models;
using System.Threading.Tasks;
using Conexo.Views;
using Conexo.ViewModels.PopUp;
using Common.Exceptions;
using FreshMvvm;
using Global.Constants.Analytics;
using Domain.Services;
using Conexo.Classes.Services;
using System.Collections.Generic;
using Common.Dependencies.Firebase;

namespace Conexo.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LoginViewModel : FreshBasePageModel
    {
        private NavigationService _navigationService;
        private ILoginService _loginService;
        private PopUpService _popUpService;
        private MessageService _messageService;
        private UserModel _currentUserModel;
        private ICUCService _cucService;
        private IFirebaseAnalyticsDependency _firebaseAnalyticsDependency;


        public string User { get; set; }
        public string Password { get; set; }
        public string FontFamily { get; set; }
        //public bool IsPassword { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRunning { get; set; }
        public bool IsToggled { get; set; }
        //public string ImageSource { get; set; } 


        public ICommand ForgotPasswordCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await OnForgotPassword();
                });
            }
        }

        public ICommand LoginTapCommand
        {
            get
            {
                return new Command(async () => {
                    await OnLoginTap();
                });
            }
        }

        public LoginViewModel(ICUCService cucService,MessageService messageService,PopUpService popUpService ,NavigationService navigationService,ILoginService loginService,IFirebaseAnalyticsDependency firebaseAnalyticsDependency)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _popUpService = popUpService;
            _messageService = messageService;
            _cucService = cucService;
            _firebaseAnalyticsDependency = firebaseAnalyticsDependency;

            Init();

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            _firebaseAnalyticsDependency.SetScreenNameAndClass(AnalyticsConstants.SCREEN_LOGIN, this.GetType().Name);
        }

        void Init()
        {
            
            this.IsEnabled = true;
            this.IsToggled = true;

            this.FontFamily = Device.RuntimePlatform == Device.iOS ? "SansaPro-Light" :
                Device.RuntimePlatform == Device.Android ? "Martinesse.ttf#Martinesse" : null;

            _currentUserModel = _loginService.GetUser();
            if (_currentUserModel != null && _currentUserModel.IsRemember)
            {
                this.User = _currentUserModel.UserName;
            }
        }

        async Task OnLoginTap(){
            if (!IsEnabled)
            {
                return;
            }

            IsEnabled = false;
            IsRunning = true;

            try
            {
                LoginRequestModel loginModel = new LoginRequestModel(User, Password, IsToggled);
                var loginResponseModel = await _loginService.Login(loginModel);

                _cucService.SetCurrentCUCList(User,loginResponseModel.listPointStore);


                await RedirectToPage(loginResponseModel);
            }
            catch (NoAuthException ex)
            {
                await _messageService.HandleException(ex);
            }
            catch (Exception ex)
            {
                await _messageService.HandleLoginExeption(ex);
            }
            finally
            {
                this.Password = string.Empty;
                this.IsEnabled = true;
                this.IsRunning = false;
            }
        }

        private async Task RedirectToPage(LoginResponseModel loginResponseModel)
        {
            if (loginResponseModel.primeraSesion.Equals("0"))
            {
                await _popUpService.ShowChangePasswordAsync(this.User, this.Password);
            }
            else
            {
                _navigationService.SetMainPage(NavigationService.MainPageName.ChoiceCuc);
            }
        }

        async Task OnForgotPassword()
        {
            if (!this.IsEnabled)
            {
                return;  
            }

            this.IsEnabled = false;
            this.IsRunning = true;

            await _popUpService.ShowForgotPasswordAsync();

            this.IsEnabled = true;
            this.IsRunning = false;
        }

    }
}
