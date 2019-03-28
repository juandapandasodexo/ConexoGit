using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Dependencies.Firebase;
using Conexo.Classes.Helpers;
using Conexo.Classes.Services;
using Domain.Models.Request;
using Domain.Services;
using FreshMvvm;
using Xamarin.Forms;

namespace Conexo.ViewModels
{

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ChangePasswordViewModel : FreshBasePageModel
    {

        private NavigationService _navigationService;
        private ILoginService _loginService;
        private MessageService _messageService;
        private IFirebaseAnalyticsDependency _firebaseAnalyticsDependency;

        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRunning { get; set; }


        public ICommand ConfirmTapCommand { get { return new Command(async () => { await ConfirmTap(); }); } }
        public ICommand CancelTapCommand { get { return new Command(() => { DestroyThisModal(); }); } }

        public ChangePasswordViewModel(MessageService messageService, NavigationService navigationService, ILoginService loginService, IFirebaseAnalyticsDependency firebaseAnalyticsDependency)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _messageService = messageService;
            _firebaseAnalyticsDependency = firebaseAnalyticsDependency;

            IsEnabled = true;
            IsRunning = false;

        }

        public void DestroyThisModal()
        {
            IsEnabled = true;
            IsRunning = false;

            this.UserName = null;
            this.CurrentPassword = null;
            this.NewPassword = null;
            this.ConfirmNewPassword = null;


        }

        public async Task ConfirmTap()
        {
            if (IsEnabled == false)
            {
                return;
            }

            IsEnabled = false;
            IsRunning = true;


            if (string.IsNullOrEmpty(this.UserName))
            {
                await _messageService.ShowErrorMessage(Application.Current.Resources["UserNameValidation"].ToString());
                this.IsEnabled = true;
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.CurrentPassword))
            {
                await _messageService.ShowErrorMessage(App.Current.Resources["CurrentPasswordValidation"].ToString());
                this.IsEnabled = true;
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.NewPassword))
            {
                await _messageService.ShowErrorMessage(App.Current.Resources["NewPasswordValidation"].ToString());
                this.IsEnabled = true;
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.ConfirmNewPassword))
            {
                await _messageService.ShowErrorMessage(App.Current.Resources["ConfirmNewPasswordValidation"].ToString());
                this.IsEnabled = true;
                this.IsRunning = false;
                return;
            }


            if (this.ConfirmNewPassword != this.NewPassword)
            {
                await _messageService.ShowErrorMessage(App.Current.Resources["SamePasswordValidation"].ToString());
                this.IsEnabled = true;
                this.IsRunning = false;
                return;
            }

            try
            {
                ChangePasswordRequestModel model = new ChangePasswordRequestModel();
                model.contraseniaActual = this.CurrentPassword;
                model.contraseniaNueva = this.NewPassword;
                model.usuario = this.UserName;


                var change = await _loginService.ChangePassword(model);

                DestroyThisModal();

                await _messageService.ShowSuccessMessage(Application.Current.Resources["TitleChangePasswordPage"].ToString(), Application.Current.Resources["SuccessChangingPassword"].ToString());


            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);

            }
            finally
            {

                this.IsEnabled = true;
                this.IsRunning = false;
            }

        }



    }
}
