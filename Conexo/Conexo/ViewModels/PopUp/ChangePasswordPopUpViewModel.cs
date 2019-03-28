using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Conexo.Classes;
using Conexo.Classes.Helpers;
using Conexo.Classes.Services;
using Conexo.Views.PopUp;
using Domain.Models;
using Domain.Models.Request;
using Domain.Models.Response;
using Domain.Services;
using FreshMvvm;
using Infraestructure.Networking;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ChangePasswordPopUpViewModel
    {
        private ILoginService _loginService;
        private MessageService _messageService;

        public string UserName { get; set; }
        public string CurrentPassword{ get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword{ get; set; }
        public bool IsEnabled{ get; set; }
        public bool IsRunning{ get; set; }

        public ICommand ConfirmTapCommand { get { return new Command(async () => { await ConfirmTap(); }); }}
        public ICommand CancelTapCommand { get { return new Command(async () => { await DestroyThisModalAsync(); });}}

        public ChangePasswordPopUpViewModel()
        {
            _loginService = FreshIOC.Container.Resolve<ILoginService>();
            _messageService = FreshIOC.Container.Resolve<MessageService>();

            IsEnabled = true;
            IsRunning = false;
        }

        public async Task DestroyThisModalAsync()
        {
            IsEnabled = true;
            IsRunning = false;
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task ConfirmTap()
        {
            if (IsEnabled==false)
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


            if(this.ConfirmNewPassword!=this.NewPassword){
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

                await DestroyThisModalAsync();

                await _messageService.ShowSuccessMessage(Application.Current.Resources["TitleChangePasswordPage"].ToString(), Application.Current.Resources["SuccessChangingPassword"].ToString());


            }catch(Exception ex)
            {
                await _messageService.HandleException(ex);

            }finally{
                
                this.IsEnabled = true;
                this.IsRunning = false;
            }

        }



    }

}
