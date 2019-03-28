using System;
using System.Threading.Tasks;
using Common.Dependencies.Resources;
using Common.Exceptions;
using Conexo.Classes.Helpers;
using Conexo.ViewModels;
using Conexo.ViewModels.PopUp;
using Conexo.Views.Messages;
using Conexo.Views.PopUp;
using Domain.Models.Response;
using FluentValidation;
using Rg.Plugins.Popup.Services;
using System.Linq;
using Conexo.Pages.PopUp;

namespace Conexo.Classes.Services
{
    public class MessageService
    {
        private IResourcesDependency _resourceDependency;
        private NavigationService _navigationService; 

        public MessageService(IResourcesDependency resourceDependency,NavigationService navigationService)
        {
            _resourceDependency = resourceDependency;
            _navigationService = navigationService;
        }

        public async Task HandleLoginExeption(Exception ex){

            string exMessage = ex.Message;


            string exName = ex.GetType().Name;
            if(exName == nameof(ValidationException)){
                exMessage = ((ValidationException)ex).Errors.First().ErrorMessage;
            }


            await ShowLoginMessage(_resourceDependency.ResolveString("TitleLoginMessagePage"),exMessage);
        }

        public async Task ShowLoginMessage(string title,string message,Action action = null)
        {
            var loginViewModel = new LoginMessagePopUpViewModel(title,message,action);
            var page = new LoginMessagePopUpPage(loginViewModel);
            await PopupNavigation.Instance.PushAsync(page);
        }

        public async Task HandleException(Exception ex)
        {

            string exName = ex.GetType().Name;

            string exMessage = ex.Message;

            switch (exName)
            {
                case nameof(ValidationException):
                    var errrorMessage = ((ValidationException)ex).Errors.First().ErrorMessage;
                    await ShowErrorMessage(errrorMessage);
                    break;
                case nameof(AppException):
                case nameof(WSErrorException):
                case nameof(NoInternetException):
                    await ShowErrorMessage(exMessage);
                    break;
                case nameof(NoAuthException):
                    await ShowErrorMessage("su sesión a vencido", LogOutHandler);
                    break;
                case nameof(TimeOutException):
                    await ShowErrorMessage("ha tardado mucho la respuesta del servidor. revisa tu conexión a internet. (vpn, proxy, etc).");
                    break;
                default:
                    await ShowErrorMessage("ha ocurrido un error no controlado. por favor intenta mas tarde");
                    break;
            }
        }

        public async Task ShowErrorMessage(string error, Action action = null)
        {
            ErrorMessage view = new ErrorMessage(error, action);
            await PopupNavigation.Instance.PushAsync(view);

        }
        public async Task ShowErrorMessage(string title, string error, Action action = null)
        {
            ErrorMessage view = new ErrorMessage(title, error, action);
            await PopupNavigation.Instance.PushAsync(view);

        }

        public async Task ShowSuccessMessage(string message)
        {
            SuccessMessage view = new SuccessMessage(message);
            await PopupNavigation.Instance.PushAsync(view);

        }

        public async Task ShowSuccessMessage(string title, string message)
        {
            SuccessMessage view = new SuccessMessage(title, message);
            await PopupNavigation.Instance.PushAsync(view);

        }

        public async Task ShowConfirmDeleteMessage(ValidateBonusResponseModel bono, Action doAction)
        {
            string description = string.Format("estas seguro de eliminar el bono {0} por valor de {1:C0}?", bono.nroBono, bono.valorBono);
            ConfirmationStatesBonusPopUpPage view = new ConfirmationStatesBonusPopUpPage("ELIMINAR BONO", description, 1, "ELIMINAR", "CANCELAR", doAction);
            await PopupNavigation.Instance.PushAsync(view);
        }

        public async Task ShowConfirmDeleteAllMessage(Action doAction)
        {
            string description = string.Format("estas seguro de eliminar todos los bonos validados?");
            ConfirmationStatesBonusPopUpPage view = new ConfirmationStatesBonusPopUpPage("CANCELAR TODO", description, 2, "SI", "NO", doAction);
            await PopupNavigation.Instance.PushAsync(view);
        }

        public async Task ShowConfirmFinishAllMessage(Action doAction)
        {
            string description = string.Format("una vez se confirme no es posible reversar la transacción ¿está seguro de enviar los bonos recibidos?");
            ConfirmationStatesBonusPopUpPage view = new ConfirmationStatesBonusPopUpPage("ENVIAR BONOS APROBADOS", description, 3, "CONFIRMAR", "VOLVER", doAction);
            await PopupNavigation.Instance.PushAsync(view);
        }

        public async Task ShowSuccessDeleteMessage()
        {
            string description = string.Format("el bono ha sido eliminado satisfactoriamente");
            ResultStatesBonusPopUpPage view = new ResultStatesBonusPopUpPage("ELIMINAR BONO", description, "CONTINUAR LEYENDO");
            await PopupNavigation.Instance.PushAsync(view);
        }

        public async Task ShowSuccessDeleteAllMessage()
        {
            string description = string.Format("todos los bonos ha sido cancelados satisfactoriamente");
            ResultStatesBonusPopUpPage view = new ResultStatesBonusPopUpPage("CANCELAR TODO", description, "VOLVER A INICIO");
            await PopupNavigation.Instance.PushAsync(view);
        }

        public async Task ShowSuccessFinishAllMessage(ValidateBonusResponseModel bono)
        {
            string description = string.Format("los bonos han sido enviados satisfactoriamente con el número de reembolso #{0}", bono.nroAprobacion.PadLeft(6, '0'));
            ResultStatesBonusPopUpPage view = new ResultStatesBonusPopUpPage("ENVIAR BONOS APROBADOS", description, "CONTINUAR");
            await PopupNavigation.Instance.PushAsync(view);
        }

        void LogOutHandler()
        {
            _navigationService.SetMainPage(NavigationService.MainPageName.Login);
        }
    }
}
