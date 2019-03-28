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
using Infraestructure.Networking;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ForgotPasswordPopUpViewModel
    {
        
        private ForgotPasswordResponseModel _forgotPasswordResponseModel;
        private ILoginService _loginService;
        private MessageService _messageService;


        public string User { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRunning { get; set; }

        public ICommand ConfirmTapCommand { get { return new Command(ConfirmTap); } }
        public ICommand CancelTapCommand { get { return new Command(async () => { await CancelTap(); }); } }


        public ForgotPasswordPopUpViewModel()
        {
            _loginService = FreshMvvm.FreshIOC.Container.Resolve<ILoginService>();
            _messageService = FreshMvvm.FreshIOC.Container.Resolve<MessageService>();
            IsEnabled = true;
        }

        public async Task DestroyThisModalAsync()
        {
            this.IsEnabled = true;
            await PopupNavigation.Instance.PopAsync();
        }


        public async void ConfirmTap()
        {
            if (!this.IsEnabled)
            {
                return;
            }
            this.IsEnabled = false;
            this.IsRunning = true;

            if (string.IsNullOrEmpty(this.User))
            {
                await _messageService.ShowErrorMessage(Application.Current.Resources["TitleValidForgotPasswordPage"].ToString(), Application.Current.Resources["AllTextValidation"].ToString());
                this.IsEnabled = true;
                this.IsRunning = false;
                return;
            }

            var forgotPasswordRequestModel = new ForgotPasswordRequestModel(){ usuario = this.User };

            try
            {
                _forgotPasswordResponseModel = await _loginService.ForgotPassword(forgotPasswordRequestModel);
                await DestroyThisModalAsync();
                await _messageService.ShowSuccessMessage(_forgotPasswordResponseModel.Descripcion);
            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);

            }finally{
                
                this.IsEnabled = true;
                this.IsRunning = false;

            }

        }


        public async Task CancelTap()
        {
            if (!this.IsEnabled)
            {
                return;
            }
            this.IsEnabled = false;

            await DestroyThisModalAsync();
        }

    }
}
