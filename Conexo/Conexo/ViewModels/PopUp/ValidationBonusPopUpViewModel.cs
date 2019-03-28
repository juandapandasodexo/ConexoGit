using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Conexo.ViewModels.Base;
using Domain.Models.Response;
using Domain.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{
    public class ValidationBonusPopUpViewModel : BaseViewModel
    {
        private ICUCService _cucService;
        private ILoginService _loginService;
        private ValidateBonusResponseModel _validateBonusResponse;
        private Action _action;

        public string Title { get; set; }
        public string Description{ get; set; }
        public string Image{ get; set; }
        public string SellingPointTitle{ get; set; }
        public string SellingPointDescription{ get; set; }
        public string SellingPointProduct{ get; set; }
        public decimal SellingPointPrice{ get; set; }
        public bool IsValid{ get; set; }


        public ICommand CancelTapCommand { get { return new Command(async () => { await DestroyThisModalAsync(); }); } }
        public ICommand NewScanTapCommand { get { return new Command(async () => { await NewScanTap(); }); } }

        public ValidationBonusPopUpViewModel(ValidateBonusResponseModel validateBonusResponse,  bool isValid = true, Action action = null)
        {

            _cucService = FreshMvvm.FreshIOC.Container.Resolve<ICUCService>();
            _loginService = FreshMvvm.FreshIOC.Container.Resolve<ILoginService>();
            _action = action;
            _validateBonusResponse = validateBonusResponse;

            this.IsValid = isValid;

            Title = IsValid ? "BONO VÁLIDO" : "BONO NO VÁLIDO";
            Image = IsValid ? "icon_check" : "icon_x";
            Description = _validateBonusResponse.Descripcion;

            var userName = _loginService.GetUser().UserName;
            var sellingPoint = _cucService.GetCurrentCUC(userName);
            SellingPointTitle = sellingPoint.nombrePuntoventa;
            SellingPointDescription = sellingPoint.descripcion;
            SellingPointProduct = _validateBonusResponse.tipoBono;
            SellingPointPrice = _validateBonusResponse.valorBono;

        }


        async Task DestroyThisModalAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        async Task NewScanTap(){

            await DestroyThisModalAsync();
            _action?.Invoke();
        }



    }
}
