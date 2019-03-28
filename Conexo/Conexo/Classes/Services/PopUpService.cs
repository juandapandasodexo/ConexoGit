using System;
using System.Threading.Tasks;
using Conexo.Pages.PopUp;
using Conexo.ViewModels.PopUp;
using Conexo.Views.PopUp;
using Domain.Models.Response;
using Rg.Plugins.Popup.Services;

namespace Conexo.Classes.Services
{
    public class PopUpService
    {
        public PopUpService()
        {
        }

        public async Task ShowChangePasswordAsync(string userName = null,string password = null){

            var changePasswordPage = new ChangePasswordPopUpPage();
            changePasswordPage.ViewModel.UserName = userName;
            changePasswordPage.ViewModel.CurrentPassword = password;
            await PopupNavigation.Instance.PushAsync(changePasswordPage);

        }

        public async Task ShowForgotPasswordAsync(){
            
            var page = new ForgotPasswordPopUpPage();
            await PopupNavigation.Instance.PushAsync(page);
        }

        public async Task ShowSellingPointSelection(){
            var page = new SellingPointPopUpPage();
            await PopupNavigation.Instance.PushAsync(page);
        }

        public async Task ShowValidationBonus(ValidateBonusResponseModel validateBonusResponse, bool isValid = true,Action action = null){

            var validationViewModel = new ValidationBonusPopUpViewModel(validateBonusResponse, isValid ,() =>
            {
                action?.Invoke();
            });

            var page = new ValidationBonusPopUpPage(validationViewModel);
            await PopupNavigation.Instance.PushAsync(page);
        }

    }
}
